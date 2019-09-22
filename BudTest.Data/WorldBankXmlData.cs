using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using BudTest.IData;
using BudTest.IModel;
using BudTest.Model;

namespace BudTest.Data
{
    public class WorldBankXmlData : IWorldBankData
    {
        private HttpClient client = new HttpClient();

        public async Task<ICountry> FindCountry(string countryCode)
        {
            this.ValidateInput(countryCode);

            var responseString = await this.client.GetStringAsync(string.Format("http://api.worldbank.org/v2/country/{0}", countryCode));

            XDocument xDocument = XDocument.Parse(responseString);

            if (xDocument.Root.Name.LocalName != "countries")
            {
                return null;
            }

            return this.BuildCountry(xDocument.Root.Elements().Single());
        }

        private Country BuildCountry(XElement countryElement)
        {
            Country country = new Country();

            country.Name = countryElement.Elements().Where(e => e.Name.LocalName == "name").Single().Value;
            country.Region = countryElement.Elements().Where(e => e.Name.LocalName == "region").Single().Value;
            country.CapitalCity = countryElement.Elements().Where(e => e.Name.LocalName == "capitalCity").Single().Value;
            country.Longitude = decimal.Parse(countryElement.Elements().Where(e => e.Name.LocalName == "longitude").Single().Value);
            country.Latitude = decimal.Parse(countryElement.Elements().Where(e => e.Name.LocalName == "latitude").Single().Value);

            return country;
        }

        private void ValidateInput(string countryCode)
        {
            Regex regex = new Regex("^[a-zA-z]{2,3}$");
            if (!regex.IsMatch(countryCode))
            {
                throw new ArgumentException("Country code must be a two or three character country code.");
            }
        }
    }
}
