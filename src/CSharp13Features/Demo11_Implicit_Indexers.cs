namespace CSharp13Features;

// ============================================================================
// DEMO 11: Implicit Index Access in Object Initializers
// ============================================================================
// C# 8 introduced the ^ operator for indexing from the end of collections.
//     int last = myArray[^1];
// In C# 13, you can now use the ^ (from-end) indexer in object initializers.
// ============================================================================

public static class Demo11_Implicit_Indexers
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 11: Implicit Index Access in Object Initializers");

        #region Basic Example

        // The ^ indexer counts from the end
        // ^1 = last element, ^2 = second to last, etc.
        var buffer = new VerificationBuffer
        {
            Entries =
            {
                [0] = "First item",
                [1] = "Second item",
                [^2] = "Second to last item",   // NEW in C# 13
                [^1] = "Last item"              // NEW in C# 13
            }
        };

        Console.WriteLine("Basic Example Result:");
        for (var i = 0; i < buffer.Entries.Length; i++)
        {
            Console.WriteLine($"  [{i}] = \"{buffer.Entries[i] ?? "(null)"}\"");
        }
        Console.WriteLine();

        // Accessing via the indexer
        Console.WriteLine($"  Last Item: {buffer.Entries[^1]}");

        // Accessing via linq extension method
        Console.WriteLine($"  Last Item: {buffer.Entries.Last()}");
        Console.WriteLine();

        #endregion

        #region Countdown Example

        var countdown = new TimerRemaining()
        {
            buffer =
            {
                [^1] = 0,
                [^2] = 1,
                [^3] = 2,
                [^4] = 3,
                [^5] = 4,
                [^6] = 5,
                [^7] = 6,
                [^8] = 7,
                [^9] = 8,
                [^10] = 9,
                [^11] = 10
            }
        };

        Console.WriteLine("Countdown to take off");
        foreach (var currentCount in countdown.buffer)
        {
            Console.WriteLine($"  {currentCount}");
        }
        Console.WriteLine();

        #endregion
    }
}

public class VerificationBuffer
{
    public string?[] Entries { get; } = new string[5];
}

public class TimerRemaining
{
    public int[] buffer { get; set; } = new int[11];
}

