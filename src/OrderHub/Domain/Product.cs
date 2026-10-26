namespace OrderHub.Domain;

public class Product : IComparable<Product>
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Category { get; set; } = "";

    // ЗАДАЧА: объявите конструктор с параметрами Name, Price, Category
    // ЗАДАЧА: переопределите свойства, чтобы они были только для чтения (private set)
    // ЗАДАЧА: верните в CompareTo результат сравнения по цене — естественный порядок
    public int CompareTo(Product? other) => throw new NotImplementedException();
}
