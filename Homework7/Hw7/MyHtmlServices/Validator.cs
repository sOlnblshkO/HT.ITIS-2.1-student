using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Hw7.MyHtmlServices;

public static class Validator
{
    public static void PropertyValidate(PropertyInfo propertyInfo, object? entity, out string message)
    {
        if (entity == null)
        {
            message = string.Empty;
            return;
        }

        var validationAttributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);

        foreach (ValidationAttribute attribute in validationAttributes)
        {
            bool isValid = attribute.IsValid(propertyInfo.GetValue(entity));

            if (!isValid)
            {
                message = attribute.ErrorMessage!;
                return;
            }
        }

        message = string.Empty;
    }
}