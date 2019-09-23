﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_2_Events_
{
    class Program
    {
        static StringCollector collectionWithChars = new StringCollector();
        static AlphaNumbericCollector collectionWithNambers = new AlphaNumbericCollector(); 

        public delegate void StringHandler(string source, string str);

        static void Main(string[] args)
        {

            collectionWithChars.OnStringAction += messageHendler;
            collectionWithNambers.OnStringAction += messageHendler;

            Console.WriteLine("Do you want to add some string? y/n");
            string answer = Console.ReadLine();
            answer = answer.ToUpper();

            switch (answer)
            {

                case "Y":
                    {
                        Yeap(answer);
                        break;
                    }
                case "N":
                    {
                        Noup();
                        break;
                    }
                default:
                    {
                        Default(answer);
                        break;
                    }
            }
            Console.ReadKey();
        }

        public static void Yeap(string _mes)
        {
            while (_mes == "Y")
            {
                Console.WriteLine("Enter a string:\n");
                Console.ForegroundColor = ConsoleColor.Green;
                string mes = Console.ReadLine();
                Console.ResetColor();

                if (!mes.Any(c => char.IsDigit(c)))
                {
                    collectionWithChars.Action(mes);
                }
                else
                {
                    collectionWithNambers.Action(mes);
                }
                Console.WriteLine("\nDo you want to add some string? y/n");

                _mes = Console.ReadLine();
                _mes = _mes.ToUpper();

                if (_mes == "N")
                {
                    Noup();
                }
                else
                    Default(_mes);
            }
        }

        public static void Noup()
        {
            Console.WriteLine("Have a nice evening. Bye");
        }

        public static void Default(string _mes)
        {
            while (!(_mes == "N" || _mes == "Y"))
            {
                Console.WriteLine("You type wrong answer. You should select y/n");
                _mes = Console.ReadLine();
                _mes = _mes.ToUpper();
                Yeap(_mes);
            }
        }

        public static void messageHendler(string source, string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nString \"{str}\" was handled by {source}");
            Console.ResetColor();
        }      
    }
}


