﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_2_Events_
{
    class AlphaNumbericCollector
    {
        List<string> collectionWithNumbers = new List<string>();

        public event Program.StringHandler OnStringAction;
        public void Action(String message)
        {
            Console.WriteLine($"\nYou have already {collectionWithNumbers.Count} elements on your collection with numbers");

            collectionWithNumbers.Add(message);

            if (OnStringAction != null)
            {
                Console.WriteLine($"\nMessage \"{message}\" was added to colection with numbers");
                Console.WriteLine($"\nAnd now you have {collectionWithNumbers.Count} elements on your collection with numbers:");
                foreach (var s in collectionWithNumbers)
                {
                    Console.WriteLine(s);
                }

                OnStringAction("AlphaNumbericCollector", message);
            }
        }
    }
}
