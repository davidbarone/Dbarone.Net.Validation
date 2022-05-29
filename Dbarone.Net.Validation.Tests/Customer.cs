namespace Dbarone.Net.Validation.Tests;
using Dbarone.Net.Validation;
using System.Collections.Generic;

/// <summary>
/// Test entity class to run validation tests on.
/// </summary>
public class Customer
{
    public int Id { get; set; }

    [RequiredValidator]
    [StringLengthValidator(Min = 1, Max = 6)]
    public string CustomerCode { get; set; } = default!;

    [RequiredValidator]
    public string CustomerName { get; set; } = default!;

    [RequiredValidator]
    [RegExValidator(Pattern = "(A|B|C)")]
    public string Rating { get; set; } = default!;

    [NumericRangeValidator(Min = 0, Max = 1000)]
    public int CreditLevel { get; set; } = default!;

    [MethodValidator]
    public void CheckCustomer(IList<ValidationResult> results, ValidatorAttribute validator)
    {
        // Customers with rating 'C' cannot have a credit level
        if (this.Rating == "C" && this.CreditLevel > 0)
        {
            results.Add(new ValidationResult
            {
                Key = "CheckCustomer",
                Source = this,
                Message = "Customers with rating of 'C' cannot have a credit level.",
                Validator = validator
            });
        }
    }
}
