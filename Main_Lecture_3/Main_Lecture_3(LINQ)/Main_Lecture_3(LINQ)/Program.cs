using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_3_LINQ_
{
    class Program
    {
        static void Main(string[] args)
        {
            //#1-v.1
            string players = " Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert";
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            string[] splitedPlayers = players.Split(',');
            var numberedPlayers = numbers.Zip(
                splitedPlayers,
                (first, second) => first + " " + second
                ).ToList();

            numberedPlayers.ForEach(name => Console.WriteLine($"{name}"));

            //#1-v.2
            string result = players.Split(',').Zip((Enumerable.Range(1, 11)), (first, second) => second + "." + first).Aggregate((current, next) => current + ", "+ next);
            Console.WriteLine(result);

            //#2
            string playersAge = "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988";      
            var player = playersAge.Split(';').Select(s => new
            {
                Name = s.Split(',')[0],
                BD = DateTime.ParseExact(s.Split(',')[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            }).OrderBy(n => n.BD).ToList();
            player.ForEach(s => Console.WriteLine($"{s.BD} Age: {Convert.ToInt32((DateTime.Now - s.BD).TotalDays/365)}"));

            //#3
            string songsLength = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
            var sumSongsLength = songsLength.Split(',').Sum(p => TimeSpan.Parse(p).TotalHours);
            Console.WriteLine($"\n{sumSongsLength}");

            Console.ReadKey();
        }
    }
}

