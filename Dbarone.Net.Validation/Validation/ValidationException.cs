namespace Dbarone.Net.Validation;

/// <summary>
/// Custom error type thrown whenever a validation error occurs.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// List of validation results.
    /// </summary>
    public IEnumerable<ValidationResult> Results { get; set; }

    /// <summary>
    /// The exception constructor.
    /// </summary>
    /// <param name="results"></param>
    public ValidationException(IEnumerable<ValidationResult> results) : base("Object validation has failed. Please refer to validation results for more information.")
    {
        this.Results = results;
    }
}