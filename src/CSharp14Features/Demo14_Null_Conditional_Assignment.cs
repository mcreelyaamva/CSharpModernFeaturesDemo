namespace CSharp14Features;

// ============================================================================
// DEMO 14: Null Conditional Assignment
// ============================================================================
// C# 14 Add conditional assignment using the null-conditional operator (?.).
// ============================================================================
public static class Demo14_Null_Conditional_Assignment
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 14: Null-Conditional Assignment");

        #region Assign order using traditional null check

        // retrieve a customer (could be null)
        var customer1 = Customer.GetCustomer(1);

        if (customer1 is not null)
        {
            customer1.Order = Order.GetOrder(123);
        }
        Console.WriteLine($"Customer 1: {customer1}");

        #endregion

        #region Assign order using null-conditional assignment

        // retrieve a customer (could be null)
        var customer2 = Customer.GetCustomer(2);

        // Using null-conditional assignment
        customer2?.Order = Order.GetOrder(124);
        Console.WriteLine($"Customer 2: {customer2}");

        #endregion

        #region Assign order using null-conditional assignment, null value

        // retrieve a customer (could be null)
        var customer3 = Customer.GetCustomer(3);

        // Using null-conditional assignment
        customer3?.Order = Order.GetOrder(125);
        Console.WriteLine($"Customer 3: {customer3}");

        #endregion
    }
}

public class Customer
{
    public string Name { get; set; } = string.Empty;

    public Order? Order { get; set; }

    public override string ToString()
    {
        return Order is not null
            ? $"Customer: {Name}, Order: {Order}"
            : $"Customer: {Name}, No Order";
    }

    public static Customer? GetCustomer(int customerId)
    {
        if (customerId > 2)
            return null;
        return new Customer { Name = customerId == 1 ? "Alice" : "Omar" };
    }
}

public class Order
{
    public int OrderId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"OrderId: {OrderId}, ProductName: {ProductName}";
    }

    public static Order GetOrder(int orderId)
    {
        return new Order
        {
            OrderId = orderId,
            ProductName = orderId == 123 ? "Smartphone" : "Laptop"
        };
    }
}
