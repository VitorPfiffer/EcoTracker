using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EcoTracker.Core.Swagger.ActionFilter
{
    public sealed class SwaggerDocFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            CreateRequestParametersDocumentation(operation, context);
        }

        #region Request Documentation

        private void CreateRequestParametersDocumentation(OpenApiOperation operation, OperationFilterContext context)
        {
            var path = context.ApiDescription.RelativePath;

            var pathProperties = path.Split("/").Where(x => x.Contains("{") && x.Contains("}") && !x.Equals("{language}"));

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var discard = operation.Parameters.FirstOrDefault(x => x.Name == "discard");

            if (discard != null)
                operation.Parameters.Remove(discard);

            var methodParameters = context.MethodInfo.GetParameters().Where(x => x.GetCustomAttribute<FromBodyAttribute>() != null);

            if (methodParameters.Any(x => x.Name.ToLower() == "discard"))
                operation.RequestBody = null;

            foreach (var param in methodParameters)
            {
                var paramProperties = param.ParameterType.GetProperties();

                var queryProperties = paramProperties.Where(x => x.HasAttribute<FromQueryAttribute>());
                var routeProperties = paramProperties.Where(x => x.HasAttribute<FromRouteAttribute>());

                foreach (var queryProperty in queryProperties)
                    if (!operation.Parameters.Any(x => x.Name.ToLower() == queryProperty.Name.ToLower()))
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            In = ParameterLocation.Query,
                            Name = queryProperty.Name.ToLower(),
                            Description = queryProperty.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "",
                            Schema = new OpenApiSchema()
                            {
                                Type = FormatType(queryProperty.PropertyType, enableMarkdown: false)
                            }
                        });

                foreach (var routeProperty in routeProperties)
                    if (!operation.Parameters.Any(x => x.Name.ToLower() == routeProperty.Name.ToLower()))
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            In = ParameterLocation.Path,
                            Name = routeProperty.Name.ToLower(),
                            Description = routeProperty.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "",
                            Schema = new OpenApiSchema()
                            {
                                Type = FormatType(routeProperty.PropertyType, enableMarkdown: false)
                            }
                        });
            }
        }

        private bool IsList(Type type) => type.IsGenericType && (typeof(IList<>) == type.GetGenericTypeDefinition() || typeof(ICollection<>) == type.GetGenericTypeDefinition()
                                                            || typeof(IEnumerable<>) == type.GetGenericTypeDefinition());

        private string FormatType(Type type, bool enableMarkdown = true)
        {
            var typeName = type.Name;

            if (IsList(type)) typeName = $"**Array[** *{type.GetGenericArguments()[0].Name}* **]**";

            else if (type.IsGenericType) typeName = $"**{type.Name.Replace("`1", "")}[** *{type.GetGenericArguments()[0].Name}* **]**";

            if (type.IsEnum) typeName = $"**Enum[** *{type.Name}* **]**";


            return enableMarkdown ? typeName : typeName.Replace("*", "");
        }

        #endregion
    }

    internal class ApiResultSuccessSample<TType>
    {
        public ApiResultSuccessSample(TType type)
        {
            Type = type;
        }

        public bool IsSuccessStatusCode = true;
        public TType Type;
    }
}
