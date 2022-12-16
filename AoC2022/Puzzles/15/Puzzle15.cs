using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles
{
    public class Puzzle15
    {
        public bool TESTING = false;

        int minRangeX;
        int maxRangeX;
        int minRangeY;
        int maxRangeY;

        int pt2minx, pt2miny = 0;
        int pt2maxx = 4000000;
        int pt2maxy = 4000000;

        BigInteger xmulti = 4000000;

        //4211420 too low
        //4361395 too low :(
        public void SolvePartOne()
        {
            var sensors = InitSensors();
            int lineNr = TESTING ? 10 : 2000000;
            countImpossibleSpots(sensors, lineNr);
        }

        public void SolvePartTwo() 
        {
            var sensors = InitSensors();


            for(int y = pt2miny; y < pt2maxy; y++)
            {
                if (!IsLineFull(y, sensors))
                {
                    break;
                }
            }
        }

        private bool IsLineFull(int y, List<Sensor> origSensors)
        {
            var horizontalRanges = new List<HorizontalRange>();
            foreach(Sensor sensor in origSensors)
            {
                var range = sensor.GetRangeForY(y);
                if(range.X1 < pt2maxx && range.X2 > 0)
                {
                    horizontalRanges.Add(range);
                }
            }

            var completerange = horizontalRanges[0];
            horizontalRanges.Remove(completerange);

            while (horizontalRanges.Count > 0)
            {
                var curentRange = horizontalRanges.Where(r => completerange.Overlaps(r)).FirstOrDefault();
                if (curentRange == null)
                {
                    int x = completerange.X1 > 0 ? completerange.X1 - 1 : completerange.X2 + 1;

                    BigInteger bx = xmulti * x;
                    var result = bx + y;

                    Console.WriteLine($"Found the solution! x: {x}, y: {y}, result: {result}");
                    return false;
                }
                horizontalRanges.Remove(curentRange);
                completerange.Expand(curentRange);
            }

            return true;
        }

        private HashSet<int> countImpossibleSpots(List<Sensor> sensors, int y)
        {
            var emptyXPositions = new HashSet<int>();
            foreach (Sensor sensor in sensors)
            {
                for(int i = sensor.X - sensor.Reach; i < sensor.X + sensor.Reach; i++)
                {
                    if (sensor.IsInRange(i,y) && (sensor.ClosestBeaconX != i || sensor.ClosestBeaconY != y))
                    {
                        emptyXPositions.Add(i);
                    }
                }
            }
            return emptyXPositions;
        }


        //ONLY IN TEST DATA, REAL DATA WILL MESS U UP
        private void printVisuals(List<Sensor> sensors)
        {
            string result = "";
            for(int i = minRangeY; i < maxRangeY; i++)
            {
                for(int j = minRangeX; j < maxRangeX; j++)
                {
                    if (sensors.Any(s => s.X == j && s.Y == i))
                    {
                        result += "S";
                    }
                    else if (sensors.Any(s => s.ClosestBeaconX == j && s.ClosestBeaconY == i))
                    {
                        result += "B";
                    }
                    else if (sensors.Any(s => s.IsInRange(j, i)))
                    {
                        result += "#";
                    }
                    else
                    {
                        result += ".";
                    }
                }
                result += "\n";
            }
            Console.WriteLine(result);
            Console.WriteLine();
        }

        private List<Sensor> InitSensors()
        {
            List<Sensor> sensors = new List<Sensor>();
            var input = DataReader.ReadForDay(15, TESTING);
            foreach(string line in input)
            {
                //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
                var split1 = line.Split(' ');
                int x = int.Parse(split1[2].Split('=')[1].Split(',')[0]);
                int y = int.Parse(split1[3].Split('=')[1].Split(':')[0]);
                int xb = int.Parse(split1[8].Split('=')[1].Split(',')[0]);
                int yb = int.Parse(split1[9].Split('=')[1]);

                AdjustRangeX(x); AdjustRangeX(xb);
                AdjustRangeY(y); AdjustRangeY(yb);

                sensors.Add(new Sensor(x, y, xb, yb));
            }

            return sensors;
        }

        private void AdjustRangeX(int x)
        {
            if(minRangeX > x) minRangeX = x;
            if(maxRangeX < x) maxRangeX = x;
        }
        private void AdjustRangeY(int y)
        {
            if(minRangeY > y) minRangeY = y;
            if(maxRangeY < y) maxRangeY = y;
        }

    }

    internal class Sensor
    {
        public int X, Y;
        public int ClosestBeaconX, ClosestBeaconY;
        public int Reach;

        public Sensor(int x, int y, int bx, int by) 
        {
            X = x; Y = y; ClosestBeaconX= bx; ClosestBeaconY = by;
            Reach = Math.Abs(X - ClosestBeaconX) + Math.Abs(Y - ClosestBeaconY);
        }
        
        public bool IsInRange(int x, int y)
        {
            int distanceToPoint = Math.Abs(X - x) + Math.Abs(Y - y);
            return distanceToPoint <= Reach;
        }

        public bool IsInRange(HorizontalRange range, int y)
        {
            return true;
        }

        public HorizontalRange GetRangeForY(int line)
        {
            int heightDifference = Math.Abs(line - Y);

            int widthOfLine = Reach - heightDifference;
            if(widthOfLine < 0)
                return new HorizontalRange(-1, -1);

            return new HorizontalRange(X - widthOfLine, X + widthOfLine);
        }
    }

    internal class HorizontalRange
    {
        public int X1, X2;
        public HorizontalRange(int x1, int x2)
        {
            X1 = x1;
            X2 = x2;
        }

        public bool Overlaps(HorizontalRange range)
        {
            bool one = X1 >= range.X1 && X1 <= range.X2;
            bool two = X2 <= range.X2 && X2 >= range.X1;

            bool th = range.X1 >= X1 && range.X1 <= X2;
            bool f = range.X2 >= X1 && range.X2 <= X2;

            return one || two || th || f;
        }

        internal void Expand(HorizontalRange curentRange)
        {
            X1 = curentRange.X1 < X1 ? curentRange.X1 : X1;
            X2 = curentRange.X2 > X2 ? curentRange.X2 : X2;
        }
    }
}
