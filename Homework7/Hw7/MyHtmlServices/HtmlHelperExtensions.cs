using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var entity = helper.ViewData.Model;
        var properties = helper.ViewData.ModelMetadata.ModelType.GetProperties();

        return new HtmlString(GetHtml(entity, properties));
    }

    private static String GetHtml(object? entity, PropertyInfo[] propertyInfos)
    {
        var htmlString = new StringBuilder();
        foreach (var propertyInfo in propertyInfos)
        {
            var display = HandleDisplay(propertyInfo);
            htmlString.AppendLine("<div>");
            htmlString.AppendLine($"<label for=\"{propertyInfo.Name}\">{display}</label><br>");
            htmlString.AppendLine(propertyInfo.PropertyType.IsEnum
                ? ParseEnum(propertyInfo)
                : ParseNotEnum(propertyInfo));
            var validationResult = ValidateField(entity, propertyInfo);
            if (validationResult != null)
            {
                htmlString.AppendLine($"<span>{validationResult}</span>");
            }

            htmlString.AppendLine("</div><br>");
        }
        return htmlString.ToString();
    }

    private static String ParseEnum(PropertyInfo propertyInfo)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"<select name=\"{propertyInfo.Name}\" required>");
        foreach (var value in Enum.GetValues(propertyInfo.PropertyType))
        {
            sb.AppendLine($"<option value=\"{value}\">{value}</option>");
        }

        sb.AppendLine("</select>");
        
        return sb.ToString();
    }

    private static String ParseNotEnum(PropertyInfo propertyInfo)
    {
        
        string type = propertyInfo.PropertyType == typeof(string) ? "text" : "number";
        return $"<input type=\"{type}\" name=\"{propertyInfo.Name}\" id=\"{propertyInfo.Name}\"/>";
    }

    private static String HandleDisplay(PropertyInfo propertyInfo)
    {
        var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        if (displayAttribute != null)
        {
            return displayAttribute.Name!;
        }
        return Regex.Replace(propertyInfo.Name, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
    }

    private static string? ValidateField(object? entity, PropertyInfo propertyInfo)
    {
        string? resp = null;

        if (entity != null)
        {
            var validationAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
            foreach (var attribute in validationAttributes)
            {
                if (!attribute.IsValid(propertyInfo.GetValue(entity)))
                {
                    resp = attribute.ErrorMessage;
                    break;
                }
                
            }
        }
        
        return resp;
    }
} 