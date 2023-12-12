namespace Hw9.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> Without<T>(this IEnumerable<T> enumerable, T without)
    {
        return enumerable.Where(item => item is not null && !item.Equals(without));
    }
}