#pragma warning disable CS0219
namespace CSharp12Features;

// ============================================================================
// DEMO 8: Type Aliases (using aliases for any type)
// ============================================================================
// 'using' can now alias any type, not just named types.
// Great for tuples, complex generics, and domain modeling.
// ============================================================================

// Type aliases at the top of the file
using StateCode = string;
using LicenseNumber = string;
using Coordinate = (double Latitude, double Longitude);
using ServiceHealth = (string ServiceName, bool IsHealthy, int ResponseTimeMs);
using VerificationResults = Dictionary<string, (bool IsValid, string Message, DateTime Timestamp)>; // value tuple with named elements

public static class Demo08_Type_Aliases
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 8: Type Aliases (using aliases for any type)");

        #region Aliasing Primitive Types for Domain Clarity

        StateCode state = "VA";
        LicenseNumber license = "D1234567";

        ExampleUsage.VerifyLicense(state, license);

        #endregion

        #region Aliasing Tuples

        Coordinate dmvLocation = (38.9072, -77.0369);
        ExampleUsage.VerifyCoordinates(dmvLocation);

        #endregion

        #region Aliasing Tuples in Collections

        ServiceHealth[] healthChecks =
        [
            ("DLDV", true, 45),
            ("HAVV", true, 32),
            ("USPVS", false, 5000),
        ];

        ExampleUsage.VerifyHealthChecks(healthChecks);

        #endregion

        #region Aliasing Complex Types

        VerificationResults results = new()
        {
            ["D1234567"] = (true, "Valid license", DateTime.UtcNow),
            ["D9999999"] = (false, "Expired license", DateTime.UtcNow),
        };

        ExampleUsage.VerifyResults(results);

        #endregion
    }
}

public static class ExampleUsage
{
    // For primitive type aliases, the method signature is self-documenting
    public static void VerifyLicense(LicenseNumber license, StateCode state)
    {
        Console.WriteLine("Aliasing primitive types for domain clarity:");
        Console.WriteLine($"  StateCode: {state}");
        Console.WriteLine($"  LicenseNumber: {license}");
        Console.WriteLine();
    }

    // For tuple aliases, the method signature is simplified and clearer
    public static void VerifyCoordinates(Coordinate location)
    {
        Console.WriteLine("Aliasing Coordinate Tuple:");
        Console.WriteLine($"  Location: ({location.Latitude}, {location.Longitude})");
        Console.WriteLine();
    }

    // For collections of tuples, the method signature is much cleaner
    public static void VerifyHealthChecks(ServiceHealth[] healthChecks)
    {
        Console.WriteLine("Aliasing Collections:");

        foreach (var health in healthChecks)
            Console.WriteLine($"  {health.ServiceName}: {(health.IsHealthy ? "Healthy" : "Degraded")} | {health.ResponseTimeMs}ms");
        
        Console.WriteLine();
    }

    // Simplified signature for complex types vs having to write out full types or break tuples into separate variables
    public static void VerifyResults(VerificationResults results)
    {
        Console.WriteLine("Aliasing Complex Types: ");

        foreach (var (key, value) in results)
            Console.WriteLine($"  [{key}] => Valid: {value.IsValid}, Message: {value.Message}, Timestamp: {value.Timestamp:D}");
        
        Console.WriteLine();
    }
}