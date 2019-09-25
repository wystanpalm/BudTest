using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BudTest.IData;
using BudTest.IModel;
using BudTest.Model;
using BudTest.Data.Entity;
using System.Text;

namespace BudTest.Data
{
    public class WorldBankXmlDeserializer : IWorldBankData
    {
        private HttpClient client;

        public WorldBankXmlDeserializer()
        {
            // Could inject this dependency in future, along with a CountryFactory
            this.client = new HttpClient();
        }

        public async Task<ICountry> FindCountry(string countryCode)
        {
            this.ValidateInput(new Country { CountryCode = countryCode });

            var responseStream = await this.client.GetStreamAsync(string.Format("http://api.worldbank.org/v2/country/{0}", countryCode));

            XDocument xDocument = XDocument.Load(responseStream);
            if (!this.ValidateResponse(xDocument))
            {
                return null;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WbCountries));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("wb", "http://www.worldbank.org");

            WbCountries wbCountries = xmlSerializer.Deserialize(xDocument.CreateReader()) as WbCountries;

            return wbCountries.Single().AsCountry();
        }

        private bool ValidateResponse(XDocument xDocument)
        {
            if (xDocument.Root.Name.LocalName != "countries")
            {
                // Assume we were returned an error response
                return false;
            }

            if (xDocument.Root.Elements().Count() != 1)
            {
                // We're expecting only one result
                return false;
            }

            return true;
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
