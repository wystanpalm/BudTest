using System;
using BudTest.IData;
using BudTest.Data;
using BudTest.IModel;
using System.Text.RegularExpressions;

namespace BudTest.ConsoleApp
{
	class Program
	{
        IWorldBankData worldBankData;

        Program()
        {
            // Could inject this dependency in future
            this.worldBankData = new WorldBankData();
        }

        static void Main(string[] args)
		{
            Program program = new Program();

            program.LaunchUI();
        }

        private void LaunchUI()
        {
            do
            {
                Console.WriteLine("Enter the country code you want to search for, type \"exit\" to quit:");
                string code = Console.ReadLine();

                if (code.ToLower() == "exit")
                {
                    break;
                }

                if (!this.ValidateUserInput(code))
                {
                    Console.WriteLine("Invalid country code");
                    continue;
                }

                var countryTask = this.worldBankData.FindCountry(code);
                countryTask.Wait();
                ICountry country = countryTask.Result;

                if (country == null)
                {
                    Console.WriteLine("Country not found");
                }
                else
                {
                    this.OutputCountryDetails(country);
                }
            } while (true);
        }

        private void OutputCountryDetails(ICountry country)
        {
            Console.WriteLine("Name: {0}", country.Name);
            Console.WriteLine("Region: {0}", country.Region);
            Console.WriteLine("Capital City: {0}", country.CapitalCity);
            Console.WriteLine("Longitude: {0}", country.Longitude);
            Console.WriteLine("Latitude: {0}", country.Latitude);
        }

        private bool ValidateUserInput(string code)
        {
            Regex regex = new Regex("^[a-zA-z]{2,3}$");
            return (regex.IsMatch(code));
        }
    }
}
