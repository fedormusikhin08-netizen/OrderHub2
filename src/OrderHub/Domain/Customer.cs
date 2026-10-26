namespace OrderHub.Domain;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string City { get; set; } = "";

    // ЗАДАЧА: добавьте конструктор и ToString()
    public Customer(int id, string name, string city)
    {
        Id = id;
        Name = name;
        City = city;
    }

    public override string ToString() => $"{Name} ({City})";
}
