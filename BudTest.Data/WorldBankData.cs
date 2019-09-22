using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using BudTest.Data.Entity;
using BudTest.IData;

namespace BudTest.Data
{
    public class WorldBankData : IWorldBankData
    {
        public async Task<string> FindCountry(string countryCode)
        {
            var client = new HttpClient();
            var serializer = new DataContractJsonSerializer(typeof(WorldBankCountry[][]));

            var countryResponse = await client.GetStreamAsync(string.Format("http://api.worldbank.org/v2/country/{0}?format=json", countryCode));

            WorldBankCountry[][] something = serializer.ReadObject(countryResponse) as WorldBankCountry[][];

            if (something.Length != 2)
            {
                return null;
            }

            var country = something[1][0];

            return country.Name;
        }
    }
}
