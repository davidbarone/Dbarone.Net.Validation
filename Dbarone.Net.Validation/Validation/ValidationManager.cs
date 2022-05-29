namespace Dbarone.Net.Validation;
using System.Collections.Generic;
using System.Linq;
using Dbarone.Net.Extensions.Reflection;

/// <summary>
/// The main validation class providing validation functionality.
/// </summary>
public static class ValidationManager
{
    /// <summary>
    /// Validates an object. Throws an exception if not valid.
    /// </summary>
    /// <param name="target"></param>
    public static void Validate(object? obj)
    {
        var results = PeekValidate(obj);
        if (results != null && results.Any())
            throw new ValidationException(results);
    }

    public static bool IsValid(object? obj) {
        var results = PeekValidate(obj);
        return !(results != null && results.Any());
    }

    /// <summary>
    /// Gets the validations results for an object, but importantly, does not throw any exception if the object is invalid.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IEnumerable<ValidationResult>? PeekValidate(object? obj)
    {
        if (obj == null)
        {
            return null;
        }

        List<ValidationResult> results = new List<ValidationResult>();
        var props = obj.GetPropertiesDecoratedBy<ValidatorAttribute>();

        foreach (var prop in props)
        {
            // get the attribute for the property
            var attributes = (ValidatorAttribute[])prop.GetCustomAttributes(typeof(ValidatorAttribute), false);
            foreach (var attribute in attributes)
            {
                string key = prop.Name;
                attribute.DoValidate(obj.Value(key), obj, key, results);
            }
        }

        // method validators
        var methods = obj.GetMethodsDecoratedBy<MethodValidatorAttribute>();
        foreach (var method in methods)
        {
            // get the attribute for the property
            var attributes = (MethodValidatorAttribute[])method.GetCustomAttributes(typeof(MethodValidatorAttribute), false);
            foreach (var attribute in attributes)
            {
                string key = method.Name;
                attribute.Method = method;
                attribute.DoValidate(null, obj, key, results);
            }
        }

        return results;
    }
}
