using OrderHub.Domain;

namespace OrderHub.Services;

public class OrderService
{
    // ЗАДАЧА: объявите три коллекции с точной сигнатурой:
    // List<Order> allOrders — все заказы
    // Queue<Order> pendingQueue — заказы, ожидающие обработки, в порядке поступления
    // Stack<string> undoHistory — история последних действий для отмены

    public Order CreateOrder(string customerName, string productName, decimal price)
        => throw new NotImplementedException();

    public Order? ProcessNext() => throw new NotImplementedException();

    public bool UndoLastAction() => throw new NotImplementedException();

    // ЗАДАЧА: допишите три метода-отчёта, используя foreach
    public void PrintAllOrders() => throw new NotImplementedException();
    public void PrintPendingQueue() => throw new NotImplementedException();
    public void PrintRecentActions() => throw new NotImplementedException();

    // ---- Этап 2. КТ32: история заказов клиента на LinkedList<T> ----

    // ЗАДАЧА: объявите поле истории и узел текущего заказа
    public void InitHistory(params Order[] orders) => throw new NotImplementedException();
    public bool PlaceOrderAfterCurrent(Order order) => throw new NotImplementedException();
    public bool TryFindOrder(string customerName, out Order? order) => throw new NotImplementedException();
    public void PrintHistory() => throw new NotImplementedException();
}
