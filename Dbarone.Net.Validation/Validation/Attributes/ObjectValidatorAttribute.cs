namespace Dbarone.Net.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Validator attribute to annotate a child member.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ObjectValidatorAttribute : ValidatorAttribute
{
    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        if (value != null)
        {
            var childResults = ValidationManager.PeekValidate(value);
            if (childResults != null)
            {
                foreach (var childResult in childResults)
                {
                    childResult.Key = key + "." + childResult.Key;
                    childResult.Source = value;
                    results.Add(childResult);
                }
            }
        }
    }
}

