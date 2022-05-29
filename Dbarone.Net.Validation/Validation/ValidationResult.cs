namespace Dbarone.Net.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represents a validation result (error).
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Key { get; set; } = default!;

    /// <summary>
    /// The validation message.
    /// </summary>
    public string Message { get; set; } = default!;

    /// <summary>
    /// The parent object being validated.
    /// </summary>
    public object Source { get; set; } = default!;

    /// <summary>
    /// The validator creating the result.
    /// </summary>
    public ValidatorAttribute Validator { get; set; } = default!;
}
