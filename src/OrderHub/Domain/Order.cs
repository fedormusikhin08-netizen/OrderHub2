namespace OrderHub.Domain;

public class Order
{
    // ЗАДАЧА: добавьте конструктор с параметрами Id, CustomerName, ProductName, Price, CreatedAt
    // ЗАДАЧА: объявите свойства только для чтения: Id, CustomerName, ProductName, Price, CreatedAt
    // подсказка: DateTime создаётся вызовом DateTime.Now, тип decimal — для денежных сумм
    public int Id { get; }
    public string CustomerName { get; }
    public string ProductName { get; }
    public decimal Price { get; }
    public DateTime CreatedAt { get; }

    public Order(int id, string customerName, string productName, decimal price, DateTime createdAt)
    {
        Id = id;
        CustomerName = customerName;
        ProductName = productName;
        Price = price;
        CreatedAt = createdAt;
    }

    // ЗАДАЧА: переопределите ToString() так, чтобы вывод был вида
    // "#12 Иванов — Клавиатура 5490,00₽ (12.03.2026)"
    // подсказка: используйте интерполяцию $"...", форматирование даты — d.ToString("dd.MM.yyyy")
    public override string ToString() =>
        $"#{Id} {CustomerName} — {ProductName} {Price:0.00}₽ ({CreatedAt:dd.MM.yyyy})";
}
