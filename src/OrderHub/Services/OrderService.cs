using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class OrderService
{
    // ЗАДАЧА: объявите три коллекции с точной сигнатурой:
    // List<Order> allOrders — все заказы
    // Queue<Order> pendingQueue — заказы, ожидающие обработки, в порядке поступления
    // Stack<string> undoHistory — история последних действий для отмены
    // подсказка: для каждой коллекции создайте экземпляр прямо в поле,
    // например: private readonly Queue<Order> pendingQueue = new();
    private readonly List<Order> allOrders = new();
    private readonly Queue<Order> pendingQueue = new();
    private readonly Stack<string> undoHistory = new();

    private int nextId = 1;

    public Order CreateOrder(string customerName, string productName, decimal price)
    {
        // ЗАДАЧА1: создайте новый заказ (Id счётчику увеличивается на 1)
        var order = new Order(nextId++, customerName, productName, price, DateTime.Now);

        // ЗАДАЧА2: добавьте заказ в allOrders
        allOrders.Add(order);

        // ЗАДАЧА3: добавьте тот же заказ в pendingQueue методом Enqueue
        pendingQueue.Enqueue(order);

        // ЗАДАЧА4: запишите в undoHistory описание действия для возможной отмены
        // подсказка: описание удобно собрать так:
        // undoHistory.Push($"Создан заказ {order.Id} для {customerName}");
        undoHistory.Push($"Создан заказ {order.Id} для {customerName}");

        // ЗАДАЧА5: верните созданный заказ
        return order;
    }

    public Order? ProcessNext()
    {
        // ЗАДАЧА1: если pendingQueue пуст — верните null
        // подсказка: проверка emptiness — pendingQueue.Count == 0
        if (pendingQueue.Count == 0)
            return null;

        // ЗАДАЧА2: извлеките заказ из очереди методом Dequeue (именно он, а не Peek)
        var order = pendingQueue.Dequeue();

        // ЗАДАЧА3: запишите в undoHistory, какой заказ обработан
        undoHistory.Push($"Обработан заказ {order.Id} ({order.CustomerName})");

        // ЗАДАЧА4: выведите в консоль $"Обработан заказ: {order}"
        Console.WriteLine($"Обработан заказ: {order}");

        // ЗАДАЧА5: верните обработанный заказ
        return order;
    }

    public bool UndoLastAction()
    {
        // ЗАДАЧА1: если undoHistory пуст — верните false
        if (undoHistory.Count == 0)
            return false;

        // ЗАДАЧА2: извлеките последнее действие методом Pop
        var action = undoHistory.Pop();

        // ЗАДАЧА3: выведите $"Отменено действие: {action}" и верните true
        Console.WriteLine($"Отменено действие: {action}");
        return true;
    }

    // ЗАДАЧА: допишите три метода-отчёта, используя foreach:
    // PrintAllOrders() — все заказы из allOrders в порядке добавления
    public void PrintAllOrders()
    {
        Console.WriteLine("--- Все заказы (List<Order>) ---");
        foreach (var order in allOrders)
            Console.WriteLine($"  {order}");
        Console.WriteLine($"  Всего заказов: {allOrders.Count}");
    }

    // PrintPendingQueue() — сколько заказов ждёт и какой первый в очереди (Peek, без извлечения)
    public void PrintPendingQueue()
    {
        Console.WriteLine("--- Ожидают обработки (Queue<Order>) ---");
        if (pendingQueue.Count == 0)
        {
            Console.WriteLine("  Очередь пуста.");
            return;
        }
        Console.WriteLine($"  В очереди: {pendingQueue.Count}, первый: {pendingQueue.Peek()}");
        foreach (var order in pendingQueue)
            Console.WriteLine($"  в очереди: {order}");
    }

    // PrintRecentActions() — содержимое undoHistory (обход идёт от последнего к первому)
    public void PrintRecentActions()
    {
        Console.WriteLine("--- Последние действия (Stack<string>) ---");
        foreach (var action in undoHistory)
            Console.WriteLine($"  {action}");
    }

}
