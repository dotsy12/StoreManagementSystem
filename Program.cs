using ContactPro;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace MyApp
{
    class Program
    {
        // القوائم الأساسية
        static Store newStore = new Store();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("1. Product Management");
                Console.WriteLine("2. Customer Management");
                Console.WriteLine("3. Order Management");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option (1-4): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ManageProducts();
                        break;
                    case "2":
                        ManageCustomers();
                        break;
                    case "3":
                        ManageOrders();
                        break;
                    case "4":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }

        }

        static void ManageProducts()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Management ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. View Products");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        newStore.AddProduct();
                        break;
                    case "2":
                        newStore.RemoveProduct();
                        break;
                    case "3":
                         newStore.UpdateProduct();
                        break;
                    case "4":
                        newStore.DisplayProducts();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void ManageCustomers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Customer Management ===");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. View Customers");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option (1-3): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        newStore.AddCustomer();
                        break;
                    case "2":
                        newStore.DisplayCustomers();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void ManageOrders()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Order Management ===");
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. View Orders");
                Console.WriteLine("3. View Total of Order");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option (1-3): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        newStore.AddOrders();
                        break;
                    case "2":
                        newStore.DisplayOrders();
                        break;
                    case "3":
                        newStore.DisplayOrdersTotalPrice();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }


    }

}
