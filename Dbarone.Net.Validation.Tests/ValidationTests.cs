namespace Dbarone.Net.Validation.Tests;
using Xunit;
using System.Linq;

public class ValidationTests
{
    public Customer GetValidCustomer() => new Customer()
    {
        CustomerCode = "ABCDEF",
        CustomerName = "Foobar Customer",
        Rating = "A",
        CreditLevel = 0
    };

    [Fact]
    public void WhenValidate_ValidCustomer_ShouldBeValid()
    {
        // Arrange
        var customer = GetValidCustomer();

        // Act
        var isValid = ValidationManager.IsValid(customer);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void WhenValidate_CustomerWithStringLengthError_ShouldCreateStringLengthValidationResult()
    {
        // Arrange
        var customer = GetValidCustomer(); // Good customer

        // Act
        customer.CustomerCode = "ABCDEFG";  // Invalid value
        var results = ValidationManager.PeekValidate(customer);
        var isValid = ValidationManager.IsValid(customer);

        // Assert
        Assert.False(isValid);
        Assert.Single(results!);
        Assert.Equal(typeof(StringLengthValidatorAttribute), results!.First().Validator.GetType());

    }

    [Fact]
    public void WhenValidate_CustomerWithNumericRangeError_ShouldCreateNumericRangeValidationResult()
    {
        // Arrange
        var customer = GetValidCustomer();  // Good customer

        // Act
        customer.CreditLevel = -999;        // Invalid value
        var results = ValidationManager.PeekValidate(customer);
        var isValid = ValidationManager.IsValid(customer);

        // Assert
        Assert.False(isValid);
        Assert.Single(results!);
        Assert.Equal(typeof(NumericRangeValidatorAttribute), results!.First().Validator.GetType());
    }

    [Fact]
    public void WhenValidate_CustomerWithRegexError_ShouldCreateRegexValidationResult()
    {
        // Arrange
        var customer = GetValidCustomer();  // Good customer

        // Act
        customer.Rating = "D";        // Invalid value
        var results = ValidationManager.PeekValidate(customer);
        var isValid = ValidationManager.IsValid(customer);

        // Assert
        Assert.False(isValid);
        Assert.Single(results!);
        Assert.Equal(typeof(RegExValidatorAttribute), results!.First().Validator.GetType());
    }

    [Fact]
    public void WhenValidate_CustomerWithMethodError_ShouldCreateMethodValidationResult()
    {
        // Arrange
        var customer = GetValidCustomer();  // Good customer

        // Act
        customer.Rating = "C";          // Invalid combination of values
        customer.CreditLevel = 100;     // Invalid combination of values
        var results = ValidationManager.PeekValidate(customer);
        var isValid = ValidationManager.IsValid(customer);

        // Assert
        Assert.False(isValid);
        Assert.Single(results!);
        Assert.Equal(typeof(MethodValidatorAttribute), results!.First().Validator.GetType());
    }
}