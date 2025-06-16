using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenByDynamic<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescendingDynamic<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression body = param;

            foreach (var member in property.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            var selector = Expression.Lambda(body, param);
            var result = typeof(Queryable).GetMethods()
                .First(method => method.Name == methodName
                                 && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), body.Type)
                .Invoke(null, new object[] { source, selector });

            return (IOrderedQueryable<T>)result!;
        }
    }
}