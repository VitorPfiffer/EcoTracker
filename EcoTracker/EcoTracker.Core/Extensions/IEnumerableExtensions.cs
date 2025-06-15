namespace EcoTracker.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool HasDuplicates<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var seen = new HashSet<T>();
            foreach (var item in source)
            {
                if (!seen.Add(item)) // Add retorna false se já existir no HashSet
                    return true; // Encontrou duplicado
            }
            return false; // Não há duplicatas
        }
    }
}
