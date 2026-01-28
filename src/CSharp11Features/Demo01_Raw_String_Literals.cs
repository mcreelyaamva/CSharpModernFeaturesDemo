namespace CSharp11Features;

// ============================================================================
// DEMO 1: Raw String Literals
// ============================================================================
// Raw string literals eliminate escape character nightmares when working
// with JSON, SQL, XML, and other text that contains quotes or special chars.
// ============================================================================

public static class Demo01_Raw_String_Literals
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 1: Raw String Literals");

        #region Before C# 11 - Escape Characters + Verbatim String Literals

        // Before C# 11: Escape characters everywhere!
        var jsonOld = "{\n  \"name\": \"DLDV\",\n  \"version\": \"3.0\",\n  \"isActive\": true\n}";

        Utilities.LogResult("BEFORE C# 11 - Escaped string (hard to read in code):", jsonOld);

        // Before C# 11: Verbatim string literals - slightly better
        jsonOld = @"{
  ""name"": ""DLDV"",
  ""version"": ""3.0""
  ""isActive"": true
}";

        Utilities.LogResult("BEFORE C# 11 - Verbatim String (slightly better in code):", jsonOld);

        #endregion

        #region Raw String Literals with """

        // C# 11: Raw string literals - what you see is what you get!
        // The closing """ determines the left margin - content is "de-indented"
        var jsonNew = """
            {
              "name": "DLDV",
              "version": "3.0",
              "isActive": true,
              "services": ["verification", "validation", "audit"]
            }
            """;

        Utilities.LogResult("C# 11 - Raw string literal (much cleaner!):",jsonNew );

        #endregion

        #region Raw String Interpolation with $ / $$

        // Single $ for normal interpolation
        var name = "Matt";
        var greeting = $"""Hello, {name}!""";

        Utilities.LogResult("C# 11 - Basic raw string literal with interpolation", greeting);

        // For interpolation that contains literal braces, use $$ (or more $s) to distinguish interpolation braces
        // The number of $ signs = number of braces needed for interpolation
        var serviceName = "DLDV";
        var version = "3.0";
        var services = new[] { "verification", "validation", "audit" };

        // $$ means we use {{ }} for interpolation, leaving single { } for JSON
        var jsonInterpolated = $$"""
            {
              "name": "{{serviceName}}",
              "version": "{{version}}",
              "serviceCount": {{services.Length}},
              "timestamp": "{{DateTime.UtcNow:o}}"
            }
            """;

        Utilities.LogResult("C# 11 - JSON raw string literal with interpolation", jsonInterpolated);

        #endregion

        #region SQL Queries

        // SQL query raw string literal + interpolation example
        var state = "VA";
        var sqlQuery = $$"""
            SELECT 
                dl.LicenseNumber,
                dl.FirstName,
                dl.LastName,
                dl.DateOfBirth,
                v.VerificationDate,
                v.Status
            FROM DriverLicenses dl
            INNER JOIN Verifications v ON dl.LicenseId = v.LicenseId
            WHERE dl.IssuingState = '{{state}}'
              AND v.VerificationDate >= DATEADD(day, -30, GETUTCDATE())
            ORDER BY v.VerificationDate DESC
            """;

        Utilities.LogResult("C# 11 - SQL raw string with interpolation", sqlQuery);

        #endregion

        #region XML Configuration

        // XML config raw string literal + interpolation example
        var serviceEndpoint = "https://api.aamva.org/dldv/v3";
        var xmlConfig = $$"""
            <?xml version="1.0" encoding="utf-8"?>
            <configuration>
              <appSettings>
                <add key="ServiceEndpoint" value="{{serviceEndpoint}}" />
                <add key="TimeoutSeconds" value="30" />
                <add key="RetryCount" value="3" />
              </appSettings>
            </configuration>
            """;

        Utilities.LogResult("C# 11 - XML raw string with interpolation",xmlConfig );

        #endregion

        #region Embedding Double Braces

        // Content has double braces - use $$$
        var interpolatedValue = "TEST";
        var template = $$$"""
                             Some text with {{literal braces}} and {{{interpolatedValue}}} value
                          """;

        Utilities.LogResult("C# 11 - Literal with double braces", template);

        #endregion

        #region Embedding Triple+ Double Quotes

        var withQuotes = """"
                            She said """Hello""" to me
                         """";

        Utilities.LogResult("C# 11 - Literal with multiple double quotes", withQuotes);

        #endregion

        #region Appropriate Use Cases

        /*

        // ==== Unit Tests ========================================================

        [Fact]
        public async Task Should_Return_Valid_DLDV_Response()
        {
            // GOOD: Test fixtures with expected data
            var expectedXml = """
                <DLDVResponse>
                    <Status>Valid</Status>
                    <VerificationId>12345</VerificationId>
                </DLDVResponse>
                """;
            
            var result = await _service.VerifyDriver(request);
            
            result.Should().BeEquivalentTo(XDocument.Parse(expectedXml));
        }


        // ==== Logging  ==========================================================

        // Logging complex request/response for troubleshooting
        _logger.LogWarning($$"""
            DLDV Verification Failed
            ----------------------------------------
            Request ID: {{requestId}}
            License Number: {{licenseNumber}}
            State: {{state}}
            Attempt: {{attemptCount}}
            Error: {{errorMessage}}
            Endpoint: {{serviceEndpoint}}
            Duration: {{elapsed.TotalMilliseconds}}ms
            ----------------------------------------
            """);


        // ==== Debug Helpers  ===================================================

        #if DEBUG

        app.MapGet("/debug/health", () => """
            {
                "status": "healthy",
                "environment": "development",
                "timestamp": "2026-01-27T10:00:00Z"
            }
            """);

        #endif


        // ==== Code Generation  ===================================================

        public class EntityGenerator
        {
            public string GenerateEntity(string className, List<(string Name, string Type)> properties)
            {
                var props = string.Join("\n", properties.Select(p => 
                    $"    public {p.Type} {p.Name} {{ get; set; }}"));
                
                return $$"""
                    using System;
                    
                    namespace AAMVA.DLDV.Models
                    {
                        public class {{className}}
                        {
                    {{props}}
                        }
                    }
                    """;
            }
        }

        */

        #endregion
    }
}

