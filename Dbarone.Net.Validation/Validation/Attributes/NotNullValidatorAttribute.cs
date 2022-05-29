namespace Dbarone.Net.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Validator attribute to annotate a non nullable member.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class NotNullValidatorAttribute : ValidatorAttribute
{
    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        if (value == null)
            results.Add(new ValidationResult { Key = key, Source = source, Message = $"{key} cannot be null.", Validator = this });
    }
}
