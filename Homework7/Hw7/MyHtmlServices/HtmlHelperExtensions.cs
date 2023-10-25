using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var builder = new HtmlContentBuilder();
        var modelType = helper.ViewData.ModelExplorer.ModelType;
        var model = helper.ViewData.Model;
        foreach (var property in modelType.GetProperties()) _ = builder.AppendHtml(HandleProperty(property, model));

        return builder;
    }

    private static IHtmlContent HandleProperty(PropertyInfo property, object? model)
    {
        var builder = new HtmlContentBuilder();
        _ = builder.AppendHtml("<div>")
            .AppendHtml(CreateLabel(property))
            .AppendHtml(CreateField(property, model))
            .AppendHtml(CreateValidationField(property, model))
            .AppendHtml("</div>");

        return builder;
    }

    private static IHtmlContent CreateLabel(PropertyInfo property)
    {
        foreach (var attribute in property.GetCustomAttributes())
            if (attribute is DisplayAttribute { Name: { } } displayAttribute)
                return CreateLabel(property.Name, displayAttribute.Name);

        return CreateLabel(property.Name, CamelCaseToDisplayName(property.Name));
    }

    private static IHtmlContent CreateLabel(string propertyName, string propertyDisplayName)
    {
        var label = $"<label for={propertyName}>{propertyDisplayName}</label>";
        return new HtmlString(label);
    }

    private static IHtmlContent CreateField(PropertyInfo property, object? model)
    {
        if (property.PropertyType == typeof(string))
            return CreateStringField(property.Name, model == null ? null : property.GetValue(model) as string);
        if (property.PropertyType == typeof(int))
            return CreateIntegerField(property.Name, model == null ? null : property.GetValue(model) as int?);
        if (property.PropertyType.IsEnum)
            return CreateEnumField(property, model);
        return new HtmlString("Unsupported property type");
    }

    private static IHtmlContent CreateStringField(string name, string? value)
    {
        return CreateInputField(name, "text", HttpUtility.HtmlEncode(value) ?? "");
    }

    private static IHtmlContent CreateIntegerField(string name, int? value)
    {
        return CreateInputField(name, "number", (value ?? 0).ToString());
    }

    private static IHtmlContent CreateInputField(string name, string type, string value)
    {
        return new HtmlString(
            $"<input id=\"{name}\" class=\"form-control\" type=\"{type}\" name=\"{name}\" value=\"{value}\"></input>");
    }

    private static IHtmlContent CreateEnumField(PropertyInfo property, object? model)
    {
        var builder = new HtmlContentBuilder();
        _ = builder.AppendHtml($"<select id=\"{property.Name}\" class=\"form-control\" name={property.Name}>");
        foreach (var name in property.PropertyType.GetEnumNames())
        {
            var selected = "";
            if (model != null)
            {
                var value = property.GetValue(model);
                if (property.PropertyType.GetEnumName(value!) == name)
                    selected = "selected";
            }

            _ = builder.AppendHtml($"<option value=\"{name}\" {selected}>{name}</option>");
        }

        _ = builder.AppendHtml("</select>");
        return builder;
    }

    private static IHtmlContent CreateValidationField(PropertyInfo property, object? model)
    {
        var builder = new HtmlContentBuilder();

        _ = builder.AppendHtml("<span>");
        if (model == null) return builder.AppendHtml("</span>");
        var attributes = property.GetCustomAttributes();
        foreach (var attribute in attributes)
        {
            if (attribute is not ValidationAttribute validationAttribute) continue;
            if (validationAttribute.IsValid(property.GetValue(model))) continue;
            _ = builder.Append(validationAttribute.ErrorMessage ?? "");
            break;
        }

        return builder.AppendHtml("</span>");
    }

    private static string CamelCaseToDisplayName(string propertyName)
    {
        return string.Join(' ', Regex.Matches(propertyName, "([A-Z]?[a-z]*)").Select(match => match.Value)).TrimEnd();
    }
}