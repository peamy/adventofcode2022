using AoC2022.Common;
using AoC2022.Puzzles._1.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._1
{
    public class Puzzle1
    {

        public string solvePartOne()
        {
            string result = "";

            result = CreateElves1().ToString();

            return result;
        }

        public string solvePartTwo()
        {
            string result = "";

            result = CreateElves2().ToString();

            return result;
        }


        private int CreateElves1()
        {
            var elves = new List<Elf>();

            int mostcalories = 0;

            var rawdata = DataReader.ReadFile("C:\\Users\\rvanes\\Documents\\Repos\\Other Programming\\AoC\\AoC2022\\AoC2022\\Puzzles\\1\\Input\\Input.txt");
            Elf currentElf = new Elf();
            foreach(var fooditem in rawdata) 
            {
                if(fooditem == "")
                {
                    elves.Add(currentElf);
                    if(currentElf.CalculateCalories() > mostcalories)
                    {
                        mostcalories = currentElf.CalculateCalories();
                    }
                    currentElf = new Elf();
                }
                else
                {
                    currentElf.FoodItems.Add(new Food(int.Parse(fooditem)));
                }
            }

            return mostcalories;
        }

        private int CreateElves2()
        {
            var elves = new List<Elf>();
            var rawdata = DataReader.ReadFile("C:\\Users\\rvanes\\Documents\\Repos\\Other Programming\\AoC\\AoC2022\\AoC2022\\Puzzles\\1\\Input\\Input.txt");
            Elf currentElf = new Elf();
            foreach (var fooditem in rawdata)
            {
                if (fooditem == "")
                {
                    elves.Add(currentElf);
                    currentElf = new Elf();
                }
                else
                {
                    currentElf.FoodItems.Add(new Food(int.Parse(fooditem)));
                }
            }

            var ordered = elves.OrderByDescending(e => e.CalculateCalories()).ToList();
            

            return ordered[0].CalculateCalories() + ordered[1].CalculateCalories() + ordered[2].CalculateCalories();
        }

    }
}
