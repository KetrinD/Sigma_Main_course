using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_2_Action_Func_Predicate_
{
    class StringCollector
    {
        List<string> collectionWithChars = new List<string>();

        public void Actions(String message)
        {
            Console.WriteLine($"\nYou have already {collectionWithChars.Count} elements on your collection with strings");

            collectionWithChars.Add(message);

            Console.WriteLine($"\nMessage \"{message}\" was added to colection with strings");

            Console.WriteLine($"\nAnd now you have {collectionWithChars.Count} elements on your collection with strings:");

            foreach (var s in collectionWithChars)
            {
                Console.WriteLine(s);
            }
        }
    }
}
