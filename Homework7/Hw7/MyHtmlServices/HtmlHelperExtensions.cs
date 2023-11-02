using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var model = helper.ViewData.Model;
        var type = helper.ViewData.ModelMetadata.ModelType;
        var htmlContent = new TagBuilder("form");
        foreach (var property in type.GetProperties())
            htmlContent.InnerHtml.AppendHtml(CreateFormGroup(property, model)); 
        return htmlContent.InnerHtml;
    }

    private static IHtmlContent CreateFormGroup(PropertyInfo property, object? model)
    {
        var formGroup = new TagBuilder("div");
        formGroup.MergeAttribute("class", "form-group");
        formGroup.InnerHtml.AppendHtml(CreateLabel(property));
        formGroup.InnerHtml.AppendHtml(property.PropertyType.IsEnum
            ? CreateSelect(property)
            : CreateInput(property));
        formGroup.InnerHtml.AppendHtml(CreateSpan(property, model));
        return formGroup;
    }
    
    private static IHtmlContent CreateLabel(PropertyInfo property)
    {
        var label = new TagBuilder("label");
        label.MergeAttribute("for", property.Name);
        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
        label.InnerHtml.AppendHtml(displayAttribute?.Name ?? SplitCamelCase(property.Name));
        return label;
    }

    private static IHtmlContent CreateInput(PropertyInfo property)
    {
        var input = new TagBuilder("input");
        input.MergeAttribute("type", property.PropertyType == typeof(string) ? "text" : "number");
        input.MergeAttribute("name", property.Name);
        input.MergeAttribute("id", property.Name);
        return input;
    }

    private static IHtmlContent CreateSelect(PropertyInfo property)
    {
        var select = new TagBuilder("select");
        select.MergeAttribute("id", property.Name);
        var values = property.PropertyType.GetEnumValues();
        foreach (var val in values)
            select.InnerHtml.AppendHtml($"<option value=\"{val}\">{val}<option/>");
        return select;
    }

    private static IHtmlContent CreateSpan(PropertyInfo property, object? model)
    {
        var span = new TagBuilder("span");
        if (model == null)
            return span;
        foreach (var validationAttribute in property.GetCustomAttributes<ValidationAttribute>())
            if (!validationAttribute.IsValid(property.GetValue(model)))
                span.InnerHtml.AppendFormat(validationAttribute.ErrorMessage!);
        return span;
    }

    private static string SplitCamelCase(string input)
    {
        return Regex.Replace(input,"([A-Z])", " $1", RegexOptions.Compiled).Trim();
    }
} 