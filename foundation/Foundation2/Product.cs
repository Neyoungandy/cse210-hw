using System;

namespace Foundation2
{
    public class Product
    {
        // Private member variables
        private string _name;
        private string _productId;
        private double _price;
        private int _quantity;

        // Constructor
        public Product(string name, string productId, double price, int quantity)
        {
            _name = name;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        // Method to calculate total cost of the product
        public double GetTotalCost()
        {
            return _price * _quantity;
        }

        // Getter methods
        public string GetName()
        {
            return _name;
        }

        public string GetProductId()
        {
            return _productId;
        }

        public double GetPrice()
        {
            return _price;
        }

        public int GetQuantity()
        {
            return _quantity;
        }
    }
}
