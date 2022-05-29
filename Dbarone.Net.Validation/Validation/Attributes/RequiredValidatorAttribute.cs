namespace Dbarone.Net.Validation;
using System.Collections.Generic;
using Dbarone.Net.Extensions.Reflection;

/// <summary>
/// Validator attribute to annotate a required member.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class RequiredValidatorAttribute : ValidatorAttribute
{
    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        if (value == null || (value != null && value.GetType().Default() == value))
        {
            results.Add(new ValidationResult { Key = key, Source = source, Message = $"{key} is required.", Validator = this });
        }
    }
}
