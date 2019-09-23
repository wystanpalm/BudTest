using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BudTest.IModel;

namespace BudTest.Model
{
    public class Country : ICountry
    {
        [RegularExpression("^[a-zA-z]{2,3}$")]
        public string CountryCode { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public string CapitalCity { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public bool IsValidCountryCode
        {
            get
            {
                var context = new ValidationContext(this, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                return Validator.TryValidateObject(
                    this, context, results,
                    validateAllProperties: true);
            }
        }
    }
}
