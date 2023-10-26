using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

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

    private static IHtmlContent Div(PropertyInfo propertyInfo, object? model)
    {
        var htmlContent = new TagBuilder("div");
        if (!propertyInfo.PropertyType.IsEnum)
        {
            htmlContent.InnerHtml.AppendHtml(GetLabel(propertyInfo));
            htmlContent.InnerHtml.AppendHtml(GetInput(propertyInfo));
        }

        else
        {
            htmlContent.InnerHtml.AppendHtml(GetLabel(propertyInfo));
            htmlContent.InnerHtml.AppendHtml(GetSelect(propertyInfo));
        }

        htmlContent.InnerHtml.AppendHtml(Validate(propertyInfo, model));
        return htmlContent;
    }

    private static IHtmlContent Validate(PropertyInfo propertyInfo, object? model)
    {
        var htmlContent = new TagBuilder("span");
        htmlContent.InnerHtml.AppendHtml(string.Empty);
        if (model == null)
            return htmlContent;

        var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);

        foreach (ValidationAttribute validationAttribute in validationAttributes)
        {
            if (validationAttribute.IsValid(propertyInfo.GetValue(model))) continue;
            htmlContent.InnerHtml.AppendHtml(validationAttribute.ErrorMessage!);
            return htmlContent;
        }

        return htmlContent;
    }

    private static IHtmlContent GetSelect(PropertyInfo propertyInfo)
    {
        var html = new TagBuilder("select");
        var values = propertyInfo.PropertyType.GetEnumValues();
        html.Attributes.Add("id", propertyInfo.Name);
        foreach (var value in values)
            html.InnerHtml.AppendHtml($"<option value=\"{value}\">{value}<option/>");
        return html;
    }

    private static IHtmlContent GetInput(PropertyInfo propertyInfo)
    {
        var htmlContent = new TagBuilder("input");
        htmlContent.Attributes.Add("type", propertyInfo.PropertyType == typeof(string) ? "text" : "number");
        htmlContent.Attributes.Add("id", propertyInfo.Name);
        htmlContent.Attributes.Add("name", propertyInfo.Name);
        return htmlContent;
    }

    private static IHtmlContent GetLabel(PropertyInfo propertyInfo)
    {
        var htmlContent = new TagBuilder("label");
        var display = propertyInfo.GetCustomAttribute(typeof(DisplayAttribute)) != null
            ? propertyInfo.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute
            : null;
        htmlContent.InnerHtml.AppendHtml(display?.Name ?? SeparateName(propertyInfo.Name));
        htmlContent.Attributes.Add("for", propertyInfo.Name);
        return htmlContent;
    }

    private static string SeparateName(string name) =>
        Regex.Replace(name, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
}