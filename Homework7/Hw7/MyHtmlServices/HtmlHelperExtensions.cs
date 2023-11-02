using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel<TModel>(this IHtmlHelper<TModel> helper)
    {
        var builder = new HtmlContentBuilder();
        var modelType = helper.ViewData.ModelExplorer.ModelType;
        var model = helper.ViewData.Model;
        
        foreach (var property in modelType.GetProperties()) 
            _ = builder.AppendHtml(HandleProperty(property, model));

        return builder;
    }

    private static IHtmlContent HandleProperty<TModel>(PropertyInfo property, TModel? model)
    {
        return new HtmlContentBuilder()
            .AppendHtml("<div>")
            .AppendHtml(CreateLabel(property))
            .AppendHtml(CreateField(property, model))
            .AppendHtml(CreateValidationMessage(property, model))
            .AppendHtml("</div>");
    }

    private static IHtmlContent CreateLabel(PropertyInfo property)
    {
        var labelName = CamelCaseToDisplayName(property.Name);
        foreach (var attribute in property.GetCustomAttributes())
            if (attribute is DisplayAttribute { Name: { } } displayAttribute)
                labelName = displayAttribute.Name;
        
        var label = $"<label for={property.Name}>{labelName}</label>";

        return new HtmlString(label);
    }

    private static IHtmlContent CreateField<TModel>(PropertyInfo property, TModel? model)
    {
        if (property.PropertyType == typeof(string))
            return CreateInputField(property.Name, "text", HttpUtility.HtmlEncode(model == null ? null : property.GetValue(model)) ?? "");
        if (property.PropertyType == typeof(int))
            return CreateInputField(property.Name, "number", ((model == null ? null : property.GetValue(model)) ?? 0).ToString()!);
        //if (property.PropertyType.IsEnum)
            return CreateEnumField(property, model);

        // return new HtmlString("Unsupported property type");
    }
    
    private static IHtmlContent CreateValidationMessage<TModel>(PropertyInfo property, TModel? model)
    {
        var builder = new HtmlContentBuilder();

        _ = builder.AppendHtml("<span>");
        if (model == null) return builder.AppendHtml("</span>");
        var attributes = property.GetCustomAttributes<ValidationAttribute>();
        foreach (var validationAttribute in attributes)
        {
            if (validationAttribute.IsValid(property.GetValue(model))) continue;
            _ = builder.Append(validationAttribute.ErrorMessage);
            break;
        }

        return builder.AppendHtml("</span>");
    }

    private static IHtmlContent CreateInputField(string name, string type, string value)
    {
        return new HtmlString(
            $"<input id=\"{name}\" class=\"form-control\" type=\"{type}\" name=\"{name}\" value=\"{value}\"></input>");
    }

    private static IHtmlContent CreateEnumField<TModel>(PropertyInfo property, TModel? model)
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

    private static string CamelCaseToDisplayName(string propertyName)
    {
        return string.Join(' ', Regex.Matches(propertyName, "([A-Z]?[a-z]*)").Select(match => match.Value)).TrimEnd();
    }
} 