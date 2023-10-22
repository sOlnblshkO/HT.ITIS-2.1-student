using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Hw7.Enums;
using Hw7.Parsers.HtmlParser;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    private static IHtmlParser _htmlParser;
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var entity = helper.ViewData.Model;
        var properties = helper.ViewData.ModelMetadata.ModelType.GetProperties();

        return new HtmlString(GetHtmlContent(properties, entity));
    }

    private static string GetHtmlContent(PropertyInfo[] properties, object? entity)
    {
        var htmlContent = new StringBuilder();
        foreach (var property in properties)
        {
            var display = GetFieldDisplay(property);
            var propertyType = property.PropertyType;

            htmlContent.AppendLine("<div>");
            htmlContent.AppendLine($"<label for=\"{property.Name}\">{display}</label><br>");

            if (propertyType.IsEnum)
                _htmlParser = new EnumParser();
            else
                _htmlParser = new FieldParser();
            
            htmlContent.AppendLine(_htmlParser.GetHtml(property));

            var validationResult = Validator.ValidateProperty(property, entity);
            
            if (validationResult.Status == ResultStatus.Error)
            {
                htmlContent.AppendLine($"<span>{validationResult.Data}</span>");
            }
            
            htmlContent.AppendLine("</div><br><br>");
        }

        return htmlContent.ToString();
    }

    private static string GetHtmlForField(PropertyInfo property)
    {
        var htmlContent = new StringBuilder();

        htmlContent.AppendLine(
            $"<input type=\"{GetInputType(property.PropertyType)}\" name=\"{property.Name}\" id=\"{property.Name}\"/>");

        return htmlContent.ToString();
    }

    private static string GetHtmlForEnum(PropertyInfo property)
    {
        var htmlContent = new StringBuilder();

        htmlContent.AppendLine($"<select name=\"{property.Name}\" required>");
        foreach (var value in Enum.GetValues(property.PropertyType))
        {
            htmlContent.AppendLine($"<option value=\"{value}\">{value}</option>");
        }

        htmlContent.AppendLine("</select>");

        return htmlContent.ToString();
    }
    
    private static string GetFieldDisplay(PropertyInfo property)
    {
        if (Attribute.GetCustomAttribute(property, typeof(DisplayAttribute)) is not DisplayAttribute displayAttribute)
        {
            //Regexp разделяет CamelCase по большим буквам 
            return string.Join(' ', Regex.Split(property.Name, "(?<!^)(?=[A-Z])"));
        }

        return displayAttribute.Name ?? string.Empty;
    }
    
    private static string GetInputType(Type type) => type == typeof(string) ? "text" : "number";
}