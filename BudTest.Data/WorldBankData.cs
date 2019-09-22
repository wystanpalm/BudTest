using System;
using System.Runtime.Serialization.Json;
using BudTest.IData;

namespace BudTest.Data
{
    public class WorldBankData : IWorldBankData
    {
        public object FindCountry(string countryCode)
        {
            if (countryCode == "GB")
            {
                return new object();
            }
            else
            {
                return null;
            }
        }
    }
}
