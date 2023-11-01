using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel<TModel>(this IHtmlHelper<TModel?> helper)
    {
        var typeModel = helper.ViewData.ModelExplorer.ModelType;
        var model = helper.ViewData.Model;
        var htmlContent = new HtmlContentBuilder();

        foreach (var property in typeModel.GetProperties())
        {
            htmlContent.AppendHtml(Div(property, model!));
        }

        return htmlContent;
    }

    private static IHtmlContent Div(PropertyInfo property, object? model)
    {
        var html = new TagBuilder("div");
        html.InnerHtml.AppendHtml(GetLabel(property));
        var label = GetLabel(property);
        if (!property.PropertyType.IsEnum)
        {
            html.InnerHtml.AppendHtml(GetInput(property));
        }
        else
        {
            html.InnerHtml.AppendHtml(GetSelect(property));
        }
        html.InnerHtml.AppendHtml(Validate(property, model));
        return html;
    }
    private static IHtmlContent GetLabel(PropertyInfo property)
    {
        var label = new TagBuilder("label");
        var display = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
        label.InnerHtml.AppendHtml(display?.Name ?? SeparateName(property.Name));
        label.Attributes.Add("for", property.Name);

        return label;
    }

    private static IHtmlContent GetInput(PropertyInfo property)
    {
        var input = new TagBuilder("input");
        input.Attributes.Add("type", property.PropertyType == typeof(string) ? "text" : "number");
        input.Attributes.Add("id", property.Name);
        input.Attributes.Add("name", property.Name);
        return input;
    }


    private static IHtmlContent GetSelect(PropertyInfo property)
    {
        var select = new TagBuilder("select");
        var values = property.PropertyType.GetEnumValues();
        select.Attributes.Add("id", property.Name);
        foreach (var value in values)
            select.InnerHtml.AppendHtml($"<option value=\"{value}\">{value}<option/>");

        return select;
    }
    private static IHtmlContent Validate(PropertyInfo propertyInfo, object? model)
    {
        
        if (model == null)
            return new HtmlString(String.Empty);
        
        var htmlContent = new TagBuilder("span");
        var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);

        foreach (ValidationAttribute validationAttribute in validationAttributes)
        {
            if (!validationAttribute.IsValid(propertyInfo.GetValue(model)))
                htmlContent.InnerHtml.AppendHtml(validationAttribute.ErrorMessage!);
        }

        return htmlContent;
    }
    private static string SeparateName(string name) =>
        Regex.Replace(name, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
    
} 