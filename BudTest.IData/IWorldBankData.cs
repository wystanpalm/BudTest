using System;
using System.Threading.Tasks;
using BudTest.IModel;

namespace BudTest.IData
{
    public interface IWorldBankData
    {
        Task<ICountry> FindCountry(string countryCode);
    }
}
