namespace Hw9.Extensions;

public static class StringExtensions
{
    public static bool StartsWith(this string value, IEnumerable<string> starts)
    {
        foreach (var start in starts)
            if (value.StartsWith(start))
                return true;

        return false;
    }
    
    public static bool EndsWith(this string value, IEnumerable<string> starts)
    {
        foreach (var start in starts)
            if (value.EndsWith(start))
                return true;

        return false;
    }

    public static string Replace(this string value, IEnumerable<string> oldValues, string newValue)
    {
        var result = value;

        foreach (var oldValue in oldValues)
            result = result.Replace(oldValue, newValue);

        return result;
    }

    public static string WithoutSpaces(this string value) => value.Replace(" ", "");
    
    public static string WithoutBrackets(this string value) => value.Replace(new[] {"(", ")"}, "");
    
    public static string Without(this string value, IEnumerable<string> without) => value.Replace(without, "");
    
    public static string Without(this string value, string without) => value.Replace(without, "");

    public static string[] Split(this string value, IEnumerable<string> separators)
    {
        var result = new List<string>() {value};

        foreach (var separator in separators)
        {
            var preResult = result.Select(x => x.Split(separator)).ToArray();
            result.Clear();
            
            foreach (var separated in preResult)
            foreach (var item in separated)
                result.Add(item);
        }

        return result.ToArray();
    }

    public static bool IsNumber(this string value)
    {
        var isDouble = false;
        var counter = 0;
        
        foreach (var symbol in value)
        {
            if (char.IsDigit(symbol)) continue;

            if (!isDouble && symbol is '.' or ',' && counter+1 != value.Length) isDouble = true;
            else return false;

            counter++;
        }

        return true;
    }
}