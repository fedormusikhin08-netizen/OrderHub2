using OrderHub.Data;
using OrderHub.Services;

Console.WriteLine("=== OrderHub — учёт заказов интернет-магазина ===");

var orders = new OrderService();
var warehouse = new WarehouseService();
var catalog = new CatalogService();

var products = SeedData.CreateProducts();
foreach (var product in products)
    catalog.AddProduct(product.Name, product.Price, product.Category);

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

    // ---- КТ33: склад на Dictionary ----
    Console.WriteLine("\n--- КТ33: склад товаров (Dictionary<string, int>) ---");
    warehouse.Receive("Клавиатура", 10);
    warehouse.Receive("Клавиатура", 5);   // количество увеличивается, а не заводится вторая запись
    warehouse.Receive("Монитор", 3);
    warehouse.Sell("Монитор", 1);
    warehouse.Sell("Монитор", 99);        // предупреждение: товара недостаточно
    warehouse.Sell("Проектор", 1);        // сообщение: товара нет на складе, без исключения
    warehouse.PrintStock();
    if (warehouse.TryGetStock("Клавиатура", out var count))
        Console.WriteLine($"TryGetStock(\"Клавиатура\") → {count} шт.");
    if (!warehouse.TryGetStock("Проектор", out _))
        Console.WriteLine("TryGetStock(\"Проектор\") → false, остаток 0");

    // ---- КТ34: три способа сортировки каталога ----
    Console.WriteLine("\n--- КТ34: сортировка каталога (IComparable, IComparer, лямбда) ---");
    catalog.PrintAll();
    catalog.PrintSortedByNaturalOrder();          // IComparable<Product>: по возрастанию цены
    catalog.PrintSortedByName();                  // NameComparer: по алфавиту
    catalog.PrintSortedByNameLength();            // лямбда Comparison<T>: по длине названия
    catalog.PrintSortedByPriceDesc();             // PriceDescComparer: по убыванию цены
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
    Console.WriteLine("5. Поступление товара на склад");
    Console.WriteLine("6. Продажа товара");
    Console.WriteLine("7. Каталог: сортировка");
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
        case "5":
            Console.Write("Название товара: ");
            var stockName = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Количество: ");
            var stockQty = int.TryParse(Console.ReadLine(), out var q) ? q : 1;
            warehouse.Receive(stockName, stockQty);
            warehouse.PrintStock();
            break;
        case "6":
            Console.Write("Название товара: ");
            var sellName = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Количество: ");
            var sellQty = int.TryParse(Console.ReadLine(), out var sq) ? sq : 1;
            warehouse.Sell(sellName, sellQty);
            warehouse.PrintStock();
            break;
        case "7":
            catalog.PrintAll();
            catalog.PrintSortedByNaturalOrder();
            catalog.PrintSortedByName();
            catalog.PrintSortedByNameLength();
            catalog.PrintSortedByPriceDesc();
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
