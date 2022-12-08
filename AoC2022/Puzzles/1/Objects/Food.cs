using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._1.Objects
{
    public class Food
    {
        public Food(int calories)
        {
            this.calories = calories;
        }
        public int calories { get; private set; }
    }
}
