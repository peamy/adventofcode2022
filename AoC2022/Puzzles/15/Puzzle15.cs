using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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


        //4211420 too low
        //4361395 too low :(
        public void SolvePartOne()
        {
            var sensors = InitSensors();
            int lineNr = TESTING ? 10 : 2000000;
            countImpossibleSpots(sensors, lineNr);

            //Console.WriteLine( Day15.ProcessPart1(DataReader.ReadForDay(15), lineNr));
        }

        public void SolvePartTwo() 
        {

        }

        private void countImpossibleSpots(List<Sensor> sensors, int y)
        {
            //List<HorizontalRange> availableSpots= new List<HorizontalRange>();
            //availableSpots.Add(new HorizontalRange(minRangeX, maxRangeX));

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
            Console.WriteLine(emptyXPositions.Count);   
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

        public HorizontalRange GetRangeForY(int line)
        {
            int heightDifference = Math.Abs(line - Y);

            int widthOfLine = Reach - heightDifference;
            if(widthOfLine < 0)
                return new HorizontalRange(0, 0);

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
    }
}
