using System;
using Xunit;
using BudTest.IData;
using BudTest.Data;
using BudTest.Model;

namespace BudTest.DataTestXUnit
{
    public class WorldBankDataTest
    {
        [Fact]
        public void FindCountry_ValidCountry_ReturnsCountry()
        {
            var target = new WorldBankData();

            var result = target.FindCountry("GB");
            result.Wait();

            Assert.IsType<Country>(result.Result);
        }

        [Fact]
        public void FindCountry_InValidCountry_ReturnsNull()
        {
            var target = new WorldBankData();

            var result = target.FindCountry("XX");
            result.Wait();

            Assert.Null(result.Result);
        }
    }
}
