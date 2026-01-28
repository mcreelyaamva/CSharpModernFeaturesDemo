// ReSharper disable UnusedVariable
// ReSharper disable UseCollectionExpression
// ReSharper disable RedundantExplicitArrayCreation
namespace CSharp12Features;

// ============================================================================
// DEMO 7: Collection Expressions
// ============================================================================
// Unified syntax for creating arrays, lists, spans, and other collections.
// Also includes the spread operator for combining collections!
// ============================================================================

public static class Demo07_Collection_Expressions
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 7: Collection Expressions");

        #region Original C# Syntax

        // Array
        int[] array1 = new int[] { 1, 2, 3, 4, 5 };

        // List
        List<int> list1 = new List<int>();
        list1.Add(1);
        list1.Add(2);
        list1.Add(3);
        list1.Add(4);
        list1.Add(5);

        // HashSet 
        HashSet<string> hashset1 = new HashSet<string>();
        hashset1.Add("1");
        hashset1.Add("2");
        hashset1.Add("3");
        hashset1.Add("4");
        hashset1.Add("5");

        Console.WriteLine("Original Syntax");
        Console.WriteLine($"  array => [{string.Join(", ", array1)}]");
        Console.WriteLine($"  list => [{string.Join(", ", list1)}]");
        Console.WriteLine($"  hashset => [{string.Join(", ", hashset1)}]");
        Console.WriteLine();

        #endregion

        #region Collection Initializers

        // Collection Initializers C# 3.0
        int[] array2 = { 1, 2, 3, 4, 5 };
        var list2 = new List<int> { 1, 2, 3, 4, 5 };
        var hashset2 = new HashSet<int> { 1, 2, 3, 4, 5 };

        Console.WriteLine("Collection Initializers");
        Console.WriteLine($"  array => [{string.Join(", ", array2)}]");
        Console.WriteLine($"  list => [{string.Join(", ", list2)}]");
        Console.WriteLine($"  hashset => [{string.Join(", ", hashset2)}]");
        Console.WriteLine();

        #endregion

        #region Target Typing

        // Target Typing: C# 9.0
        int[] array3 = { 1, 2, 3, 4, 5 };
        List<int> list3 = new() { 1, 2, 3, 4, 5 };
        Span<int> span3 = stackalloc int[] { 1, 2, 3, 4, 5 };
        HashSet<int> hashset3 = new() { 1, 2, 3, 4, 5 };

        Console.WriteLine("Target Typing");
        Console.WriteLine($"  array => [{string.Join(", ", array3)}]");
        Console.WriteLine($"  list => [{string.Join(", ", list3)}]");
        Console.WriteLine($"  span => [{string.Join(", ", span3.ToArray())}]");
        Console.WriteLine($"  hashset => [{string.Join(", ", hashset3)}]");
        Console.WriteLine();

        #endregion

        #region Collection Expressions

        // Collection Expressions: All use the same [ ] syntax now!
        int[] array4 = [1, 2, 3, 4, 5];
        List<int> list4 = [1, 2, 3, 4, 5];
        Span<int> span4 = [1, 2, 3, 4, 5];
        HashSet<int> hashset4 = [1, 2, 3, 4, 5];

        Console.WriteLine("Collection Expressions");
        Console.WriteLine($"  array => [{string.Join(", ", array4)}]");
        Console.WriteLine($"  list => [{string.Join(", ", list4)}]");
        Console.WriteLine($"  span => [{string.Join(", ", span4.ToArray())}]");
        Console.WriteLine($"  hashset => [{string.Join(", ", hashset4)}]");
        Console.WriteLine();

        #endregion

        #region Empty Collections

        //  Previous empty collections
        string[] oldEmptyArray = Array.Empty<string>();
        List<int> oldEmptyList = new List<int>();

        // Much cleaner than Array.Empty<string>() or new List<int>()
        string[] emptyArray = [];
        List<int> emptyList = [];

        #endregion

        #region Spread Operator

        string[] coreServices = ["DLDV", "HAVV", "USPVS"];
        string[] newServices = ["SSOLV", "VSAdmin"];

        // Spread combines collections
        string[] allServices = [.. coreServices, .. newServices];

        Console.WriteLine("Spread Combine Collections");
        Console.WriteLine($"  coreServices: [{string.Join(", ", coreServices)}]");
        Console.WriteLine($"  newServices:  [{string.Join(", ", newServices)}]");
        Console.WriteLine($"  allServices:  [{string.Join(", ", allServices)}]");
        Console.WriteLine();

        // Mix spread with individual items
        Console.WriteLine("Spread Combine Collections");
        string[] mixedServices = [.. coreServices, "DebugService", .. newServices, "MockService"];
        Console.WriteLine($"  Mixed spread: [{string.Join(", ", mixedServices)}]");
        Console.WriteLine();

        #endregion

        #region Conditional Spreading

        var includeDebugServices = true;
        var includeLegacyServices = false;

        string[] debugServices = ["DebugService", "MockService"];
        string[] legacyServices = ["OldDLDV", "LegacyHAVV"];

        // Conditionally include collections
        string[] configuredServices = [
            "DLDV",
            "HAVV",
            ..(includeDebugServices ? debugServices : []),
            ..(includeLegacyServices ? legacyServices : [])
        ];

        Console.WriteLine("Conditional Spreading:");
        Console.WriteLine($"  includeDebugServices: {includeDebugServices}");
        Console.WriteLine($"  includeLegacyServices: {includeLegacyServices}");
        Console.WriteLine($"  Result: [{string.Join(", ", configuredServices)}]");
        Console.WriteLine();

        #endregion

        #region Practical Example - Building API Responses

        var standardHeaders = new KeyValuePair<string, string>[]
        {
            new("Content-Type", "application/json"),
            new("X-Request-Id", Guid.NewGuid().ToString()[..8])
        };

        var authHeaders = new KeyValuePair<string, string>[]
        {
            new("Authorization", "Bearer xxx..."),
            new("X-Api-Version", "3.0")
        };

        var requiresAuth = true;

        KeyValuePair<string, string>[] responseHeaders = [
            ..standardHeaders,
            ..(requiresAuth ? authHeaders : []),
            new("X-Timestamp", DateTime.UtcNow.ToString("O"))
        ];

        Console.WriteLine("Response headers:");
        foreach (var header in responseHeaders)
            Console.WriteLine($"    {header.Key}: {header.Value}");
        Console.WriteLine();

        #endregion
    }
}
