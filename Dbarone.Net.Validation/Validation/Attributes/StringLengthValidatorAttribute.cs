namespace Dbarone.Net.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Validator attribute to validate the string length of a member.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class StringLengthValidatorAttribute : ValidatorAttribute
{
    /// <summary>
    /// Minimum length of the string.
    /// </summary>
    public int Min { get; set; }

    /// <summary>
    /// Maximum length of the string.
    /// </summary>
    public int Max { get; set; }

    private string ErrorMessage
    {
        get
        {
            if (Min == 0)
                return "Maximum length of {0} is {1}.";
            else
                return "{0} must be between {2} and {1} characters.";
        }
    }

    /// <summary>
    /// DoValidate method.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="target"></param>
    /// <param name="key"></param>
    /// <param name="results"></param>
    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        // note that string length ONLY runs if value not null.
        // to test for nulls, use NotNull validator as well.
        if (value != null)
        {
            if (value.GetType() != typeof(string))
                results.Add(new ValidationResult { Key = key, Source = source, Message = "StringLengthValidator.DoValidate(): Validator must operate on a string type.", Validator = this });

            if (Max < Min)
                results.Add(new ValidationResult { Key = key, Source = source, Message = "StringLengthValidator.DoValidate(): Max is less than Min.", Validator = this });

            if (Max < 0)
                results.Add(new ValidationResult { Key = key, Source = source, Message = "StringLengthValidator.DoValidate(): Max cannot be negative.", Validator = this });

            if (Min < 0)
                results.Add(new ValidationResult { Key = key, Source = source, Message = "StringLengthValidator.DoValidate(): Min cannot be negative.", Validator = this });

            if (value?.ToString()?.Length < Min || value?.ToString()?.Length > Max)
                results.Add(new ValidationResult { Key = key, Source = source, Message = string.Format(ErrorMessage, key, Max, Min), Validator = this });
        }
    }
}
