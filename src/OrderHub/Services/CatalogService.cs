using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class CatalogService
{
    private readonly List<Product> products = new();

    public void AddProduct(string name, decimal price, string category)
    {
        // ЗАДАЧА: добавьте товар в список products
        products.Add(new Product(name, price, category));
    }

    public void PrintAll()
    {
        Console.WriteLine("--- Каталог в исходном порядке ---");
        // ЗАДАЧА: добавьте метод PrintAll(), который печатает каталог в исходном порядке
        foreach (var product in products)
            Console.WriteLine($"  {product}");
    }

    public void PrintSortedByNaturalOrder()
    {
        // ЗАДАЧА1: вызовите Sort() без аргументов — сработает IComparable<Product>
        products.Sort();

        // ЗАДАЧА2: выведите каждый товар через foreach
        Console.WriteLine("--- Каталог: сортировка по цене (Sort(), IComparable<Product>) ---");
        foreach (var product in products)
            Console.WriteLine($"  {product}");
    }

    public void PrintSortedByName()
    {
        // ЗАДАЧА: вызовите Sort(new NameComparer())
        products.Sort(new NameComparer());

        // ЗАДАЧА: выведите результат
        Console.WriteLine("--- Каталог: сортировка по названию (Sort(new NameComparer()), IComparer<Product>) ---");
        foreach (var product in products)
            Console.WriteLine($"  {product}");
    }

    public void PrintSortedByPriceDesc()
    {
        // дополнение этапа 4: альтернативный порядок по убыванию цены
        products.Sort(new PriceDescComparer());
        Console.WriteLine("--- Каталог: сортировка по убыванию цены (Sort(new PriceDescComparer())) ---");
        foreach (var product in products)
            Console.WriteLine($"  {product}");
    }

    public void PrintSortedByNameLength()
    {
        // ЗАДАЧА: отсортируйте лямбдой подлине названия —
        // Sort((a, b) => a.Name.Length.CompareTo(b.Name.Length));
        // это делегат Comparison<Product>
        products.Sort((a, b) => a.Name.Length.CompareTo(b.Name.Length));

        // ЗАДАЧА: выведите результат и поясните в консоли, что тип Product не менялся
        Console.WriteLine("--- Каталог: сортировка по длине названия (лямбда Comparison<Product>) ---");
        foreach (var product in products)
            Console.WriteLine($"  {product}");
        Console.WriteLine("  Порядок задан только для этого вызова: тип Product не менялся.");
    }
}
