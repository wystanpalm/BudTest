using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using BudTest.IData;
using BudTest.IModel;
using BudTest.Model;

namespace BudTest.Data
{
    public class WorldBankData : IWorldBankData
    {
        private HttpClient client;

        public WorldBankData()
        {
            // Could inject this dependency in future, along with a CountryFactory
            this.client = new HttpClient();
        }

    public async Task<ICountry> FindCountry(string countryCode)
        {
            ICountry country = new Country { CountryCode = countryCode };
            this.ValidateInput(country);

            var responseString = await this.client.GetStringAsync(string.Format("http://api.worldbank.org/v2/country/{0}", countryCode));

            XDocument xDocument = XDocument.Parse(responseString);            
            if (!this.ValidateResponse(xDocument))
            {
                return null;
            }

            // Slightly optomistic here that there won't ever be two search results
            // could two countries share two letters of a three-letter country code
            // and if so, would the World Bank service return both? 
            return this.BuildCountry(country, xDocument.Root.Elements().Single());
        }

        private bool ValidateResponse(XDocument xDocument)
        {
            return xDocument.Root.Name.LocalName == "countries";
        }

        private ICountry BuildCountry(ICountry country, XElement countryElement)
        {
            country.Name = countryElement.Elements().Where(e => e.Name.LocalName == "name").Single().Value;
            country.Region = countryElement.Elements().Where(e => e.Name.LocalName == "region").Single().Value;
            country.CapitalCity = countryElement.Elements().Where(e => e.Name.LocalName == "capitalCity").Single().Value;
            country.Longitude = decimal.Parse(countryElement.Elements().Where(e => e.Name.LocalName == "longitude").Single().Value);
            country.Latitude = decimal.Parse(countryElement.Elements().Where(e => e.Name.LocalName == "latitude").Single().Value);

            return country;
        }

        private void ValidateInput(ICountry country)
        {
            if (!country.IsValidCountryCode)
            {
                throw new ArgumentException("Country code must be a two or three character country code.");
            }
        }
    }
}
