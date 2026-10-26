using OrderHub.Domain;

namespace OrderHub.Services;

public class CategorySummary
{
    public string Category { get; set; } = "";
    public int Count { get; set; }
    public decimal AveragePrice { get; set; }
}

public class ReportService
{
    // ЗАДАЧА: Where → OrderBy → Select → ToList, агрегаты Count / Any / All / First
    public List<string> ExpensiveProducts(IEnumerable<Product> products, decimal threshold) => throw new NotImplementedException();
    public int CountExpensive(IEnumerable<Product> products, decimal threshold) => throw new NotImplementedException();
    public bool HasVeryExpensive(IEnumerable<Product> products, decimal threshold) => throw new NotImplementedException();
    public bool AllInStock(IEnumerable<Product> products) => throw new NotImplementedException();
    public Product? Cheapest(IEnumerable<Product> products) => throw new NotImplementedException();

    // ---- Этап 6. КТ36: проекции, фильтрация, группировка ----
    public List<CategorySummary> SummarizeByCategory(IEnumerable<Product> products) => throw new NotImplementedException();
    public CategorySummary? MostExpensiveCategory(IEnumerable<Product> products) => throw new NotImplementedException();
    public List<string> CategoriesWithAtLeast(IEnumerable<Product> products, int minCount) => throw new NotImplementedException();

    // ---- Этап 7. КТ37: запросы к коллекциям, Join и Take ----
    public List<string> TopCustomers(IEnumerable<Customer> customers, IEnumerable<Order> orders, int topCount) => throw new NotImplementedException();
    public decimal RevenueForYear(IEnumerable<Order> orders, int year) => throw new NotImplementedException();
    public string? CustomerWithMostOrders(IEnumerable<Customer> customers, IEnumerable<Order> orders) => throw new NotImplementedException();
    public List<Order> OrdersAboveAverage(IEnumerable<Order> orders) => throw new NotImplementedException();
}
