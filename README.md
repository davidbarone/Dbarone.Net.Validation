# Dbarone.Net.Validation
A .NET object validation library using attribute annotations.

Validating objects is a common task in enterprise applications. This library uses attributes to annotate a class to describe a set of validation rules. Objects can then be validated, and any validation results obtained. Validation attributes are generally places on properties of a class, with the exception of the `MethodValidatorAttribute` which is placed on methods.

## Validation Attributes
A number of validation attributes have been included for common validation tasks:

| Validator               | Description                                                              |
| ----------------------- | ------------------------------------------------------------------------ |
| NotNullValidator        | Validates whether a property has a null value                            |
| NumericRanteValidator   | Validates that a numeric property is within a range of values            |
| RegExValidatorAttribute | Validates that a string property matches a RegEx expression              |
| RequiredValidator       | Validates that a property has a value. Checks for empty strings          |
| StringLengthValidator   | Validates that a string property has a length between min and max values |
| MethodValidator         | Provides a mechanism to add a custom validation method                   |
| ObjectValidator         | Applies validation on a child object                                     |

## ValidationManager
The `ValidationManager` class provides the core validation functions:

| Method       | Purpose                                                                                   |
| ------------ | ----------------------------------------------------------------------------------------- |
| Validate     | Validates an object. Throws a `ValidationException` error if validation fails             |
| PeekValidate | Does not throw an error, but returns the validation results, which can then be inspected. |
| IsValid      | Returns true if the object passes validation.                                             |

## Example code

``` C#
using Dbarone.Net.Validation;

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
}

public class CustomerService
{
    public void AddCustomer(Customer customer)
    {
        if ValidationManager.IsValid(customer) {
            // OK - persist customer to database etc.
            ...
            ...
        }
    }
}
```