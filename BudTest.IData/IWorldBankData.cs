using System;
using System.Threading.Tasks;

namespace BudTest.IData
{
    public interface IWorldBankData
    {
        Task<string> FindCountry(string countryCode);
    }
}
