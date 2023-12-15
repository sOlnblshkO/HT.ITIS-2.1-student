namespace Hw9.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Убирает without из enumerable
    /// </summary>
    /// <param name="enumerable">Изначальная коллеция</param>
    /// <param name="without">Параметр, который надо исключить из коллекции</param>
    /// <returns>IEnumerable<T> без without параметра</returns>
    public static IEnumerable<T> Without<T>(this IEnumerable<T> enumerable, T without)
    {
        return enumerable.Where(item => item is not null && !item.Equals(without));
    }
}