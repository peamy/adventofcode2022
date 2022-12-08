using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._4
{
    public class Puzzle4
    {
        string[] input;
        public Puzzle4()
        {
            input = FileReader.GetInputForDay(4);
        }
        public void SolvePartOne()
        {
            int total = 0;
            for(int i = 0; i < input.Length; i++) 
            {
                if (DoesOneContainTheOther(input[i].Split(',')[0], input[i].Split(',')[1]))
                    total++;
            }
            Console.WriteLine(total);
        }

        public void SolvePartTwo() 
        {
            int total = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (HasOverlap(input[i].Split(',')[0], input[i].Split(',')[1]))
                    total++;
            }
            Console.WriteLine(total);
        }

        private bool DoesOneContainTheOther(string one, string two)
        {
            int onesmall = int.Parse(one.Split('-')[0]);
            int onebig = int.Parse(one.Split('-')[1]);

            int twosmall = int.Parse(two.Split('-')[0]);
            int twobig = int.Parse(two.Split('-')[1]);

            return ((onesmall >= twosmall && onebig <= twobig) || (onesmall <= twosmall && onebig >= twobig));

        }

        private bool HasOverlap(string one, string two)
        {
            int onesmall = int.Parse(one.Split('-')[0]);
            int onebig = int.Parse(one.Split('-')[1]);

            int twosmall = int.Parse(two.Split('-')[0]);
            int twobig = int.Parse(two.Split('-')[1]);

            return ((onesmall <= twobig && onebig >= twosmall) || (twosmall <= onebig && twobig >= onesmall));

        }
    }
}
