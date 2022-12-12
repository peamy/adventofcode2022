using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles
{
    public class Puzzle12
    {

        List<Point> AllPoints;
        Point start;

        public void SolvePartOne()
        {
            Initdata();

            Queue<Point> ToCheck = new Queue<Point>();
            ToCheck.Enqueue(start);

            int partone = CalculateValues(ToCheck);

            Console.WriteLine($"Part 1 result: {partone}");
        }

        public void SolvePartTwo()
        {
            Initdata();

            Queue<Point> ToCheck = new Queue<Point>();
            var allstarts = AllPoints.Where(p => p.Height == 1).ToList();
            foreach (var firstchecks in allstarts) {
                firstchecks.Checking = true;
                ToCheck.Enqueue(firstchecks);
            }

            var best = CalculateValues(ToCheck);

            Console.WriteLine($"Part two result: {best}");
        }

        /// <summary>
        /// return score of the shortest path to a point
        /// </summary>
        /// <param name="ToCheck">Queue of starting points. Queue will be usedto enqueue points that need to be scored next</param>
        /// <returns>Score of whoever reaches the finish first +1, or -1 if no finish found</returns>
        private int CalculateValues(Queue<Point> ToCheck)
        {
            while (ToCheck.Count > 0)
            {
                var point = ToCheck.Dequeue();
                if (!point.IsChecked)
                {
                    var neighbours = GetNeighbours(point);
                    foreach (var n in neighbours)
                    {
                        if (n.Score == 0 && !n.Checking) {
                            n.Score = point.Score + 1;
                            ToCheck.Enqueue(n);
                        }
                        if (n.IsFinish)
                        {
                            Console.WriteLine($"Finish is reached in {n.Score} steps!");
                            return n.Score;
                        }
                    }
                    point.IsChecked = true;
                }
            }
            return -1;
        }

        //This should be a property of a Point and calculated only once
        //However performance is not a big issue for this puzzle
        private List<Point> GetNeighbours(Point point)
        {
            // thought it should be one, but getting the right output when it's 2
            int maxHeight = 2; 
            List<Point> list = new List<Point>();

            //dirty hardcoded (:
            var left = AllPoints.Where(p => p.X == point.X - 1 && p.Y == point.Y && p.IsChecked == false && p.Height >= point.Height - maxHeight && p.Height <= point.Height + 1).FirstOrDefault();
            var right = AllPoints.Where(p => p.X == point.X + 1 && p.Y == point.Y && p.IsChecked == false && p.Height >= point.Height - maxHeight && p.Height <= point.Height + 1).FirstOrDefault();
            var up = AllPoints.Where(p => p.X == point.X  && p.Y == point.Y - 1 && p.IsChecked == false && p.Height >= point.Height - maxHeight && p.Height <= point.Height + 1).FirstOrDefault();
            var down = AllPoints.Where(p => p.X == point.X  && p.Y == point.Y + 1 && p.IsChecked == false && p.Height >= point.Height - maxHeight && p.Height <= point.Height + 1).FirstOrDefault();
            if (left != null)
                list.Add(left);
            if (right != null)
                list.Add(right);
            if (up != null)
                list.Add(up);
            if (down != null)
                list.Add(down);
            return list;
        }

        private void Initdata(bool dummy = false)
        {
            var input = DataReader.ReadForDay(12, dummy);
            int amountOfPoints = input[0].Count() * input[0].Length;
            AllPoints = new List<Point>(amountOfPoints);
                        
            for (int i = 0; i < input.Count(); i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    char c = input[i][j];
                    var number = c - 96;
                    Point newPoint = new Point(j, i,number);
                    if (c == 'S')
                    {
                        start = newPoint;
                        start.Height = 0;
                        newPoint.IsStart = true;
                        start.Score = 0;
                    }
                    if (c == 'E')
                    {                        
                        newPoint.Height = 26;
                        newPoint.IsFinish = true;
                    }

                    AllPoints.Add(newPoint);
                }
            }
        }
    }

    internal class Point
    {
        public Point(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }
        public int Height;
        public int Score;
        public bool IsStart;
        public bool IsFinish;
        public bool IsChecked;
        public bool Checking;
        public int X { get; set; }
        public int Y { get; set; }
    }
}
