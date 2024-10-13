using System;

namespace Foundation2
{
    public class Address
    {
        // Private member variables
        private string _street;
        private string _city;
        private string _state;
        private string _country;

        // Constructor
        public Address(string street, string city, string state, string country)
        {
            _street = street;
            _city = city;
            _state = state;
            _country = country;
        }

        // Method to check if the address is in the USA
        public bool IsInUSA()
        {
            return _country.Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        // Method to get the full address
        public string GetFullAddress()
        {
            return $"{_street}\n{_city}, {_state}\n{_country}";
        }

        // Properties (optional, if you need to access individual fields)
        public string Street => _street;
        public string City => _city;
        public string State => _state;
        public string Country => _country;
    }
}
