using System.Reflection;
using System.Text;

namespace Hw7.Parsers.HtmlParser;

public class EnumParser: IHtmlParser
{
    public string GetHtml(PropertyInfo propertyInfo)
    {
        var htmlContent = new StringBuilder();

        htmlContent.AppendLine($"<select name=\"{propertyInfo.Name}\" required>");
        foreach (var value in Enum.GetValues(propertyInfo.PropertyType))
        {
            htmlContent.AppendLine($"<option value=\"{value}\">{value}</option>");
        }

        htmlContent.AppendLine("</select>");

        return htmlContent.ToString();
    }
}