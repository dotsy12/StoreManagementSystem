class Customer
{
    private string _customerName;   
    private int _customerId;
    private string _email;  
    private string _phone;

    public string CustomerName { get => _customerName; set => _customerName = value; }
    public int CustomerId { get => _customerId; set => _customerId = value; }
    public string Email{ get => _email; set => _email = value; }
    public string Phone { get => _phone;    set => _phone = value; }

    public void DisplayDetails()
    {
        Console.WriteLine($"🆔 ID: {CustomerId}");
        Console.WriteLine($"📌 Name: {CustomerName}");
        Console.WriteLine($"📝 Email: {Email}");
        Console.WriteLine($"💰 Phone: ${Phone}");
        Console.WriteLine(new string('-', 60));
    }


}
    
