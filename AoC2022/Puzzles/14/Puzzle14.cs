using AoC2022.Common;
using AoC2022.Puzzles._14.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles
{
    public  class Puzzle14
    {
        public void SolvePartOne()
        {
            return;
            var map = InitializeMap();
            map.PrintMap();
            int total = 0;
            while (!map.AddSand())
            {
                total++;
                //Console.WriteLine("Simulated one grain of sand. Press any button to continue.");
                //Console.ReadKey();
            }
            map.PrintMap();
            Console.WriteLine($"Reached the end with {total} grains of sand");
        }

        public void SolvePartTwo() 
        {
            var map = InitializeMapPt2();
            map.PrintMap();
            int total = 0;
            while (!map.AddSand())
            {
                total++;
                //Console.WriteLine("Simulated one grain of sand. Press any button to continue.");
                //Console.ReadKey();
            }
            map.PrintMap();
            //29043 too low!
            Console.WriteLine($"Reached the end with {total} grains of sand");
        }

        private Map InitializeMap()
        {


            int maxx = 0; int maxy = 0;
            foreach (var item in input)
            {
                foreach(var ite in item.Split("-> "))
                {
                    int x = int.Parse(ite.Split(",")[0]);
                    int y = int.Parse(ite.Split(",")[1]);
                    maxx = Math.Max(maxx, x);
                    maxy= Math.Max(maxy, y);
                }
            }

            var map = new Map(maxx+1, maxy+1);
            foreach(var item in input)
            {
                map.AddRocks(item);
            }
            return map;
        }

        private Map InitializeMapPt2()
        {
            var input = DataReader.ReadForDay(14, false);


            int maxx = 0; int maxy = 0;
            foreach (var item in input)
            {
                foreach (var ite in item.Split("-> "))
                {
                    int x = int.Parse(ite.Split(",")[0]);
                    int y = int.Parse(ite.Split(",")[1]);
                    maxx = Math.Max(maxx, x);
                    maxy = Math.Max(maxy, y);
                }
            }

            maxy += 2;
            maxx += 1000;

            var map = new Map(maxx+1, maxy + 1, true);
            foreach (var item in input)
            {
                map.AddRocks(item);
            }
            map.AddRocks($"{0},{maxy} -> {maxx},{maxy}", false);
            return map;
        }
    }
}
