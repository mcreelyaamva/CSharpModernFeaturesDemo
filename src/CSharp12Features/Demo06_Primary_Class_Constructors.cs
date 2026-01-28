// ReSharper disable CapturedPrimaryConstructorParameterIsMutable
// ReSharper disable UnusedParameter.Local
// ReSharper disable ReplaceWithPrimaryConstructorParameter
#pragma warning disable CS9113
#pragma warning disable CS9107
namespace CSharp12Features;

// ============================================================================
// DEMO 6: Primary Constructors for Classes
// ============================================================================
// Primary constructors eliminate the boilerplate of declaring fields and
// assigning them in a constructor. This is HUGE for DI-heavy code!
// ============================================================================

public static class Demo06_Primary_Class_Constructors
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 6: Primary Constructors for Classes");

        #region Before C# 12 - So Much Boilerplate!

        /*
            public class VerificationService
            {
                private readonly ILogger<VerificationService> _logger;
                private readonly IVerificationRepository _repository;
                private readonly IConfiguration _config;
                private readonly IHttpClientFactory _httpClientFactory;
        
                public VerificationService(
                    ILogger<VerificationService> logger,
                    IVerificationRepository repository,
                    IConfiguration config,
                    IHttpClientFactory httpClientFactory)
                {
                    _logger = logger;
                    _repository = repository;
                    _config = config;
                    _httpClientFactory = httpClientFactory;
                }
        
                public async Task<VerificationResult> VerifyAsync(string license)
                {
                   _logger.LogInformation("Verifying {License}", _license);
                   var timeout = _config.GetValue<int>("VerificationTimeout");
                   return await _repository.VerifyAsync(_license);
                }
            }
        */

        #endregion

        #region C# 12 - Primary Constructors

        /*
            public class VerificationService(
                ILogger<VerificationService> logger,
                IVerificationRepository repository,
                IConfiguration config,
                IHttpClientFactory httpClientFactory)
            {
                public async Task<VerificationResult> VerifyAsync(string license)
                {
                    logger.LogInformation("Verifying {License}", license);
                    var timeout = config.GetValue<int>("VerificationTimeout");
                    return await repository.VerifyAsync(license);
                }
            }

        */

        #endregion

        #region Using Example Classes

        // Create a service using primary constructor
        var service = new LicenseValidationService(new ConsoleLogger(), "VA", 4);
        var result = service.ValidateLicense("D1234567");

        Console.WriteLine($"Validation result: {result}");
        Console.WriteLine();

        #endregion

        #region Combining with Inheritance

        var specialService = new SpecializedLicenseService(new ConsoleLogger(), "MD", 5, true);
        var specialResult = specialService.ValidateLicense("D1234567");

        Console.WriteLine($"Caching enabled: {specialService.IsCachingEnabled}");
        Console.WriteLine($"Validation result: {specialResult}");
        Console.WriteLine();

        #endregion

        #region Mutable Parameter Caveat

        var mutableLicenseService = new MutableLicenseService(new ConsoleLogger(), "MO", 6);
        mutableLicenseService.SetLogger(new SomeOtherLogger()); // override logger!

        var mutableResult = mutableLicenseService.ValidateLicense("D1234567");

        Console.WriteLine($"Mutable validation result: {mutableResult}");
        Console.WriteLine();

        #endregion

        #region Force a primary constructor parameter to be immutable

        var imMutableLicenseService = new ImmutableLicenseService(new ConsoleLogger(), "CA", 7);
        var imMutableResult = imMutableLicenseService.ValidateLicense("D1234567");

        Console.WriteLine($"Immutable validation result: {imMutableResult}");
        Console.WriteLine();

        #endregion
    }
}

// Old Style Class Definition
public class LicenseValidationService
{
    private readonly ISimpleLogger _logger;
    private readonly string _defaultState;
    private readonly int _maxRetries;

    public LicenseValidationService(
        ISimpleLogger logger,
        string defaultState,
        int maxRetries = 3)
    {
        _logger = logger;
        _defaultState = defaultState;
        _maxRetries = maxRetries;
    }

    public string ValidateLicense(string licenseNumber)
    {
        _logger.Log($"Validating license {licenseNumber} for state {_defaultState}");
        _logger.Log($"Max retries configured: {_maxRetries}");

        // Simulate validation
        return $"License {licenseNumber} is valid in {_defaultState}";
    }

    public string GetState() => _defaultState;
}

/*
// C# 12 Primary Constructor - clean and concise!
public class LicenseValidationService(
    ISimpleLogger logger,
    string defaultState,
    int maxRetries = 3)
{
    public string ValidateLicense(string licenseNumber)
    {
        logger.Log($"Validating license {licenseNumber} for state {defaultState}");
        logger.Log($"Max retries configured: {maxRetries}");

        // Simulate validation
        return $"License {licenseNumber} is valid in {defaultState}";
    }

    public string GetState() => defaultState;
}
*/

// Inheritance with primary constructors
public class SpecializedLicenseService(
    ISimpleLogger logger,
    string defaultState,
    int maxRetries,
    bool enableCaching)
    : LicenseValidationService(logger, defaultState, maxRetries) 
{
    public bool IsCachingEnabled => enableCaching;
}

// Mutable parameter example
public class MutableLicenseService(
    ISimpleLogger logger,
    string defaultState,
    int maxRetries = 3)
{
    public string ValidateLicense(string licenseNumber)
    {
        logger.Log($"Validating license {licenseNumber} for state {defaultState}");
        logger.Log($"Max retries configured: {maxRetries}");

        // Simulate validation
        return $"License {licenseNumber} is valid in {defaultState}";
    }

    public void SetLogger(ISimpleLogger newLogger)
    {
        logger = newLogger; // this compiles! Parameter is mutable!
    }
}

// Immutable parameter example
public class ImmutableLicenseService(
    ISimpleLogger logger,
    string defaultState,
    int maxRetries = 3)
{
    private readonly ISimpleLogger _logger = logger;

    public string ValidateLicense(string licenseNumber)
    {
        _logger.Log($"Validating license {licenseNumber} for state {defaultState}");
        _logger.Log($"Max retries configured: {maxRetries}");

        // Simulate validation
        return $"License {licenseNumber} is valid in {defaultState}";
    }

    /*
    public void SetLogger(ISimpleLogger newLogger)
    {
        _logger = newLogger; // this does not compile, parameter is protected by readonly field
    }
    */
}

// Simple logger for demo purposes
public interface ISimpleLogger
{
    void Log(string message);
}

public class ConsoleLogger : ISimpleLogger
{
    public void Log(string message) => Console.WriteLine($"{message}");
}

public class SomeOtherLogger : ISimpleLogger
{
    public void Log(string message) => Console.WriteLine($"OOPS! This shouldn't be modifiable: {message}");
}