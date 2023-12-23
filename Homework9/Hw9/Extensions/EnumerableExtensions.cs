namespace Hw9.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Убирает without из enumerable
    /// </summary>
    /// <param name="enumerable">Изначальная коллеция</param>
    /// <param name="without">Параметр, который надо исключить из коллекции</param>
    /// <returns>IEnumerable<T> без without параметра</returns>
    public static IEnumerable<T> Without<T>(this IEnumerable<T> enumerable, IEnumerable<T> without)
    {
        var result = enumerable;
        
        foreach (var withoutItem in without)
            result = result.Where(item => item != null && !item.Equals(withoutItem));

        return result;
    }
}