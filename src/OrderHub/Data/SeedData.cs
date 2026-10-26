using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Data;

// ЗАДАЧА: начальные данные всех этапов — заказы для истории и отчётов
public static class SeedData
{
    public static Order[] CreateOrders() => new Order[]
    {
        new Order(1, "Иванов",    "Клавиатура",  5490m, new DateTime(2026, 3, 12)),
        new Order(2, "Иванов",    "Монитор",    18990m, new DateTime(2026, 3, 15)),
        new Order(3, "Петрова",   "Мышь",        1290m, new DateTime(2026, 3, 16)),
        new Order(4, "Петрова",   "Ноутбук",    74990m, new DateTime(2026, 3, 20)),
        new Order(5, "Сидоров",   "Наушники",    4500m, new DateTime(2026, 3, 22)),
        new Order(6, "Сидоров",   "Клавиатура",  6990m, new DateTime(2026, 4, 1)),
        new Order(7, "Кузнецова", "Монитор",    21990m, new DateTime(2025, 4, 5)),
        new Order(8, "Иванов",    "Подставка",    990m, new DateTime(2026, 4, 10)),
    };

}
