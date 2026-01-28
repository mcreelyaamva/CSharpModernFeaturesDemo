namespace CSharp14Features;

// ============================================================================
// DEMO 13: Extension Everything
// ============================================================================
// C# 14 adds extension types, enabling extension methods to add properties,
// operators, and static members to existing types.
// ============================================================================

public static class Demo13_Extension_Everything
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 13: Extension Methods - Current & Future");

        // traditional extension method
        var sourceString  = "hello world, how are you?";
        Console.WriteLine($"WordCount: {sourceString.WordCount_Traditional()}");

        #region new extension syntax examples

        // new extension method syntax
        Console.WriteLine($"WordCount: {sourceString.WordCount()}");

        // new extension property
        sourceString = "madam";
        Console.WriteLine($"IsPalindrome: {sourceString.IsPalindrome}");

        // new static extension method
        sourceString = "this does have a value";
        Console.WriteLine($"HasValue: {string.HasValue(sourceString)}");

        // new static extension method
        sourceString = "string example 1";
        var sourceString2 = "string example 2";
        Console.WriteLine($"Combine: {string.Combine(sourceString, sourceString2)}");

        // new static extension operator
        sourceString = "C:" | "temp" | "file.txt";
        Console.WriteLine($"Custom operator: {sourceString}");

        #endregion
    }
}

public static class MyExtensions
{
    public static int WordCount_Traditional(this string source)
    {
        return source.Split([' ', '.', '?'], StringSplitOptions.RemoveEmptyEntries).Length;
    }

    #region new extension syntax examples

    extension(string source)
    {
        // standard instance extension method with new syntax
        public int WordCount() =>
            source.Split([' ', '.', '?'], StringSplitOptions.RemoveEmptyEntries).Length;

        // new extension property
        public bool IsPalindrome => source == new string(source.Reverse().ToArray());

        // static extension method
        public static bool HasValue(string value)
            => !string.IsNullOrEmpty(value);

        // static extension method
        public static string Combine(string value1, string value2)
            => string.Join("+", [value1, value2]);

        // static string operator extension
        public static string operator | (string left, string right)
            => string.Join("\\", left, right);
    }

    /*

    // extension indexers did not make it into C# 14, but here is an example of what they might look like:
    extension<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
    {
       public TValue this[TKey key, TValue fallback]
           => dictionary.TryGetValue(key, out var value) ? value : fallback;
    }

    */

    #endregion
}
