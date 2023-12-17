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

public class FormCreatorService<TModel>
{
    private readonly TModel _model;
    
    public FormCreatorService(TModel model) => _model = model;

    /// <summary>
    /// Метод, возвращающий поле для формы, опираясь на свойство модели
    /// </summary>
    /// <returns>IHtmlContent</returns>
    public IHtmlContent CreateFormItem(PropertyInfo property)
    {
        var div = new TagBuilder("div");

        var label = CreateLabel(property);
        var field = CreateField(property);
        
        div.InnerHtml.AppendHtml(label);
        div.InnerHtml.AppendHtml(ValidateExtensions.GetFieldErrors(_model, property));
        div.InnerHtml.AppendHtml(field);
        
        return div;
    }

    /// <summary>
    /// Метод, возвращающий Label для поля формы
    /// </summary>
    /// <returns>IHtmlContent</returns>
    private static IHtmlContent CreateLabel(PropertyInfo property)
    {
        var label = new TagBuilder("label");
        label.Attributes.Add("for", property.Name);
        label.Attributes.Add("name", property.Name);

        var labelText = ValidateExtensions.GetLabelName(property);
        label.InnerHtml.AppendHtml(labelText);
        
        return label;
    }

    /// <summary>
    /// Метод, возвращающий поле формы
    /// </summary>
    /// <returns>IHtmlContent</returns>
    private static IHtmlContent CreateField(PropertyInfo property)
    {
        var tagType = ValidateExtensions.GetTagType(property);
        var field = GetFieldByType(property, tagType);
        field.Attributes.Add("id", property.Name);
        field.Attributes.Add("name", property.Name);
        
        return field;
    }

    /// <summary>
    /// Метод, возвращающий тег, опираясю на тип поля(FieldType) 
    /// </summary>
    /// <returns>TagBuilder</returns>
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

    /// <summary>
    /// Метод, возвращающий тег select
    /// </summary>
    /// <returns>TagBuilder</returns>
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

    /// <summary>
    /// Метод, возвращающий тег input с определённым типом
    /// </summary>
    /// <returns>TagBuilder</returns>
    private static TagBuilder CreateInputWithType(string tagType)
    {
        var field = new TagBuilder("input");
        field.Attributes.Add("type", tagType);
        return field;
    }
        
}