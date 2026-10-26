using System.Collections.Generic;

namespace OrderHub.Services;

public class WarehouseService
{
    // ЗАДАЧА: объявите Dictionary<string, int> stock — название товара → количество
    // подсказка: поле инициализируется прямо при объявлении: = new Dictionary<string, int>();
    private readonly Dictionary<string, int> stock = new();

    public void Receive(string productName, int quantity)
    {
        // ЗАДАЧА1: проверьте наличие товара через TryGetValue
        if (stock.TryGetValue(productName, out var existing))
        {
            // ЗАДАЧА2: если товар есть — увеличьте количество (stock[productName] = existing + quantity)
            stock[productName] = existing + quantity;
        }
        else
        {
            // ЗАДАЧА3: если товара нет — добавьте новую запись через индексатор
            stock[productName] = quantity;
        }

        // ЗАДАЧА4: выведите итоговое количество товара на складе
        Console.WriteLine($"Поступление: {productName} → на складе {stock[productName]} шт.");
    }

    public bool Sell(string productName, int quantity)
    {
        // ЗАДАЧА1: если товара нет вообще — сообщите об этом и верните false
        // (никаких исключений: используйте TryGetValue)
        if (!stock.TryGetValue(productName, out var current))
        {
            Console.WriteLine($"Продажа невозможна: товара «{productName}» нет на складе.");
            return false;
        }

        // ЗАДАЧА2: если товара недостаточно — выведите предупреждение и верните false
        if (current < quantity)
        {
            Console.WriteLine($"Предупреждение: товара «{productName}» недостаточно (есть {current}, нужно {quantity}).");
            return false;
        }

        // ЗАДАЧА3: уменьшите остаток на quantity
        var rest = current - quantity;
        stock[productName] = rest;

        // ЗАДАЧА4: если остаток стал равен нулю — удалите запись через Remove
        // и сообщите, что товар снят с учёта
        if (rest == 0)
        {
            stock.Remove(productName);
            Console.WriteLine($"Продано: {productName} × {quantity}. Товар снят с учёта.");
            return true;
        }

        // ЗАДАЧА5: верните true
        Console.WriteLine($"Продано: {productName} × {quantity}, остаток {rest} шт.");
        return true;
    }

    public bool TryGetStock(string productName, out int quantity)
    {
        // ЗАДАЧА: реализуйте безопасный поиск остатка через TryGetValue
        // ЗАДАЧА: при отсутствии товара присвойте quantity = 0 — значение по умолчанию,
        // чтобы у вызывающего кода не было неинициализированной переменной
        if (stock.TryGetValue(productName, out var value))
        {
            quantity = value;
            return true;
        }

        quantity = 0;
        return false;
    }

    public void PrintStock()
    {
        Console.WriteLine("--- Склад (Dictionary<string, int>) ---");
        // ЗАДАЧА: переберите склад через foreach по KeyValuePair<string, int>
        // ЗАДАЧА: отсортируйте вывод по алфавиту (OrderBy можно использовать уже сейчас)
        if (stock.Count == 0)
        {
            Console.WriteLine("  Склад пуст.");
            return;
        }

        foreach (var pair in stock.OrderBy(p => p.Key, StringComparer.Ordinal))
            Console.WriteLine($"  {pair.Key}: {pair.Value} шт.");
    }
}
