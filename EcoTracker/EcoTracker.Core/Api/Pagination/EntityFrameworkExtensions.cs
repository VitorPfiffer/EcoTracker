using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace EcoTracker.Core.Api.Pagination
{
    public static class EntityFrameworkExtensions
    {
        private static readonly Dictionary<string, Func<Expression, Expression, Expression>> _logicalOperatorMap =
        new(StringComparer.OrdinalIgnoreCase)
        {
             { "," , Expression.And},
             { "|" , Expression.Or },
        };

        private static readonly Dictionary<string, Func<Expression, Expression, Expression>> _operatorMap =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "=", Expression.Equal },
            { ">", Expression.GreaterThan },
            { "<", Expression.LessThan },
            { "!=", Expression.NotEqual },
        };

        public static Expression<Func<T, bool>> GetExpression<T>(string filter)
        {
            var operatorConfig = _operatorMap.Keys.FirstOrDefault(op => filter.Contains(op)) ?? throw new InvalidOperationException($"No valid operator found in filter: {filter}. Supported operators are: {string.Join(", ", _operatorMap.Keys)}.");

            var parameter = Expression.Parameter(typeof(T), "x");

            var fullFilterExpression = filter.Split(operatorConfig);

            var property = Expression.Property(parameter, fullFilterExpression[0].Trim());
            var value = Expression.Constant(Convert.ChangeType(fullFilterExpression[1].Trim(), property.Type));

            var comparison = _operatorMap[operatorConfig](property, value);

            return Expression.Lambda<Func<T, bool>>(comparison, parameter);
        }

        private static Expression<Func<T, bool>> CombineExpressions<T>(string logicalOperator,
            Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftBody = Expression.Invoke(left, parameter);
            var rightBody = Expression.Invoke(right, parameter);

            var combinedBody = _logicalOperatorMap[logicalOperator](leftBody, rightBody);

            return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PagedQuery filters)
        {
            if (string.IsNullOrEmpty(filters.Filters)) return query;

            var pattern = @"([^,|]+|[,|])";
            var spllitedFilters = Regex.Matches(filters.Filters, pattern).ToArray();

            Expression<Func<T, bool>> combinedExpression = null;

            for (int i = 0; i < spllitedFilters.Length; i += 2)
            {
                var filter = spllitedFilters[i].Value;

                var filterExpression = GetExpression<T>(filter);

                if (combinedExpression == null)
                    combinedExpression = filterExpression;
                else
                    combinedExpression = CombineExpressions(spllitedFilters[i - 1].Value, combinedExpression, filterExpression);
            }

            return query.Where(combinedExpression);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagedQuery queryParameters)
        {
            if (queryParameters == null || queryParameters.PageSize == 0 || queryParameters.Page == 0) throw new InvalidOperationException("Incorrect Pagination Query");

            var itemsToSkip = queryParameters.Page == 1 ? 0 : queryParameters.PageSize * queryParameters.Page;

            return query.Skip(itemsToSkip).Take(queryParameters.PageSize);
        }
    }
}
