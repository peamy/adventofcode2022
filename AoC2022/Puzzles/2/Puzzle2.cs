using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC2022.Common;

namespace adventofcode2022.Puzzles._2
{
    public class Puzzle2
    {
        public void SolvePartOne()
        {
            var input = DataReader.ReadForDay(2);
            int totalscore = 0;
            foreach(var part in input)
            {
                totalscore += calculateYourScore(part);
            }
            Console.WriteLine(totalscore);

        }

        public void SolvePartTwo()
        {
            var input = DataReader.ReadForDay(2);
            int totalscore = 0;
            foreach (var part in input)
            {
                totalscore += calculateYourScore2(part);
            }
            Console.WriteLine(totalscore);

        }

        private int calculateYourScore(string match)
        {
            int score = 0;
            string[] split = match.Split(' ');
            var opponent = split[0];
            var you = split[1];

            switch (you)
            {
                case "X":
                    score += 1;
                    break;
                case "Y":
                    score += 2;
                    break;
                case "Z":
                    score += 3;
                    break;
            }
            if (opponent == "A" && you == "X" || opponent == "B" && you == "Y" || opponent == "C" && you == "Z")
                score += 3;
            else if (opponent == "A" && you == "Y" || opponent == "B" && you == "Z" || opponent == "C" && you == "X")
                score += 6;

             return score;
        }

        private int calculateYourScore2(string match)
        {
            int score = 0;
            string[] split = match.Split(' ');
            var opponent = split[0];
            var you = split[1];

            switch (you)
            {
                case "Z":
                    score += 6;
                    if (opponent == "A")
                        score += 2;
                    if (opponent == "B")
                        score += 3;
                    if (opponent == "C")
                        score += 1;
                    break;
                case "Y":
                    score += 3;
                    if (opponent == "A")
                        score += 1;
                    if (opponent == "B")
                        score += 2;
                    if (opponent == "C")
                        score += 3;
                    break;
                case "X":
                    if (opponent == "A")
                        score += 3;
                    if (opponent == "B")
                        score += 1;
                    if (opponent == "C")
                        score += 2;
                    break;
            }

            return score;
        }
    }
}
