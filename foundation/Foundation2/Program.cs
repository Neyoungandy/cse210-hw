using System;

namespace Foundation2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Address objects
            Address address1 = new Address("123 Elm St", "New York", "NY", "USA");
            Address address2 = new Address("456 Oak St", "Toronto", "ON", "Canada");

            // Create Customer objects
            Customer customer1 = new Customer("Andrew Mogbeyiromore", address1);
            Customer customer2 = new Customer("Gift Andre", address2);

            // Create Product objects
            Product product1 = new Product("Laptop", "P001", 800.00, 1);
            Product product2 = new Product("Mouse", "P002", 25.00, 2);
            Product product3 = new Product("Keyboard", "P003", 50.00, 1);
            Product product4 = new Product("Monitor", "P004", 200.00, 1);

            // Create Order objects and add products
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);

            // List of orders
            var orders = new List<Order> { order1, order2 };

            // Display information for each order
            foreach (var order in orders)
            {
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Cost: ${order.GetTotalCost():F2}\n");
            }

            // Keep the console window open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
