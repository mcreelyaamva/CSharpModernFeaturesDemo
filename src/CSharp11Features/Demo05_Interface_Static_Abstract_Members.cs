namespace CSharp11Features;

// ============================================================================
// DEMO 5: Static Abstract/Virtual Members in Interfaces (C# 11)
// ============================================================================
// Interfaces can now require static members, enabling generic math,
// factory patterns, and more.
// ============================================================================

public static class Demo05_Interface_Static_Abstract_Members
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 5: Static Abstract/Virtual Members in Interfaces");

        #region The Problem - No Static Polymorphism

        /*
              // Before C# 11, you couldn't do this since static members weren't part of the interface contract:
              public interface IHasFactory
              {
                  static IHasFactory Create();
              }
              public T Create<T>() where T : IHasFactory
              {
                  return T.Create();  // Compile error!
              }
        */

        #endregion

        #region The Solution - Static Abstract Members

        /*
              //THE SOLUTION - Static abstract interface members:
              public interface IVerificationRequest
              {
                  static abstract string ServiceCode { get; }
                  static abstract IVerificationRequest Create(string id);
              }
              
              // Now generics can access static members!
              public T CreateRequest<T>(string id) where T : IVerificationRequest
              {
                  Console.WriteLine($"Creating request for {T.ServiceCode}");
                  return T.Create(id);
              }

        */

        #endregion

        #region Factory Pattern

        var dldvRequest = CreateRequest<DldvRequest>("D1234567");
        var havvRequest = CreateRequest<HavvRequest>("123456789");
        var uspvsRequest = CreateRequest<UspvsRequest>("987654321");

        Console.WriteLine($"  {DldvRequest.ServiceCode} Id: {dldvRequest.Id}");
        Console.WriteLine($"  {HavvRequest.ServiceCode} Id: {havvRequest.Id}");
        Console.WriteLine($"  {UspvsRequest.ServiceCode} Masked Id: {uspvsRequest.GetMaskedId()}");
        Console.WriteLine();

        #endregion

    }

    // Generic factory using static abstract members
    static T CreateRequest<T>(string id) where T : IVerificationRequest<T>
    {
        Console.WriteLine($"  Creating {T.ServiceCode} request...");
        return T.Create(id);
    }
}

// Interface with static abstract members
public interface IVerificationRequest<T> where T : IVerificationRequest<T>
{
    static abstract string ServiceCode { get; }
    static abstract T Create(string id);
    string Id { get; }
}

// C# 9 example record types implementing the interface
public record DldvRequest(string Id) : IVerificationRequest<DldvRequest>
{
    public static string ServiceCode => "DLDV";
    public static DldvRequest Create(string id) => new(id);
}

public record HavvRequest(string Id) : IVerificationRequest<HavvRequest>
{
    public static string ServiceCode => "HAVV";
    public static HavvRequest Create(string id) => new(id);
}

// standard c# class implementing the interface
public class UspvsRequest : IVerificationRequest<UspvsRequest>
{
    public UspvsRequest(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
    public static string ServiceCode => "USPVS";
    public static UspvsRequest Create(string id) => new UspvsRequest(id);
    public string GetMaskedId() => Id.Substring(Id.Length - 4).PadLeft(Id.Length, '*');
}
