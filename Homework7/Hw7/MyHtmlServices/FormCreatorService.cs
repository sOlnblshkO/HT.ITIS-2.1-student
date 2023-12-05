using System.Reflection;
using Hw7.ValidateServices;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public enum FieldType{
    Select,
    InputText,
    InputNumber,
}

public class FormCreatorService(object? model)
{
    public IHtmlContent CreateFormItem(PropertyInfo property)
    {
        var div = new TagBuilder("div");

        var label = CreateLabel(property);
        var field = CreateField(property);
        
        div.InnerHtml.AppendHtml(label);
        div.InnerHtml.AppendHtml(ValidateExtensions.GetFieldErrors(model, property));
        div.InnerHtml.AppendHtml(field);
        
        return div;
    }

    private static IHtmlContent CreateLabel(PropertyInfo property)
    {
        var label = new TagBuilder("label");
        label.Attributes.Add("for", property.Name);
        label.Attributes.Add("name", property.Name);

        var labelText = ValidateExtensions.GetLabelName(property);
        label.InnerHtml.AppendHtml(labelText);
        
        return label;
    }

    private static IHtmlContent CreateField(PropertyInfo property)
    {
        var tagType = ValidateExtensions.GetTegType(property);
        var field = GetFieldByType(property, tagType);
        field.Attributes.Add("id", property.Name);
        field.Attributes.Add("name", property.Name);
        
        return field;
    }

    private static TagBuilder GetFieldByType(PropertyInfo property, FieldType type)
    {
        return type switch
        {
            FieldType.Select => CreateSelect(property),
            FieldType.InputNumber => CreateInputWithType("number"),
            FieldType.InputText => CreateInputWithType("text"),
            _ => default!
        };
    }

    private static TagBuilder CreateSelect(PropertyInfo property)
    {
        var field = new TagBuilder("select");
        
        var enumValues = Enum.GetNames(property.PropertyType);
        
        foreach (var item in enumValues)
        {
            var option = new TagBuilder("option");
            option.Attributes.Add("value", item);
            option.InnerHtml.AppendHtml(item);
            field.InnerHtml.AppendHtml(option);
        }

        return field;
    }

    private static TagBuilder CreateInputWithType(string tagType)
    {
        var field = new TagBuilder("input");
        field.Attributes.Add("type", tagType);
        return field;
    }
        
}