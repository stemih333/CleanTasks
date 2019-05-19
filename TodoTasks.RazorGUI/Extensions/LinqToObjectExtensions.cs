using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoTasks.RazorGUI.Extensions
{
    public static class LinqToObjectExtensions
    {
        public static IEnumerable<T> OrderByString<T>(this IEnumerable<T> src, string propertyName, string sortOrder = "asc")
        {
            var type = typeof(T);
            var prop = type.GetProperty(propertyName);
            if (prop == null) throw new NullReferenceException($"Property '{propertyName}' does not exist on type: {type.Name}");
            return (sortOrder.Equals("asc"))
                ? src.OrderBy(_ => prop.GetValue(_))
                : src.OrderByDescending(_ => prop.GetValue(_));
        }
    }
}
