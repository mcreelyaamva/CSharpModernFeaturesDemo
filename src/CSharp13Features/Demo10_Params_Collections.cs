namespace CSharp13Features;

// ============================================================================
// DEMO 10: params Collections
// ============================================================================
// 'params' now works with Span<T>, ReadOnlySpan<T>, IEnumerable<T>, and
// other collection types - not just arrays! This enables zero-allocation calls.
// ============================================================================

public static class Demo10_Params_Collections
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 10: params Collections");

        #region Basic overview

        // Before C# 13 - params only worked with arrays
        LogStatesArray("VA", "MD", "DC");

        // C# 13 - params with ReadOnlySpan<T> (zero allocation!)
        LogStatesSpan("VA", "MD", "DC");

        // params with IEnumerable
        ProcessServices("DLDV", "HAVV", "USPVS");

        // Or pass a collection directly with collection expression
        ProcessServices(["DLDV", "HAVV", "USPVS", "SSOLV"]);

        #endregion

        #region High-performance sum example

        Console.WriteLine("High-performance Sum Example:");

        // These calls don't allocate arrays
        var sum1 = Sum(1, 2, 3, 4, 5);
        var sum2 = Sum(10, 20, 30);
        var sum3 = Sum(100);

        Console.WriteLine($"  Sum(1, 2, 3, 4, 5) = {sum1}");
        Console.WriteLine($"  Sum(10, 20, 30) = {sum2}");
        Console.WriteLine($"  Sum(100) = {sum3}");
        Console.WriteLine();

        #endregion

        #region Flexible Logging Example

        Console.WriteLine("Flexible Logging Example:");

        // Individual parameters
        LogVerification("DLDV", "D1234567", "VA");

        // Can also pass tuples or structured data
        var verifications = new[]
        {
            ("HAVV", "H9876543", "MD"),
            ("USPVS", "U5555555", "DC")
        };
        foreach (var (service, license, state) in verifications)
        {
            LogVerification(service, license, state);
        }

        #endregion
    }

    // Old way - params with array (allocates)
    static void LogStatesArray(params string[] states)
    {
        Console.WriteLine("BEFORE C# 13 - params only worked with arrays:");
        Console.WriteLine($"  [Array version] Logging {states.Length} states: {string.Join(", ", states)}");
        Console.WriteLine("  (This allocated a new string[] on the heap)");
        Console.WriteLine();
    }

    // NEW in C# 13 - params with Span (no allocation!)
    static void LogStatesSpan(params ReadOnlySpan<string> states)
    {
        Console.WriteLine("C# 13 - params with ReadOnlySpan<T> (zero allocation):");
        Console.Write($"  [Span version] Logging {states.Length} states: ");
        for (int i = 0; i < states.Length; i++)
        {
            if (i > 0) Console.Write(", ");
            Console.Write(states[i]);
        }
        Console.WriteLine();
        Console.WriteLine("  (No heap allocation - stack allocated!)");
        Console.WriteLine();
    }

    // NEW in C# 13 - params with IEnumerable (flexible)
    static void ProcessServices(params IEnumerable<string> services)
    {
        Console.WriteLine("params with IEnumerable<T> for flexibility:");
        Console.Write("  Services: ");
        Console.WriteLine(string.Join(", ", services));
        Console.WriteLine();
    }

    // High-performance sum with no allocation
    static int Sum(params ReadOnlySpan<int> values)
    {
        int total = 0;
        foreach (var value in values)
            total += value;
        return total;
    }

    /// Log Verification Checks
    static void LogVerification(params ReadOnlySpan<string> parts)
    {
        if (parts.Length >= 3)
        {
            Console.WriteLine($"  [{parts[0]}] Verified {parts[1]} in {parts[2]}");
        }
    }
}
