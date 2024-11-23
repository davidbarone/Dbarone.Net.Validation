namespace Dbarone.Net.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;


/// <summary>
/// Validator attribute that uses a Regex expression to validate a member.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class RegExValidatorAttribute : ValidatorAttribute
{
    public string Pattern { get; set; } = default!;

    public RegExValidatorAttribute(string pattern)
    {
        this.Pattern = pattern;
    }

    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        Regex ex = new Regex(Pattern);

        if (value != null)
        {
            if (value.GetType() != typeof(string))
                results.Add(new ValidationResult { Key = key, Source = source, Message = "RegExValidatorAttribute.DoValidate(): Validator must operate on a string type.", Validator = this });

            if (!ex.IsMatch(value.ToString() ?? ""))
                results.Add(new ValidationResult { Key = key, Source = source, Message = $"{key} has an invalid value.", Validator = this });
        }
    }
}
