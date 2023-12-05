using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel<TModel>(this IHtmlHelper<TModel> helper)
    {
        var model = helper.ViewData.Model;
        var modelProperties = model?
                                  .GetType()
                                  .GetProperties(BindingFlags.Public | BindingFlags.Instance) 
                              ?? Array.Empty<PropertyInfo>();
        
        var htmlContent = new HtmlContentBuilder();
        var formCreator = new FormCreatorService(model);

        foreach (var property in modelProperties)
          htmlContent.AppendHtml(formCreator.CreateFormItem(property));
        
        return htmlContent;
    }
    
    
} 