using System;

namespace Foundation2
{
    public class Customer
    {
        // Private member variables
        private string _name;
        private Address _address;

        // Constructor
        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        // Method to check if the customer is in the USA
        public bool IsInUSA()
        {
            return _address.IsInUSA();
        }

        // Getter methods
        public string GetName()
        {
            return _name;
        }

        public Address GetAddress()
        {
            return _address;
        }
    }
}
