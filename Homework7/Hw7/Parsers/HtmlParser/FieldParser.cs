using System.Reflection;
using System.Text;

namespace Hw7.Parsers.HtmlParser;

public class FieldParser: IHtmlParser
{
    public string GetHtml(PropertyInfo propertyInfo) =>
        $"<input type=\"{GetInputType(propertyInfo.PropertyType)}\" name=\"{propertyInfo.Name}\" id=\"{propertyInfo.Name}\"/>";
    
    private static string GetInputType(Type type) => type == typeof(string) ? "text" : "number";
}