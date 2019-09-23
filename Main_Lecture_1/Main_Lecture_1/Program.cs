using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Main_Lecture_1
{
    class Program
    {
        static void Main(string[] args)
        {

            Country firstContry = new Country() { Name = "Germany" };
            Country secondContry = new Country() { Name = "Poland" };
            Country thirdContry = new Country() { Name = "Montenegro" };
            Country fourthContry = new Country() { Name = "Georgia" };
            Country fifthContry = new Country() { Name = "Egypt" };
            Country sixthContry = new Country() { Name = "Turkey" };
            Country seventhContry = new Country() { Name = "United Arab Emirates" };

            VisitedCoutriesCollection<Country> visitedCountries = new VisitedCoutriesCollection<Country>();

            visitedCountries.AddCountry(firstContry);
            visitedCountries.AddCountry(secondContry);
            visitedCountries.AddCountry(thirdContry);
            visitedCountries.AddCountry(fourthContry);
            visitedCountries.AddCountry(fifthContry);
            visitedCountries.AddCountry(sixthContry);
            visitedCountries.AddCountry(seventhContry);

            foreach (var country in visitedCountries)
            {
                Console.WriteLine(country.ToString());
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(visitedCountries.Count());
            Console.ResetColor();
            Console.WriteLine("\n");

            visitedCountries.RemoveCountry(firstContry);

            foreach (var country in visitedCountries)
            {
                Console.WriteLine(country);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(visitedCountries.Count());
            Console.ResetColor();
            Console.WriteLine("\n");

            visitedCountries.RemoveCountryAtPosition(3);

            foreach (var country in visitedCountries)
            {
                Console.WriteLine(country);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(visitedCountries.Count());
            Console.ResetColor();
            Console.WriteLine("\n");

            Console.ReadKey();
        }
    }
}
