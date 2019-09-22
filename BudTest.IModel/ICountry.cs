using System;

namespace BudTest.IModel
{
    public interface ICountry
    {
        string Name { get; set; }

        string Region { get; set; }

        string CapitalCity { get; set; }

        decimal Longitude { get; set; }

        decimal Latitude { get; set; }
    }
}
