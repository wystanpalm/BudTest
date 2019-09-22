using System;
using BudTest.IModel;

namespace BudTest.Model
{
    public class Country : ICountry
    {
        public string Name { get; set; }

        public string Region { get; set; }

        public string CapitalCity { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
