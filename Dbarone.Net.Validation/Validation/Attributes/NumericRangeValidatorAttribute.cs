namespace Dbarone.Net.Validation;
using System;
using System.Collections.Generic;
using Dbarone.Net.Extensions.Reflection;

/// <summary>
/// Validator attribute to annotate a member which takes a range of numeric values.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class NumericRangeValidatorAttribute : ValidatorAttribute
{
    public double Min { get; set; }
    public double Max { get; set; }

    public NumericRangeValidatorAttribute(int max)
    {
        this.Max = max;
    }

    public NumericRangeValidatorAttribute(int min, int max)
    {
        this.Min = min;
        this.Max = max;
    }

    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        // note that numeri range ONLY runs if value not null.
        // to test for nulls, use NotNull validator as well.
        if (value != null)
        {
            if (!value.GetType().IsNumeric())
                results.Add(new ValidationResult { Key = key, Source = source, Message = "NumericRangeValidatorAttribute.DoValidate(): NumericRangeValidator must operate on a numeric type.", Validator = this });

            if (Max < Min)
                results.Add(new ValidationResult { Key = key, Source = source, Message = "NumericRangeValidatorAttribute.DoValidate(): Max is less than Min.", Validator = this });

            double v = Convert.ToDouble(value);
            if (v < Min || v > Max)
                results.Add(new ValidationResult { Key = key, Source = source, Message = $"Value of {key} must be between {Min} and {Max}.", Validator = this });
        }
    }
}
