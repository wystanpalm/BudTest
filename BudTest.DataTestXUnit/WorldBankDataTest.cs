using System;
using Xunit;
using BudTest.IData;
using BudTest.Data;

namespace BudTest.DataTestXUnit
{
    public class WorldBankDataTest
    {
        [Fact]
        public void FindCountry_ValidCountry_ReturnsObject()
        {
            var target = new WorldBankData();

            var result = target.FindCountry("GB");

            Assert.IsType<object>(result);
        }

        [Fact]
        public void FindCountry_InValidCountry_ReturnsNull()
        {
            var target = new WorldBankData();

            var result = target.FindCountry("XX");

            Assert.Null(result);
        }
    }
}
