using ContactPro;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Store
{
    const string ProductPath = "ProductData.txt";
    const string CustomerPath = "CustomerData.txt";
    const string OrderPath = "OrderData.txt";
    public List<Product> Products { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Order> Orders { get; set; }

    public Store()
    {
        try
        {
            Products = DataBaseMangment.LoadData<Product>(ProductPath) ?? new List<Product>();
            Orders = DataBaseMangment.LoadData<Order>(OrderPath) ?? new List<Order>();
            Customers = DataBaseMangment.LoadData<Customer>(CustomerPath) ?? new List<Customer>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Store: {ex.Message}");
            Products = new List<Product>();
            Orders = new List<Order>();
            Customers = new List<Customer>();
        }
    }
    public void AddProduct()
    {
        if (Products == null) Products = new List<Product>();
        Product newProduct = new Product();

        Console.Write("Enter Product ID: ");
        int id;
        while (true)
        {
            Console.Write("Enter Product ID: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out id))
            {
                if (Products != null && Products.Any(p => p.ProductId == id))
                {
                    Console.WriteLine("❌ Error! This Product ID already exists. Please enter a unique ID.");
                }
                else
                {
                    break; // ✅ خرج من الحلقة عند الإدخال الصحيح

                }

            }
            else
            {
                Console.WriteLine("❌ Error! Please enter a valid numeric Product ID.");
            }
        }

        newProduct.ProductId = id;

        Console.Write("Enter Product Name: ");
        newProduct.ProductName = Console.ReadLine();

        Console.Write("Enter Product Description: ");
        newProduct.Description = Console.ReadLine();

        Console.Write("Enter Product Price: ");
        int price;
        while (!int.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Error! Enter a valid product price: ");
        }

        newProduct.Price = price;

        Console.Write("Enter Product Quantity: ");
        int quantity;
        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.Write("Error! Enter a valid product quantity : ");
        }
        newProduct.Quantity = quantity;

        Products.Add(newProduct);
        DataBaseMangment.SaveData(Products, ProductPath);
        Console.WriteLine($"✅ Product '{newProduct.ProductName}' added successfully!");
        Console.ReadKey();
    }
    public void AddCustomer()
    {
        if (Customers == null) Customers = new List<Customer>();

        Customer newcustomer = new Customer();
        Console.Write("Enter Customer ID: ");
        int id;
        while (true)
        {
            Console.Write("Enter Customer ID: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out id))
            {
                if (Customers != null && Customers.Any(c => c.CustomerId == id))
                {
                    Console.WriteLine("❌ Error! This Customer ID already exists. Please enter a unique ID.");

                }
                else
                {

                    break; // ✅ خرج من الحلقة عند الإدخال الصحيح
                }
            }
            else
            {
                Console.WriteLine("❌ Error! Please enter a valid numeric Customer ID.");
            }
        }

        newcustomer.CustomerId = id;
        Console.Write("Name:");
        newcustomer.CustomerName = Console.ReadLine();
        Console.Write("Phone:");
        newcustomer.Phone = Console.ReadLine();
        Console.Write("Email:");
        newcustomer.Email = Console.ReadLine();
        Customers.Add(newcustomer);
        DataBaseMangment.SaveData(Customers, CustomerPath);
        Console.WriteLine($"✅ Customer '{newcustomer.CustomerName}' added successfully!");
        Console.ReadKey();
    }
    public void AddOrders()
    {
        if (Orders == null) Orders = new List<Order>();
        if (Customers.Count == 0 || Products.Count == 0)
        {
            Console.WriteLine("⚠ No customers or products available to create an order!");
            return;
        }

        Order newOrder = new Order();

        Console.Write("📝 Enter order name: ");
        newOrder.OrderName = Console.ReadLine();

        // عرض العملاء المتاحين
        Console.WriteLine("\n👤 Available customers:");
        foreach (var customer in Customers)
        {
            Console.WriteLine($" CustomerID: {customer.CustomerId} || Name:{customer.CustomerName}");
        }

        // اختيار العميل
        Console.Write("🔹 Enter customer ID: ");
        int customerId;
        while (!int.TryParse(Console.ReadLine(), out customerId))
        {
            Console.WriteLine("❌ Invalid input. Please enter a valid customer ID.");
            Console.Write("🔹 Enter customer ID: ");
        }

        newOrder.customer = Customers.Find(c => c.CustomerId == customerId);
        if (newOrder.customer == null)
        {
            Console.WriteLine("❌ Invalid customer ID.");
            return;
        }

        // اختيار المنتجات
        newOrder.Products = new List<Product>();
        Console.WriteLine("\n📦 Available products:");
        foreach (var product in Products)
        {
            Console.WriteLine($"ID: {product.ProductId} | Name: {product.ProductName} | Price: {product.Price}");
        }

        while (true)
        {
            Console.Write("🔹 Enter product ID to add to the order (or 0 to finish selection): ");
            int productId;
            while (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("❌ Invalid input. Please enter a valid product ID.");
                Console.Write("🔹 Enter product ID: ");
            }

            if (productId == 0) break;

            Product selectedProduct = Products.Find(p => p.ProductId == productId);
            if (selectedProduct != null)
            {
                newOrder.Products.Add(selectedProduct);
            }
            else
            {
                Console.WriteLine("❌ Invalid product ID.");
            }
        }

        newOrder.OrderDate = DateTime.Now;
        newOrder.CalculateTotal();
        Orders.Add(newOrder);
        DataBaseMangment.SaveData(Orders, OrderPath);
        Console.WriteLine("✅ Order created successfully!\n");
        Console.ReadKey();
    }
    public void DisplayOrders()
    {
        if (Orders == null || !Orders.Any())
        {
            Console.WriteLine("⚠ No orders available to display!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n📋 Orders List:");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine("All Orders 📝 ");
        Console.WriteLine(new string('-', 50));

        foreach (var order in Orders)
        {
            try
            {
                order.DisplayOrderDetails();
                Console.WriteLine($"💰 Total Price: ${order.CalculateTotal()}");
                Console.WriteLine(new string('-', 50));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying order: {ex.Message}");
            }
        }

        Console.ReadKey();
    }
    public void DisplayOrdersTotalPrice()
    {
        decimal total = 0;
        if (Orders.Count == 0)
        {
            Console.WriteLine($"Orders Total Price = {total} $");
            Console.ReadKey();
            return;
        }

        foreach (var order in Orders)
        {
            total += order.CalculateTotal();
        }
        Console.WriteLine($"Orders Total Price = {total} $");
        Console.ReadKey();

    }

    public void DisplayProducts()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("⚠ No Product available to display!");
            Console.ReadKey();
            return;

        }
        Console.WriteLine("\n📦 Available Products:");
        Console.WriteLine(new string('-', 60));

        foreach (var product in Products)
        {
            product.DisplayDetails();
        }
        Console.ReadKey();


    }

    public void DisplayCustomers()
    {
        if (Customers == null || !Customers.Any())
        {
            Console.WriteLine("⚠ No Customers available to display!");
            return;
        }

        Console.WriteLine("\n📦 Available Customer:");
        Console.WriteLine(new string('-', 60));

        foreach (var customer in Customers)
        {
            customer.DisplayDetails();
        }

        Console.ReadKey();
    }
    public void RemoveProduct()
    {

        if (Products.Count == 0)
        {
            Console.WriteLine("⚠ No products available to remove!\r\n");
            Console.ReadKey();
            return;

        }
        Console.WriteLine("\n🗑 Remove Product");
        Console.Write("🔹 Enter Product ID to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ invalid input! please enter a valid number.");
            return;
        }
        Product ProductRemove = Products.Find(p => p.ProductId == id);
        if (ProductRemove == null)
        {
            Console.WriteLine("❌ product not found!");
            return;

        }
        Console.Write($"Are you sure you want to delete {ProductRemove.ProductName}? (y/n): ");
        if (Console.ReadLine().ToLower() != "y") return;
        Products.Remove(ProductRemove);
        DataBaseMangment.SaveData(Products, ProductPath);
        Console.WriteLine($"✅ product '{ProductRemove.ProductName}' removed successfully!");
        Console.ReadKey();

    }
    public void UpdateProduct()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("⚠ No products available to update!\r\n");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n✏️ Update Product");
        Console.Write("🔹 Enter Product ID to update: ");

        int id;
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("❌ Invalid input! Please enter a valid number.");
            Console.Write("🔹 Enter Product ID: ");
        }

        Product productToUpdate = Products.Find(p => p.ProductId == id);
        if (productToUpdate == null)
        {
            Console.WriteLine("❌ Product not found!");
            return;
        }

        Console.WriteLine("\n📌 Current Product Details:");
        productToUpdate.DisplayDetails();

        Console.WriteLine("\n🔄 Enter new details (leave empty to keep current values):");

        Console.Write($"📛 Name ({productToUpdate.ProductName}): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            productToUpdate.ProductName = newName;
        }

        Console.Write($"📝 Description ({productToUpdate.Description}): ");
        string newDescription = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDescription))
        {
            productToUpdate.Description = newDescription;
        }

        Console.Write($"💰 Price ({productToUpdate.Price}): ");
        string newPriceInput = Console.ReadLine();
        if (int.TryParse(newPriceInput, out int newPrice))
        {
            productToUpdate.Price = newPrice;
        }

        Console.Write($"📦 Quantity ({productToUpdate.Quantity}): ");
        string newQuantityInput = Console.ReadLine();
        if (int.TryParse(newQuantityInput, out int newQuantity))
        {
            productToUpdate.Quantity = newQuantity;
        }

        DataBaseMangment.SaveData(Products, ProductPath);
        Console.WriteLine($"✅ Product '{productToUpdate.ProductName}' updated successfully!");
        Console.ReadKey();
    }
}