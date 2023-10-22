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

            htmlContent.AppendLine("<div>");
            htmlContent.AppendLine($"<label for=\"{property.Name}\">{display}</label><br>");

            htmlContent.AppendLine(GetHtmlContent(property));

            var validationResult = Validator.ValidateProperty(property, entity);
            
            if (validationResult.Status == ResultStatus.Error)
            {
                htmlContent.AppendLine($"<span>{validationResult.Data}</span>");
            }
            
            htmlContent.AppendLine("</div><br><br>");
        }

        return htmlContent.ToString();
    }

    private static string GetHtmlContent(PropertyInfo property)
    {
        IHtmlParser? htmlParser;
        
        if (property.PropertyType.IsEnum)
            htmlParser = new EnumParser();
        else
            htmlParser = new FieldParser();

        return htmlParser.GetHtml(property);
    }
    
    private static string GetFieldDisplay(PropertyInfo property)
    {
        if (Attribute.GetCustomAttribute(property, typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
        {
            return displayAttribute.Name!;
        }
        
        //Regexp разделяет CamelCase по большим буквам 
        return string.Join(' ', Regex.Split(property.Name, "(?<!^)(?=[A-Z])"));
    }
}