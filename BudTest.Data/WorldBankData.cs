using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BudTest.Data.Entity;
using BudTest.IData;
using BudTest.IModel;

namespace BudTest.Data
{
    public class WorldBankData : IWorldBankData
    {
        public async Task<ICountry> FindCountry(string countryCode)
        {
            this.ValidateInput(countryCode);

            var client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(WorldBankCountry[][]));

            var countryResponse = await client.GetStreamAsync(string.Format("http://api.worldbank.org/v2/country/{0}?format=json", countryCode));

            WorldBankCountry[][] response = serializer.ReadObject(countryResponse) as WorldBankCountry[][];
            
            if (!this.ValidateResponse(response))
            {
                return null;
            }

            return response[1][0].MapToCountry();
        }

        private bool ValidateResponse(WorldBankCountry[][] response)
        {
            return (response.Length == 2);
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
