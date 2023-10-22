using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Hw7.Enums;
using Hw7.Response;

namespace Hw7.MyHtmlServices;


public static class Validator
{
    public static Response<string> ValidateProperty(PropertyInfo propertyInfo, object? entity)
    {
        var response = new Response<string>
        {
            Status = ResultStatus.Ok,
            Data = string.Empty
        };
        
        if (entity != null)
        {
            var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);

            foreach (ValidationAttribute attribute in validationAttributes)
            {
                bool isValid = attribute.IsValid(propertyInfo.GetValue(entity));

                if (!isValid)
                {
                    response.Status = ResultStatus.Error;
                    response.Data = attribute.ErrorMessage!;
                }
            }
        }

        return response;
    }
}