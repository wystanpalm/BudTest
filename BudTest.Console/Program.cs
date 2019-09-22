using System;
using BudTest.IData;
using BudTest.Data;
using BudTest.IModel;
using System.Text.RegularExpressions;

namespace BudTest.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{

            IWorldBankData worldBankData = new WorldBankData();

            do
            {
                Console.WriteLine("Enter the country code you want to search for, type \"Exit\" to quit:");
			    string code = Console.ReadLine();

                if (code == "Exit")
                {
                    break;
                }

                Regex regex = new Regex("^[a-zA-z]{2,3}$");
                if (!regex.IsMatch(code))
                {
                    Console.WriteLine("Invalide country code");
                    continue;
                }

                var countryTask = worldBankData.FindCountry(code);
                countryTask.Wait();
                ICountry country = countryTask.Result;

                if (country == null)
                {
                    Console.WriteLine("Country not found");
                }
                else
                {
                    Console.WriteLine("Name: {0}", country.Name);
                    Console.WriteLine("Region: {0}", country.Region);
                    Console.WriteLine("Capital City: {0}", country.CapitalCity);
                    Console.WriteLine("Longitude: {0}", country.Longitude);
                    Console.WriteLine("Latitude: {0}", country.Latitude);
                }
            } while (true);
        }
    }
}
