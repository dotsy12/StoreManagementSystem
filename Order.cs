class Order
{
    private string _orderName;
    private DateTime _orderDate;

    public string OrderName { get => _orderName; set => _orderName = value; }
    public Customer customer { get; set; }
    public List<Product> Products { get; set; } // يجب تهيئة هذه القائمة
    public DateTime OrderDate { get => _orderDate; set => _orderDate = value; }

    // مُنشئ لتهيئة القائمة Products
    public Order()
    {
        Products = new List<Product>(); // تهيئة القائمة هنا
    }
  
        public decimal CalculateTotal()
        {
            decimal total = 0;
            if (Products != null) // التحقق من أن Products ليست null
            {
                foreach (var item in Products)
                {
                    total += item.Price;
                }
            }
            return total;
        }

    public void DisplayOrderDetails()
    {
        Console.WriteLine($"📌 Order Name: {OrderName}");
        Console.WriteLine($"📅 Order Date: {OrderDate}");

        if (customer != null) // التحقق من أن customer ليس null
        {
            Console.WriteLine($"👤 Customer: {customer.CustomerName} (ID: {customer.CustomerId})");
        }
        else
        {
            Console.WriteLine("👤 Customer: Not specified"); // رسالة بديلة إذا كان customer غير معين
        }

        Console.WriteLine("🛒 Products:");
        if (Products != null)
        {
            foreach (var item in Products)
            {
                Console.WriteLine($"   🔹 {item.ProductName} - ${item.Price}");
            }
        }
    }
}




