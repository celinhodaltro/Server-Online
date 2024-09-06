using System.Reflection;
using Microsoft.EntityFrameworkCore;

public static class IQueryableExtensions
{
    public static IQueryable<T> IncludeNavigations<T>(this IQueryable<T> query) where T : class
    {
        var entityType = typeof(T);
        var dbSetProperties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string))
            .ToList();

        foreach (var property in dbSetProperties)
        {
            query = query.Include(property.Name);
        }

        return query;
    }
}