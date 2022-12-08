using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._1.Objects
{
    public class Elf
    {
        public Elf() 
        {
            FoodItems = new List<Food>();
        }
        public List<Food> FoodItems { get; set; }
        public int CalculateCalories()
        {
            return FoodItems.Sum(f => f.calories);
        }
    }
}
