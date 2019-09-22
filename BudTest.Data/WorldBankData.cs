using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
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
            var client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(WorldBankCountry[][]));

            var countryResponse = await client.GetStreamAsync(string.Format("http://api.worldbank.org/v2/country/{0}?format=json", countryCode));

            WorldBankCountry[][] response = serializer.ReadObject(countryResponse) as WorldBankCountry[][];

            if (response.Length != 2)
            {
                return null;
            }

            var country = response[1][0].MapToCountry();

            return country;
        }
    }
}
