using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Hw7.MyHtmlServices;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.ValidateServices;

public static class ValidateExtensions
{
    /// <summary>
    /// Метод, возвращающий тип поля формы (input/select)
    /// </summary>
    /// <returns>FieldType</returns>
    public static FieldType GetTagType(PropertyInfo property)
    {
        if (property.PropertyType.IsEnum) return FieldType.Select;
        if (property.PropertyType.IsValueType) return FieldType.InputNumber;
        return FieldType.InputText;
    }
    
    /// <summary>
    /// Метод, возвращающий имя Label для поля формы
    /// </summary>
    /// <returns>string</returns>
    public static string GetLabelName(PropertyInfo property)
    {
        var displayAttribute = property.GetCustomAttributes(true).
            OfType<DisplayAttribute>().
            FirstOrDefault();
        
        if (!string.IsNullOrEmpty(displayAttribute?.Name))
            return displayAttribute.Name;

        return CamelCaseToLabelName(property);
    }

    /// <summary>
    /// Метод, возвращающий имя Label для поля формы,
    /// если вдруг у свойства нет атрибута Display
    /// </summary>
    /// <returns>string</returns>
    private static string CamelCaseToLabelName(PropertyInfo property)
    {
        var words = Regex.Split(property.Name ,"(?<!^)(?=[A-Z])");
        return string.Join(" ", words);
    }

    /// <summary>
    /// Возвращает span с ошибками, которые были допущены
    /// пользователем при заполнении полей, опираясь на Validation атрибуты
    /// </summary>
    /// <returns>IHtmlContent</returns>
    public static IHtmlContent GetFieldErrors<TModel>(TModel model, PropertyInfo property)
    {
        var attributes = property.
            GetCustomAttributes(true).
            OfType<ValidationAttribute>();

        var errorSpan = new TagBuilder("span");
        
        foreach (var attr in attributes)
        {
            if (attr.IsValid(property.GetValue(model))) continue;
            
            errorSpan.InnerHtml.Append(attr.ErrorMessage ?? "Error");
        }

        return errorSpan;
    }
}