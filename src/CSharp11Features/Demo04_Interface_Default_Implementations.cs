namespace CSharp11Features;

// ============================================================================
// DEMO 4: Default Interface Implementations (C# 8+)
// ============================================================================
// Interfaces can now provide default method implementations.
// This allows adding new methods to interfaces without breaking implementers
// ============================================================================

public static class Demo04_Interface_Default_Implementations
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 4: Default Interface Implementations");

        #region The Problem - Interface Versioning

        /*
            // Version 1 of your interface
            public interface IVerificationService
            {
                VerificationResult Verify(string license);
            }

            // ...50 classes implement this interface...

            // Version 2 - you want to add async support
            public interface IVerificationService
            {
                VerificationResult Verify(string license);
                Task<VerificationResult> VerifyAsync(string license);  // BREAKS ALL 50 CLASSES!
            }  
        */

        #endregion

        #region The Solution - Default Implementations

        /*
            public interface IVerificationService
            {
                VerificationResult Verify(string license);
              
                // Default implementation - existing classes don't break
                Task<VerificationResult> VerifyAsync(string license)
                {
                    return Task.FromResult(Verify(license));
                }
            }
        */

        #endregion

        #region Live Demo

        // BasicService only implements the required method
        IVerificationService basic = new BasicVerificationService();

        // AdvancedService overrides the default
        IVerificationService advanced = new AdvancedVerificationService();

        // Both work with sync
        Console.WriteLine("  Sync verification:");
        Console.WriteLine($"    Basic:    {basic.Verify("D1234567").Status}");
        Console.WriteLine($"    Advanced: {advanced.Verify("D1234567").Status}");

        // Both work with async (basic uses default, advanced uses override)
        Console.WriteLine();
        Console.WriteLine("  Async verification:");
        Console.WriteLine($"    Basic (default impl):    {basic.VerifyAsync("D1234567").Result.Status}");
        Console.WriteLine($"    Advanced (custom impl):  {advanced.VerifyAsync("D1234567").Result.Status}");

        // Default implementations are not available from the concrete class if it is not implemented
        //var basicConcreteClassInstance = new BasicVerificationService();
        var advancedConcreteClassInstance = new AdvancedVerificationService();

        // Only Advanced work with sync through the concrete class
        Console.WriteLine("  Async verification:");
        //Console.WriteLine($"    Basic:    {basicConcreteClassInstance.VerifyAsync("D1234567").Status}"); // can't compile
        Console.WriteLine($"    Advanced (custom impl):  {advancedConcreteClassInstance.VerifyAsync("D1234567").Result.Status}");

        #endregion

    }
}

// Interface with default implementations
public interface IVerificationService
{
    // Required - all implementers must provide this
    VerificationResult Verify(string license);

    // Default implementation - can be overridden
    Task<VerificationResult> VerifyAsync(string license)
    {
        // Default: just wrap the sync method
        return Task.FromResult(Verify(license));
    }

    #region Caveats

    // Error: Interfaces cannot contain instance fields
    //string VerifyType; // doesn't work
    //string VerifyType = "Basic"; // doesn't work

    // Error: Interfaces cannot contain auto-implemented properties with backing fields
    //string VerifyType { get; set; } = "Basic"; // doesn't work
    //string VerifyType { get; set; } // works if implemented, but can't have backing field

    // Interfaces can have overridable read-only property
    //string VerifyType => "Basic"; // does work
    //string VerifyType { get { return "Basic"; } } // does work

    #endregion
}

// Basic implementation - only implements required method
public class BasicVerificationService : IVerificationService
{
    public VerificationResult Verify(string license)
    {
        return new VerificationResult("Valid (Basic)");
    }
}

// Advanced implementation - overrides the defaults
public class AdvancedVerificationService : IVerificationService
{
    public VerificationResult Verify(string license)
    {
        return new VerificationResult("Valid (Advanced)");
    }

    // Override the default async implementation
    public async Task<VerificationResult> VerifyAsync(string license)
    {
        await Task.Delay(10); // Simulate actual async work
        return new VerificationResult("Valid (Advanced Async)");
    }

    //public string VerifyType => "Advanced";
}

// Simple result type
public record VerificationResult(string Status, string? Message = null);
