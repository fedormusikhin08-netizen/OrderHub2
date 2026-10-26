using OrderHub.Data;
using OrderHub.Services;

Console.WriteLine("=== OrderHub — учёт заказов интернет-магазина ===");

var orders = new OrderService();

void RunAllDemos()
{
    // ---- КТ31: очередь Queue, стек Stack, список List ----
    Console.WriteLine("\n--- КТ31: очередь Queue, стек Stack, список List ---");
    orders.CreateOrder("Иванов", "Клавиатура", 5490m);
    orders.CreateOrder("Петрова", "Монитор", 18990m);
    orders.CreateOrder("Сидоров", "Мышь", 1290m);
    orders.ProcessNext();
    orders.ProcessNext();
    orders.PrintAllOrders();
    orders.PrintPendingQueue();
    orders.PrintRecentActions();

    // ---- КТ32: история клиента на LinkedList ----
    Console.WriteLine("\n--- КТ32: история заказов клиента (LinkedList) ---");
    var seed = SeedData.CreateOrders();
    orders.InitHistory(seed[0], seed[1], seed[2]);
    orders.PlaceOrderAfterCurrent(seed[4]);
    orders.PrintHistory();
    if (orders.TryFindOrder("Иванов", out var found))
        Console.WriteLine($"Поиск «Иванов»: найден заказ {found}");
    else
        Console.WriteLine("Поиск «Иванов»: заказ не найден");
    if (!orders.TryFindOrder("Нет такого", out _))
        Console.WriteLine("Поиск «Нет такого»: false, без исключения");
}

RunAllDemos();

bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("Меню OrderHub:");
    Console.WriteLine("1. Добавить заказ в очередь");
    Console.WriteLine("2. Обработать следующий заказ");
    Console.WriteLine("3. Отменить последнее действие");
    Console.WriteLine("4. История заказов клиента");
    Console.WriteLine("0. Показать все отчёты");
    Console.WriteLine("q. Выход");
    Console.Write("Выбор: ");
    var choice = Console.ReadLine()?.Trim();
    switch (choice)
    {
        case "1":
            Console.Write("Имя клиента: ");
            var name = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Товар: ");
            var item = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Цена: ");
            var price = decimal.TryParse(Console.ReadLine(), out var parsed) ? parsed : 0m;
            orders.CreateOrder(name, item, price);
            break;
        case "2":
            orders.ProcessNext();
            break;
        case "3":
            orders.UndoLastAction();
            break;
        case "4":
            orders.PrintHistory();
            break;
        case "0":
            RunAllDemos();
            break;
        case "q":
            running = false;
            break;
        default:
            Console.WriteLine("Нет такого пункта меню.");
            break;
    }
}
Console.WriteLine("До встречи!");
