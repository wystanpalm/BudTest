using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using BudTest.Model;

namespace BudTest.Data.Entity
{
    [XmlType("country")]
    public class WbCountry
    {
        [XmlElement("iso2Code")]
        public string Iso2Code;

        [XmlElement("name")]
        public string Name;

        [XmlElement("region")]
        public string Region;

        [XmlElement("capitalCity")]
        public string CapitalCity;

        [XmlElement("longitude")]
        public string Longitude;

        [XmlElement("latitude")]
        public string Latitude;

        public Country AsCountry()
        {
            Country country = new Country {
                Name = this.Name,
                CapitalCity = this.CapitalCity,
                Region = this.Region,
                Longitude = decimal.Parse(this.Longitude),
                Latitude = decimal.Parse(this.Latitude)
            };

            return country;
        }
    }
}
