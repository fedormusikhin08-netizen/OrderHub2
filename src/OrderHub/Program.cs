using OrderHub.Data;
using OrderHub.Services;

Console.WriteLine("=== OrderHub — учёт заказов интернет-магазина ===");

var orders = new OrderService();
var warehouse = new WarehouseService();
var catalog = new CatalogService();

var products = SeedData.CreateProducts();
foreach (var product in products)
    catalog.AddProduct(product.Name, product.Price, product.Category);

var reports = new ReportService();

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

    // ---- КТ35: базовые LINQ-запросы ----
    Console.WriteLine("\n--- КТ35: Where → OrderBy → Select и агрегаты Count/Any/All/First ---");
    const decimal threshold = 5000m;
    Console.WriteLine($"Дороже {threshold:0.00}₽:");
    foreach (var line in reports.ExpensiveProducts(products, threshold))
        Console.WriteLine($"  {line}");
    Console.WriteLine($"CountExpensive(5000): {reports.CountExpensive(products, threshold)}");
    Console.WriteLine($"HasVeryExpensive(50000): {reports.HasVeryExpensive(products, 50000m)}");
    Console.WriteLine($"AllInStock: {reports.AllInStock(products)}");
    Console.WriteLine($"Cheapest: {reports.Cheapest(products)}");
    Console.WriteLine($"Порог изменён на 15000 → CountExpensive: {reports.CountExpensive(products, 15000m)} (код не менялся)");
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
    Console.WriteLine("8. Отчёт: дорогие товары");
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
        case "8":
            Console.Write("Порог цены: ");
            var limitPrice = decimal.TryParse(Console.ReadLine(), out var limit) ? limit : 5000m;
            foreach (var expensiveLine in reports.ExpensiveProducts(products, limitPrice))
                Console.WriteLine($"  {expensiveLine}");
            Console.WriteLine($"Дороже порога: {reports.CountExpensive(products, limitPrice)} шт.");
            Console.WriteLine($"Есть ли дороже 50000: {reports.HasVeryExpensive(products, 50000m)}");
            Console.WriteLine($"Самый дешёвый: {reports.Cheapest(products)}");
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
