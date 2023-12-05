using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Hw7.MyHtmlServices;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.ValidateServices;

public static class ValidateExtensions
{
    public static FieldType GetTegType(PropertyInfo property)
    {
        if (property.PropertyType.IsEnum) return FieldType.Select;
        if (property.PropertyType.IsValueType) return FieldType.InputNumber;
        return FieldType.InputText;
    }

    public static string GetLabelName(PropertyInfo property)
    {
        var displayAttribute = property.
            GetCustomAttributes(true).
            FirstOrDefault(attr => attr is DisplayAttribute) as DisplayAttribute;
        
        if (!string.IsNullOrEmpty(displayAttribute?.Name))
            return displayAttribute.Name;

        return CamelCaseToLabelName(property);
    }

    private static string CamelCaseToLabelName(PropertyInfo property)
    {
        var words = Regex.Split(property.Name ,"(?<!^)(?=[A-Z])");
        return string.Join(" ", words);
    }

    public static IHtmlContent GetFieldErrors(object? model, PropertyInfo property)
    {
        var attributes = property.
            GetCustomAttributes(true).
            OfType<ValidationAttribute>().ToList();

        var errorSpan = new TagBuilder("span");
        
        foreach (var attr in attributes)
        {
            if (attr.IsValid(property.GetValue(model))) continue;
            
            errorSpan.InnerHtml.Append(attr.ErrorMessage ?? "Error");
            break;
        }

        return errorSpan;
    }
}