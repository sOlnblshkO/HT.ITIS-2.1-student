using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel<TModel>(this IHtmlHelper<TModel> helper)
    {
        var content = new HtmlContentBuilder();
        var model = helper.ViewData.Model;

        foreach (var propertyInfo in helper.ViewData.ModelExplorer.ModelType.GetProperties())
            content.AppendHtml(GetHtmlSectionForProperty(propertyInfo, model));
        
        return content;
    }

    private static IHtmlContent GetHtmlSectionForProperty<TModel>(PropertyInfo propertyInfo, TModel model)
    {
        var propertyType = propertyInfo.PropertyType;
        var resultContent = new TagBuilder("div");
        
        //htmlContent.AppendLine($"<label for=\"{propertyInfo.Name}\">{display}</label><br>");
        resultContent.InnerHtml.AppendHtml(GetLabelForProperty(propertyInfo));


        resultContent.InnerHtml.AppendHtml(propertyType.IsEnum
            ? GetSelectorForProperty(propertyInfo)
            : GetInputFieldForProperty(propertyInfo));

        resultContent.InnerHtml.AppendHtml(Validate(propertyInfo, model));

        return resultContent;
    }

    private static IHtmlContent GetInputFieldForProperty(PropertyInfo propertyInfo)
    {
        var resultContent = new TagBuilder("input");
        resultContent.Attributes.Add("class", propertyInfo.Name);
        resultContent.Attributes.Add("id", propertyInfo.Name);
        resultContent.Attributes.Add("type", propertyInfo.PropertyType == typeof(string) 
            ? "text" 
            : "number");
        return resultContent;
    }

    private static IHtmlContent GetSelectorForProperty(PropertyInfo propertyInfo)
    {
        var resultContent = new TagBuilder("select");
        var values = propertyInfo.PropertyType.GetEnumValues();
        
        resultContent.Attributes.Add("id",propertyInfo.Name);
        
        foreach (var value in values)
        {
            resultContent.InnerHtml.AppendHtml($"<option value = \"{value}\">{value}<option/>");
        }

        return resultContent;
    }

    private static IHtmlContent GetLabelForProperty(PropertyInfo propertyInfo)
    {
        var resultContent = new TagBuilder("label");
        resultContent.Attributes.Add("for", propertyInfo.Name);
        resultContent.InnerHtml.AppendHtml(GetFieldName(propertyInfo));
        return resultContent;
    }

    private static string GetFieldName(PropertyInfo propertyInfo)
    {
        return (DisplayAttribute?)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayAttribute)) == null
            ? string.Join(' ', Regex.Split(propertyInfo.Name, @"(?<!^)(?=[A-Z])"))
            : ((DisplayAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayAttribute))!).Name;
    }

    private static IHtmlContent Validate<TModel>(PropertyInfo propertyInfo, TModel model)
    {
        var resultContent = new TagBuilder("span");
        if (model == null)
            return resultContent;
        
        var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);
        
        foreach (ValidationAttribute attribute in validationAttributes)
            if (!attribute.IsValid(propertyInfo.GetValue(model)))
                resultContent.InnerHtml.AppendHtml(attribute.ErrorMessage!);

        return resultContent;
    }
} 