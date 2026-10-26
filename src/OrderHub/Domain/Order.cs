namespace OrderHub.Domain;

public class Order
{
    // ЗАДАЧА: добавьте конструктор с параметрами Id, CustomerName, ProductName, Price, CreatedAt
    // ЗАДАЧА: объявите свойства только для чтения: Id, CustomerName, ProductName, Price, CreatedAt
    // ЗАДАЧА: переопределите ToString() так, чтобы вывод был вида
    // "#12 Иванов — Клавиатура 5490,00₽ (12.03.2026)"
    public override string ToString() => throw new NotImplementedException();
}
