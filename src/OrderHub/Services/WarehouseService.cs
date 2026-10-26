namespace OrderHub.Services;

public class WarehouseService
{
    // ЗАДАЧА: объявите Dictionary<string, int> stock — название товара → количество

    public void Receive(string productName, int quantity) => throw new NotImplementedException();
    public bool Sell(string productName, int quantity) => throw new NotImplementedException();
    public bool TryGetStock(string productName, out int quantity) => throw new NotImplementedException();
    public void PrintStock() => throw new NotImplementedException();
}
