using System.Reflection;

namespace Hw7.Parsers.HtmlParser;

public interface IHtmlParser
{
    public string GetHtml(PropertyInfo propertyInfo);
}