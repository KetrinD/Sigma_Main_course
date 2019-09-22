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

            Coutry firstContry = new Coutry() { Name = "Germany" };
            Coutry secondContry = new Coutry() { Name = "Poland" };
            Coutry thirdContry = new Coutry() { Name = "Montenegro" };
            Coutry fourthContry = new Coutry() { Name = "Georgia" };
            Coutry fifthContry = new Coutry() { Name = "Egypt" };
            Coutry sixthContry = new Coutry() { Name = "Turkey" };
            Coutry seventhContry = new Coutry() { Name = "United Arab Emirates" };

            VisitedCoutriesCollection<Coutry> visitedCountries = new VisitedCoutriesCollection<Coutry>();

            visitedCountries.AddCountry(firstContry);
            visitedCountries.AddCountry(secondContry);
            visitedCountries.AddCountry(thirdContry);
            visitedCountries.AddCountry(fourthContry);
            visitedCountries.AddCountry(fifthContry);
            visitedCountries.AddCountry(sixthContry);
            visitedCountries.AddCountry(seventhContry);

            foreach (var country in visitedCountries)
            {
                Console.WriteLine(country);
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
