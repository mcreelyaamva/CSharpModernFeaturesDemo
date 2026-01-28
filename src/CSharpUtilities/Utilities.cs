namespace CSharpUtilities;

public static class Utilities
{
    public static void LogDemoTitle(string title)
    {
        Console.WriteLine("─".PadRight(70, '─'));
        Console.WriteLine(title);
        Console.WriteLine("─".PadRight(70, '─'));
        Console.WriteLine();
    }

    public static void LogResult(string title, string result)
    {
        Console.WriteLine(title);
        Console.WriteLine(result);
        Console.WriteLine();
    }

    public static void EndDemo()
    {
        Console.WriteLine();
        Console.WriteLine("End of demo...");
        Console.ReadKey();
    }
}
