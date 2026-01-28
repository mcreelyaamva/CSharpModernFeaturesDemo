#pragma warning disable CS8618
namespace CSharp11Features;

// ============================================================================
// DEMO 2: Required Members
// ============================================================================
// The 'required' modifier ensures that certain properties must be set
// during object initialization. Great for DTOs and API contracts!
// ============================================================================

public static class Demo02_Required_Members
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 2: Required Members");

        #region The Problem - Forgetting to Set Properties

        // Before C# 11, you had to rely on constructors or hope developers
        // remembered to set all necessary properties
        var oldRequest = new DriverLicenseRequestOld
        {
            LicenseNumber = "D1234567",
            // Oops! Forgot IssuingState and DateOfBirth - compiles fine but fails at runtime
        };

        Console.WriteLine("OLD WAY - Missing properties compile but fail at runtime:");
        Console.WriteLine($"  LicenseNumber: {oldRequest.LicenseNumber}");
        //Console.WriteLine($"  IssuingState: {oldRequest.IssuingState.ToUpper()}"); // runtime NullReferenceException
        Console.WriteLine();

        #endregion

        #region C# 11 - Required Members

        // With 'required', the compiler enforces that properties are set

        // var badRequest = new DriverLicenseRequest
        // {
        //     LicenseNumber = "D1234567"
        //     // ERROR CS9035: Required member 'IssuingState' must be set
        //     // ERROR CS9035: Required member 'DateOfBirth' must be set
        // };

        // This works - all required members are set
        var goodRequest = new DriverLicenseRequest
        {
            LicenseNumber = "D1234567",
            IssuingState = "VA",
            DateOfBirth = new DateOnly(1985, 6, 15),
            // MiddleName is optional - no 'required' keyword
        };

        Console.WriteLine("C# 11 - Required members enforced at compile time:");
        Console.WriteLine($"  LicenseNumber: {goodRequest.LicenseNumber}");
        Console.WriteLine($"  IssuingState: {goodRequest.IssuingState}");
        Console.WriteLine($"  DateOfBirth: {goodRequest.DateOfBirth}");
        Console.WriteLine($"  MiddleName: {goodRequest.MiddleName ?? "(not provided - that's OK, it's optional)"}");
        Console.WriteLine();

        #endregion

        #region Works Great with Records Too

        // Required members work with records for immutable DTOs
        var verificationResult = new VerificationResultRecord
        {
            TransactionId = Guid.NewGuid(),
            IsValid = true,
            VerificationTimestamp = DateTime.UtcNow,
            ResponseCode = "SUCCESS"
        };

        Console.WriteLine("Works with records too:");
        Console.WriteLine($"  TransactionId: {verificationResult.TransactionId}");
        Console.WriteLine($"  IsValid: {verificationResult.IsValid}");
        Console.WriteLine($"  ResponseCode: {verificationResult.ResponseCode}");
        Console.WriteLine();

        #endregion
    }
}

// Old way - no enforcement
public class DriverLicenseRequestOld
{
    public string? LicenseNumber { get; init; }
    public string IssuingState { get; init; }
    public DateOnly DateOfBirth { get; init; }
}

// C# 11 - required members
public class DriverLicenseRequest
{
    public required string LicenseNumber { get; init; }
    public required string IssuingState { get; init; }
    public required DateOnly DateOfBirth { get; init; }

    // Optional - no 'required' keyword
    public string? MiddleName { get; init; }
    public string? Suffix { get; init; }
}

// Works with records too!
public record VerificationResultRecord
{
    public required Guid TransactionId { get; init; }
    public required bool IsValid { get; init; }
    public required DateTime VerificationTimestamp { get; init; }
    public required string ResponseCode { get; init; }

    // Optional fields
    public string? ErrorMessage { get; init; }
    public Dictionary<string, string>? AdditionalData { get; init; }
}