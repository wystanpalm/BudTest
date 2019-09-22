using System;

namespace BudTest.IData
{
    public interface IWorldBankData
    {
        object FindCountry(string countryCode);
    }
}
