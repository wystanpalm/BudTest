using System;
using Xunit;
using BudTest.IData;
using BudTest.Data;
using BudTest.Model;

namespace BudTest.DataTestXUnit
{
    public class WorldBankDataTest
    {
        // ToDo: these tests consume the live WorldBank service - could mock the HttpClient to make them unit tests

        [Fact]
        public void FindCountry_ValidCountry_ReturnsCountry()
        {
            var target = new WorldBankData();

            var task = target.FindCountry("GB");
            task.Wait();

            Assert.IsType<Country>(task.Result);
        }

        [Fact]
        public void FindCountry_InValidCountry_ReturnsNull()
        {
            var target = new WorldBankData();

            var task = target.FindCountry("XX");
            task.Wait();

            Assert.Null(task.Result);
        }

        [Theory]
        [InlineData("GB")]
        public void FindCountry_GreatBritain_ReturnsRequiredValues(string value)
        {
            var target = new WorldBankData();

            var task = target.FindCountry(value);
            task.Wait();

            Assert.Equal("United Kingdom", task.Result.Name);
            Assert.Equal("Europe & Central Asia", task.Result.Region);
            Assert.Equal("London", task.Result.CapitalCity);
            Assert.Equal(-0.126236M, task.Result.Longitude);
            Assert.Equal(51.5002M, task.Result.Latitude);
        }

        [Fact]
        public void FindCountry_InvalidCharacters_ThrowsException()
        {
            var target = new WorldBankData();

            var task = target.FindCountry("/");
            Exception exception = Assert.Throws<AggregateException>(() => task.Wait());
            Assert.IsType<ArgumentException>(exception.InnerException);
        }
    }
}
