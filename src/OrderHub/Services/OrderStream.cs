using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class OrderStream
{
    // ЗАДАЧА1: реализуйте бесконечный генератор заказов
    // public static IEnumerable<Order> Orders()
    // подсказка по алгоритму: счётчик Id растёт на 1, клиенты берутся по кругу из массива,
    // цена тоже увеличивается; обязательно while (true)
    // ЗАДАЧА2: перед каждым yield return добавьте Console.WriteLine с текстом
    // $"Готовлю заказ №{n}" — так вы увидите момент фактического выполнения
    public static IEnumerable<Order> Orders()
    {
        string[] customers = { "Иванов", "Петрова", "Сидоров", "Кузнецова" };
        int n = 0;
        decimal price = 1000m;

        while (true)
        {
            n++;
            price += 500m;
            Console.WriteLine($"Готовлю заказ №{n}");
            yield return new Order(n, customers[(n - 1) % customers.Length], $"Товар №{n}", price, DateTime.Now);
        }
    }

    // ЗАДАЧА3: реализуйте конечный итератор с фильтром
    // public static IEnumerable<Order> Expensive(int limit, decimal threshold)
    // подсказка: обычный for, внутри проверка условия и continue;
    // при недостижении порога пропускайте значение — yield return не вызывается
    public static IEnumerable<Order> Expensive(int limit, decimal threshold)
    {
        string[] customers = { "Иванов", "Петрова", "Сидоров", "Кузнецова" };

        for (int i = 1; i <= limit; i++)
        {
            decimal price = i * 2500m;
            if (price < threshold)
                continue;

            Console.WriteLine($"Готовлю дорогой заказ №{i} ({price:0.00}₽)");
            yield return new Order(i, customers[(i - 1) % customers.Length], $"Товар №{i}", price, DateTime.Now);
        }
    }
}
