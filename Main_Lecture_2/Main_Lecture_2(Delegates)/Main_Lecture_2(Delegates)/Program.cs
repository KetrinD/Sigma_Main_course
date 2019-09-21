using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_2_Delegates_
{
    public delegate void Message(string message);
    class Program
    {
        static void Main(string[] args)
        {
          
            StringCollector collectionWithChars = new StringCollector();
            AlphaNumbericCollector collectionWithNambers = new AlphaNumbericCollector();

            Console.WriteLine("Do you want to add some string? y/n");
            string answer = Console.ReadLine().ToLower();
            answer = answer.ToUpper();

            Repeat:
            switch (answer)
            {
               
                case "Y":
                    {
                        while (answer == "Y")
                        {
                            Console.WriteLine("Enter a string:\n");
                            string mes = Console.ReadLine();

                            Message messageString = collectionWithChars.Action;
                            Message messageNembers = collectionWithNambers.Action;

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
                                goto case "N";
                            }

                            else if (!(answer == "N" || answer == "Y"))
                                goto default;
                        }
                        break;
                    }
                case "N":
                    {
                        Console.WriteLine("Have a nice evening. Bye");
                        break;
                    }
                default:
                    {
                        while (! (answer == "N" ||answer == "Y"))
                        {

                            Console.WriteLine("You type wrong answer. You should select y/n");
                            answer = Console.ReadLine();
                            answer = answer.ToUpper(); { goto Repeat; }
                        }
                        break;
                    }
            }
            Console.ReadKey();
        }
    }
 }


