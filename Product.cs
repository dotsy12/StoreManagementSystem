using System.Reflection.Metadata.Ecma335;

public class Product
{
    private int _productId;
    private string _productName;
    private string _description;
    private int _price;
    private int _quantity;

    public int ProductId { get => _productId; set => _productId = value; }
    public string ProductName { get => _productName; set => _productName = value; }

    public string Description { get => _description; set => _description = value; }

    public int Price { get => _price; set => _price = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }

    public void DisplayDetails()
    {
        Console.WriteLine($"🆔 ID: {ProductId}");
        Console.WriteLine($"📌 Name: {ProductName}");
        Console.WriteLine($"📝 Description: {Description}");
        Console.WriteLine($"💰 Price: ${Price}");
        Console.WriteLine($"📦 Quantity: {Quantity}");
        Console.WriteLine(new string('-', 60));
    }



}
