namespace OrderHub.Domain;

public class Product : IComparable<Product>
{
    public string Name { get; private set; } = "";
    public decimal Price { get; private set; }
    public string Category { get; private set; } = "";

    // ЗАДАЧА: объявите конструктор с параметрами Name, Price, Category
    // ЗАДАЧА: переопределите свойства, чтобы они были только для чтения (private set)
    public Product(string name, decimal price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }

    public int CompareTo(Product? other)
    {
        // ЗАДАЧА: верните результат сравнения по цене — естественный порядок
        // подсказка: other может быть null, предусмотрите этот случай,
        // например так: if (other is null) return 1;
        // подсказка: цены типа decimal, у них тоже есть CompareTo
        if (other is null) return 1;
        return Price.CompareTo(other.Price);
    }

    public override string ToString() => $"{Name} — {Price:0.00}₽ ({Category})";
}

// ЗАДАЧА: объявите класс NameComparer : IComparer<Product>
// ЗАДАЧА: реализуйте метод Compare так, чтобы сортировка шла по алфавиту названий
// подсказка: string.Compare(a?.Name, b?.Name) возвращает int,
// но для CultureCompare надёжнее: string.Compare(a?.Name, b?.Name, StringComparison.Ordinal)
public class NameComparer : IComparer<Product>
{
    public int Compare(Product? a, Product? b) =>
        string.Compare(a?.Name, b?.Name, StringComparison.Ordinal);
}

// ЗАДАЧА: объявите класс PriceDescComparer : IComparer<Product> — по убыванию цены
// подсказка: b.Price.CompareTo(a.Price), обратный порядок получается перестановкой операндов
public class PriceDescComparer : IComparer<Product>
{
    public int Compare(Product? a, Product? b)
    {
        if (a is null) return b is null ? 0 : -1;
        if (b is null) return 1;
        return b.Price.CompareTo(a.Price);
    }
}
