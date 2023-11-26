using System.Text.RegularExpressions;

namespace Hw11.Patterns;

public static class Patterns
{
    public static readonly Regex NumPattern = new(@"^\d+");
    public static readonly Regex SmartSplitPattern = new("(?<=[-+*/()])|(?=[-+*/()])");
}