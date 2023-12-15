using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    /// <summary>
    /// Метод, возвращающий форму, опираясь на переданную модель
    /// </summary>
    /// <param name="helper"></param>
    /// <typeparam name="TModel">Модель для создания формы под неё</typeparam>
    /// <returns>IHtmlContent</returns>
    public static IHtmlContent MyEditorForModel<TModel>(this IHtmlHelper<TModel> helper)
    {
        var model = helper.ViewData.Model;
        var modelProperties = model?.GetType().GetProperties() ?? Array.Empty<PropertyInfo>();
        
        var htmlContent = new HtmlContentBuilder();
        var formCreator = new FormCreatorService(model);
        
        foreach (var property in modelProperties)
          htmlContent.AppendHtml(formCreator.CreateFormItem(property));
        
        return htmlContent;
    }
} 