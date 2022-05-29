namespace Dbarone.Net.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

/// <summary>
/// Validator attribute to annotate a method which can provide a custom validation.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class MethodValidatorAttribute : ValidatorAttribute
{
    public MethodInfo Method { get; set; } = default!;

    public override void DoValidate(object? value, object source, string key, IList<ValidationResult> results)
    {
        if (Method == null)
            throw new ArgumentNullException("MethodValidatorAttribute.DoValidate(): Method is null.");
        if (Method.ReturnType != typeof(void))
            throw new ArgumentException("MethodValidatorAttribute.DoValidate(): Method validator has invalid signature (1).");
        ParameterInfo[] parameters = Method.GetParameters();

        if (parameters.Length == 2 && parameters[0].ParameterType == typeof(IList<ValidationResult>) && parameters[1].ParameterType == typeof(ValidatorAttribute))
        {
            Method.Invoke(source, new object[] { results, this });
        }
        else
        {
            throw new ArgumentException("MethodValidatorAttribute.DoValidate(): Method validator has invalid signature (2).");
        }
    }
}