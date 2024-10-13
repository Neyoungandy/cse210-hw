using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation2
{
    public class Order
    {
        // Private member variables
        private List<Product> _products;
        private Customer _customer;

        // Constructor
        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        // Method to add a product to the order
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        // Method to calculate the total cost (products + shipping)
        public double GetTotalCost()
        {
            double total = 0;

            foreach (var product in _products)
            {
                total += product.GetTotalCost();
            }

            // Add shipping cost
            total += _customer.IsInUSA() ? 5 : 35;

            return total;
        }

        // Method to generate packing label
        public string GetPackingLabel()
        {
            StringBuilder packingLabel = new StringBuilder();
            packingLabel.AppendLine("Packing Label:");

            foreach (var product in _products)
            {
                packingLabel.AppendLine($"Product: {product.GetName()}, ID: {product.GetProductId()}");
            }

            return packingLabel.ToString();
        }

        // Method to generate shipping label
        public string GetShippingLabel()
        {
            StringBuilder shippingLabel = new StringBuilder();
            shippingLabel.AppendLine("Shipping Label:");
            shippingLabel.AppendLine(_customer.GetName());
            shippingLabel.AppendLine(_customer.GetAddress().GetFullAddress());

            return shippingLabel.ToString();
        }
    }
}
