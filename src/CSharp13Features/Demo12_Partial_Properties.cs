namespace CSharp13Features;

// ============================================================================
// DEMO 12: Partial Properties
// ============================================================================
// Partial members can now include properties, great for source generators.
// Allows you to add custom// get / setter code, validation, and logging without
//     losing your changes after regenerating the classes
// C# 14 allows for partial constructors as well, but that's not a part of this demo
// ============================================================================

public static class Demo12_Partial_Properties
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 12: Partial Properties");

        // Demo with our partial class
        var config = new VerificationConfig
        {
            ServiceEndpoint = "https://api.aamva.org/dldv/v3"
        };

        Console.WriteLine($"  ServiceEndpoint: {config.ServiceEndpoint}");
        Console.WriteLine($"  TimeoutSeconds: {config.TimeoutSeconds}");
        Console.WriteLine();
    }
}

// Declaration part (could be source-generated)
public partial class VerificationConfig
{
    public partial string ServiceEndpoint { get; set; }
    public partial int TimeoutSeconds { get; }
}

// Implementation part (handwritten)
public partial class VerificationConfig
{
    private string _endpoint = "";

    public partial string ServiceEndpoint
    {
        get => _endpoint;
        set => _endpoint = value ?? throw new ArgumentNullException(nameof(value));
    }

    public partial int TimeoutSeconds => 30;
}