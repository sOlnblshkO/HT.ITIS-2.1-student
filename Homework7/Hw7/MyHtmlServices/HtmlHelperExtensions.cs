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

        return GetHtmlString(entity, properties);
    }

    private static HtmlString GetHtmlString(object? entity, PropertyInfo[] propertyInfos)
    {
        var htmlString = new StringBuilder();
        foreach (var propertyInfo in propertyInfos)
        {
            var display = GetDisplayName(propertyInfo);
            htmlString.AppendLine("<div>");
            htmlString.AppendLine($"<label for=\"{propertyInfo.Name}\">{display}</label><br>");
            htmlString.AppendLine(ParseField(propertyInfo));
            var validationResult = ValidateField(entity, propertyInfo);
            if (!validationResult.IsSuccess)
            {
                htmlString.AppendLine($"<span>{validationResult.Message}</span>");
            }

            htmlString.AppendLine("</div><br>");
        }

        return new HtmlString(htmlString.ToString());
    }

    private static string ParseField(PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType.IsEnum) return ParseEnum(propertyInfo);
        
        string type = propertyInfo.PropertyType == typeof(string) ? "text" : "number";
        return $"<input type=\"{type}\" name=\"{propertyInfo.Name}\" id=\"{propertyInfo.Name}\"/>";
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
    

    private static String GetDisplayName(PropertyInfo propertyInfo)
    {
        var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        if (displayAttribute != null)
        {
            return displayAttribute.Name!;
        }
        return Regex.Replace(propertyInfo.Name, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
    }

    private static Result ValidateField(object? entity, PropertyInfo propertyInfo)
    {
        var result = new Result{IsSuccess = true};
        if (entity != null)
        {
            var validationAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
            foreach (var attribute in validationAttributes)
            {
                if (!attribute.IsValid(propertyInfo.GetValue(entity)))
                {
                    result.Message = attribute.ErrorMessage;
                    result.IsSuccess = false;
                    break;
                }
                
            }
        }
        return result;
    }

    private class Result
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
} 