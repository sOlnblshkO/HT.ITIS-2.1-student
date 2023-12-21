namespace Hw9.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Проверяет начинается ли строка с одного из переданных элементов коллекции
    /// </summary>
    /// <param name="value">Строка</param>
    /// <param name="starts">Элементы</param>
    /// <returns>true - если строка начинается с одного из элементов starts, false - иначе</returns>
    public static bool StartsWith(this string value, IEnumerable<string> starts)
    {
        foreach (var start in starts)
            if (value.StartsWith(start))
                return true;

        return false;
    }

    /// <summary>
    /// Проверяет заканчивается ли строка одним из переданных элементов коллекции
    /// </summary>
    /// <param name="value">Строка</param>
    /// <param name="ends">Элементы</param>
    /// <returns>true - если строка заканчивается одноим из элементов ends, false - иначе</returns>
    public static bool EndsWith(this string value, IEnumerable<string> ends)
    {
        foreach (var end in ends)
            if (value.EndsWith(end))
                return true;

        return false;
    }

    /// <summary>
    /// Заменяет все старые значения встречающиеся в строке на новое
    /// </summary>
    /// <param name="value">Строка</param>
    /// <param name="oldValues">Старые элементы</param>
    /// <param name="newValue">Новый элемент</param>
    /// <returns></returns>
    public static string Replace(this string value, IEnumerable<string> oldValues, string newValue)
    {
        var result = value;

        foreach (var oldValue in oldValues)
            result = result.Replace(oldValue, newValue);

        return result;
    }

    
    /// <summary>
    /// Исключает пробелы из строки
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Строка без пробелов</returns>
    public static string WithoutSpaces(this string value) => value.Replace(" ", "");
    
    /// <summary>
    /// Исключает скобки ("(", ")") из строки 
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Строка без скобок</returns>
    public static string WithoutBrackets(this string value) => value.Replace(new[] {"(", ")"}, "");
}