namespace CSharp11Features;

// ============================================================================
// DEMO 3: File-Local Types
// ============================================================================
// The 'file' modifier restricts a type's visibility to the current file.
// Great for helper types that shouldn't leak outside their context.
// ============================================================================

public static class Demo03_File_Local_Types
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 3: File-Local Types");

        #region File-Local Helper Type

        // FileLocalHelper is only visible in this file!
        // It won't pollute the namespace or conflict with types in other files
        // And can be used by multiple classes within this file
        var localFileHelper = new FileLocalHelper();
        var localFileResult = localFileHelper.FormatVerificationResult(true, "VA", "D1234567");

        Utilities.LogResult("File-local helper type (only visible in this file):", localFileResult);

        // A private helper class can access private members in the parent class, and vice versa
        // This creates a more tightly coupled relationship between the two types, and is more brittle
        var privateHelper = new PrivateHelper();
        var privateResult = privateHelper.FormatVerificationResult(true, "VA", "D1234567");

        Utilities.LogResult("Private helper type (only visible in this class):", privateResult);

        // A private helper class can access private members in the parent class, and vice versa
        // This creates a more tightly coupled relationship between the two types, and is more brittle
        var internalHelper = new InternalHelper();
        var internalResult = internalHelper.FormatVerificationResult(true, "VA", "D1234567");

        Utilities.LogResult("Internal helper type (only visible in this assembly):", internalResult);

        #endregion
    }

    // This type is ONLY visible within this class
    private class PrivateHelper
    {
        public string FormatVerificationResult(bool isValid, string state, string licenseNumber)
        {
            var status = isValid ? "VALID" : "INVALID";
            return $"[{state}] {licenseNumber}: {status}";
        }
    }
}


// This type is ONLY visible within this file
// Other files in the same project cannot see or use this type
file class FileLocalHelper
{
    public string FormatVerificationResult(bool isValid, string state, string licenseNumber)
    {
        var status = isValid ? "VALID" : "INVALID";
        return $"[{state}] {licenseNumber}: {status}";
    }
}

// This type is ONLY visible within this class
internal class InternalHelper
{
    public string FormatVerificationResult(bool isValid, string state, string licenseNumber)
    {
        var status = isValid ? "VALID" : "INVALID";
        return $"[{state}] {licenseNumber}: {status}";
    }
}
