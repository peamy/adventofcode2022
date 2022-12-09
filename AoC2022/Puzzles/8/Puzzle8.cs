using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._8
{
    public class Puzzle9
    {
        int[,] forest;
        bool[,] results;
        public void SolvePartOne()
        {
            return;

            InitializeData();
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                checkRowHorizontal(i);
                checkRowHorizontal(i, true);
            }

            for (int i = 0; i < forest.GetLength(1); i++)
            {
                checkRowVertical(i);
                checkRowVertical(i, true);
            }

            int total = 0;
            foreach (var res in results)
            {
                if (res)
                    total++;
            }
            Console.WriteLine(total);
        }

        public void SolvePartTwo() 
        {
            InitializeData();
            int max = 0;
            for (int i = 1; i < forest.GetLength(0) -1; i++)
            {
                for (int j = 1; j < forest.GetLength(1)-1; j++)
                {
                    var score = FindViewScore(j, i);
                    max = score > max? score : max;
                }
            }
            Console.WriteLine(max);
        }

        private int FindViewScore(int x, int y)
        {
            if(x == 2 && y == 3)
            {

            }
            int up = checkUp(x, y);
            int down = checkDown(x, y);
            int left = checkLeft(x, y);
            int right = checkRight(x, y);
            return up * down * left * right;
        }

        private int checkUp(int x, int y)
        {
            int total = 0;
            int myHeight = forest[y, x];
            for(int i = y-1; i >= 0; i--)
            {
                total++;
                if (forest[i, x] >= myHeight)
                    break;
            }
            return total;
        }

        private int checkDown(int x, int y)
        {
            int total = 0;
            int myHeight = forest[y, x];
            for (int i = y + 1; i < forest.GetLength(0); i++)
            {
                total++;
                if (forest[i, x] >= myHeight)
                    break;
            }
            return total;
        }

        private int checkLeft(int x, int y)
        {
            int total = 0;
            int myHeight = forest[y, x];
            for (int i = x - 1; i >= 0; i--)
            {
                total++;
                if (forest[y, i] >= myHeight)
                    break;
            }
            return total;
        }

        private int checkRight(int x, int y)
        {
            int total = 0;
            int myHeight = forest[y, x];
            for (int i = x + 1; i < forest.GetLength(1); i++)
            {
                total++;
                if (forest[y, i] >= myHeight)
                    break;
            }
            return total;
        }

        private void checkRowHorizontal(int y, bool inverted = false)
        {
            int highest = -1;
            for(int i = 0; i < forest.GetLength(1); i++){
                int place = inverted ? forest.GetLength(1) - (i+1) : i;
                int current = forest[y, place];
                if (current > highest) {
                    highest = current;
                    results[y, place] = true;
                }
            }
        }

        private void checkRowVertical(int x, bool inverted = false)
        {
            int highest = -1;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                int place = inverted ? forest.GetLength(1) - (i+1) : i;
                int current = forest[place, x];
                if (current > highest)
                {
                    highest = current;
                    results[place, x] = true;
                }
            }
        }

        private void InitializeData()
        {
            var input = DataReader.ReadForDay(8, false);
            int x = input.Length;
            int y = input[0].Length;
            forest = new int[x,y];
            results = new bool[x, y];

            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j< y; j++)
                {
                    forest[i, j] = int.Parse(input[i].Substring(j,1));
                }
            }
        }
    }
}
