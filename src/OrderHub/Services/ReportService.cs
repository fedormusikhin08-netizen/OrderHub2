using System.Collections.Generic;
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
    // ЗАДАЧА: метод принимает IEnumerable<Product> products и decimal threshold
    public List<string> ExpensiveProducts(IEnumerable<Product> products, decimal threshold)
    {
        // ЗАДАЧА1: отфильтруйте товары дешевле порога через Where
        // ЗАДАЧА2: отсортируйте по цене через OrderBy
        // ЗАДАЧА3: спроецируйте каждый товар в строку через Select
        // подсказка: строка должна содержать название и цену,
        // например $"{p.Name} — {p.Price:0.00}₽"
        // ЗАДАЧА4: материализуйте результат вызовом ToList() и верните его
        // переписывать это обычным foreach с if внутри нельзя — цель этапа именно LINQ
        return products
            .Where(p => p.Price > threshold)
            .OrderBy(p => p.Price)
            .Select(p => $"{p.Name} — {p.Price:0.00}₽")
            .ToList();
    }

    public int CountExpensive(IEnumerable<Product> products, decimal threshold)
    {
        // ЗАДАЧА: верните количество товаров дороже порога одним вызовом Count
        // подсказка: products.Count(p => p.Price > threshold)
        return products.Count(p => p.Price > threshold);
    }

    public bool HasVeryExpensive(IEnumerable<Product> products, decimal threshold)
    {
        // ЗАДАЧА: верните результат Any — есть лихоть один товар дороже порога
        return products.Any(p => p.Price > threshold);
    }

    public bool AllInStock(IEnumerable<Product> products)
    {
        // ЗАДАЧА: через All верните true, только если у всех товаров цена больше нуля
        return products.All(p => p.Price > 0);
    }

    public Product? Cheapest(IEnumerable<Product> products)
    {
        // ЗАДАЧА: найдите самый дешёвый товар
        // подсказка: сортировать не нужно — есть OrderBy(p => p.Price).First(),
        // либо FirstOrDefault, чтобы не бросать исключение на пустой коллекции
        return products.OrderBy(p => p.Price).FirstOrDefault();
    }

    // ---- Этап 6. КТ36: проекции, фильтрация, группировка ----

    // ЗАДАЧА: метод принимает IEnumerable<Product> и возвращает IEnumerable<CategorySummary>
    public List<CategorySummary> SummarizeByCategory(IEnumerable<Product> products)
    {
        // ЗАДАЧА1: сгруппируйте товары по полю Category через GroupBy
        // ЗАДАЧА2: для каждой группы посчитайте количество товаров (g.Count())
        // ЗАДАЧА3: посчитайте среднюю цену (g.Average(p => p.Price))
        // ЗАДАЧА4: спроецируйте результат в CategorySummary с полями
        // Category = g.Key, Count = …, AveragePrice = …
        // ЗАДАЧА5: отсортируйте сводку по убыванию количества товаров через OrderByDescending
        // ЗАДАЧА6: материализуйте через ToList()
        // ВАЖНО: агрегаты обязаны считаться LINQ-методами прямо на группе;
        // ручной цикл со счётчиком не засчитывается
        return products
            .GroupBy(p => p.Category)
            .Select(g => new CategorySummary
            {
                Category = g.Key,
                Count = g.Count(),
                AveragePrice = g.Average(p => p.Price)
            })
            .OrderByDescending(s => s.Count)
            .ToList();
    }

    public CategorySummary? MostExpensiveCategory(IEnumerable<Product> products)
    {
        // ЗАДАЧА: найдите самую дорогую категорию (средняя цена максимальна)
        // подсказка: OrderByDescending(s => s.AveragePrice).First()
        return SummarizeByCategory(products)
            .OrderByDescending(s => s.AveragePrice)
            .FirstOrDefault();
    }

    public List<string> CategoriesWithAtLeast(IEnumerable<Product> products, int minCount)
    {
        // ЗАДАЧА: найдите категории, где товаров не меньше заданного числа,
        // и спроецируйте только названия таких категорий через Select
        return SummarizeByCategory(products)
            .Where(s => s.Count >= minCount)
            .Select(s => s.Category)
            .ToList();
    }

}
