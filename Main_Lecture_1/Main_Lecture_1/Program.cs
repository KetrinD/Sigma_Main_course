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

            VisitedCoutriesCollection<Coutry> visitedCountries = new VisitedCoutriesCollection<Coutry>();

            visitedCountries.AddCountry(firstContry);
            visitedCountries.AddCountry(secondContry);

            foreach (var country in visitedCountries)
            {
                Console.WriteLine(country);
            }
          
            Console.ReadKey();
        }
    }
}
