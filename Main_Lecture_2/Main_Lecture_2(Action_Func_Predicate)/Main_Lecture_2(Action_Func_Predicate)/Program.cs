using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_2_Action_Func_Predicate_
{
    class Program
    {
        static void Main(string[] args)
        {

            StringCollector collectionWithChars = new StringCollector();
            AlphaNumbericCollector collectionWithNambers = new AlphaNumbericCollector();

            Console.WriteLine("Do you want to add some string? y/n");
            string answer = Console.ReadLine();
            answer = answer.ToUpper();

            switch (answer)
            {

                case "Y":
                    {
                        Yeap();
                        break;
                    }
                case "N":
                    {
                        Noup();
                        break;
                    }
                default:
                    {
                        Default();
                        break;
                    }
            }

            void Yeap()
            {
                while (answer == "Y")
                {
                    Console.WriteLine("Enter a string:\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string mes = Console.ReadLine();
                    Console.ResetColor();

                    Action<string> messageString = collectionWithChars.Actions;
                    Action<string> messageNembers = collectionWithNambers.Actions;

                    if (!mes.Any(c => char.IsDigit(c)))
                    {
                        messageString(mes);
                    }
                    else
                    {
                        messageNembers(mes);
                    }
                    Console.WriteLine("\nDo you want to add some string? y/n");

                    answer = Console.ReadLine();
                    answer = answer.ToUpper();

                    if (answer == "N")
                    {
                        Noup();
                    }

                    else if (!(answer == "N" || answer == "Y"))
                        Default();
                }
            }

            void Noup()
            {
                Console.WriteLine("Have a nice evening. Bye");
            }

            void Default()
            {
                while (!(answer == "N" || answer == "Y"))
                {

                    Console.WriteLine("You type wrong answer. You should select y/n");
                    answer = Console.ReadLine();
                    answer = answer.ToUpper();
                    Yeap();
                }
            }

            Console.ReadKey();
        }
    }
}
