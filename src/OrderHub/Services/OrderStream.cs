using OrderHub.Domain;

namespace OrderHub.Services;

public class OrderStream
{
    // ЗАДАЧА1: бесконечный генератор заказов с yield return и трассировкой
    public static IEnumerable<Order> Orders() => throw new NotImplementedException();

    // ЗАДАЧА3: конечный итератор с фильтром по порогу
    public static IEnumerable<Order> Expensive(int limit, decimal threshold) => throw new NotImplementedException();
}
