using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2022.Puzzles._14.Objects
{
    public class Map
    {
        public ObjectType[,] FullMap;

        private int currentSandX;
        private int currentSandY;

        public Map(int maxX, int maxY, bool finishtop = false) 
        {
            FullMap = new ObjectType[maxY, maxX];
            leftMostRock = maxX;
            rightMostRock = 0;
            finishOnReachTop = finishtop;
        }

        private int leftMostRock;
        private int rightMostRock;
        private bool finishOnReachTop = false;
        public void AddRocks(string walls, bool addtomaxwall = true)
        {
            //498,4-> 498,6-> 496,6
            int prevx = -1;
            int prevy = -1;
            foreach(var point in walls.Split("-> "))
            {
                int x = int.Parse(point.Split(',')[0]);
                int y = int.Parse(point.Split(',')[1]);

                AddObjectToMap(x, y, addtomaxwall);

                if (-1 != prevx && -1 != prevy)
                {
                    while(x != prevx) 
                    {
                        prevx = x > prevx ? prevx+1 : prevx-1;
                        AddObjectToMap(prevx, prevy, addtomaxwall);
                    }
                    while (y != prevy)
                    {
                        prevy = y > prevy ? prevy + 1 : prevy - 1;
                        AddObjectToMap(prevx, prevy, addtomaxwall);
                    }
                }

                prevx = x; prevy = y;
            }

        }

        public bool AddSand()
        {
            currentSandX = 500;
            currentSandY = 0;

            FullMap[currentSandY, currentSandX] = ObjectType.SAND;
            while (currentSandFall())
            {
            }

            //PrintMap();
            if (finishOnReachTop)
                return ReachedTop();
            return ReachedBottem();
        }

        private bool currentSandFall()
        {
            if (FullMap[currentSandY+1, currentSandX] == ObjectType.AIR)
            {
                return !MoveSand(currentSandY + 1, currentSandX);
            }
            else if(xOutOfBounds(currentSandX - 1)){
                return false;
            }
            else if (FullMap[currentSandY + 1, currentSandX - 1] == ObjectType.AIR)
            {
                return !MoveSand(currentSandY + 1, currentSandX - 1);
            }
            else if (xOutOfBounds(currentSandX + 1))
            {
                return false;
            }
            else if (FullMap[currentSandY + 1, currentSandX +1] == ObjectType.AIR)
            {
                return !MoveSand(currentSandY + 1, currentSandX + 1);
            }
            

            return false;
        }

        private bool xOutOfBounds(int x)
        {
            return x >= FullMap.GetLength(1) || x < 0;
        }

        private bool MoveSand(int targetY, int targetX)
        {
            AddObjectToMap(targetX, targetY, true, ObjectType.SAND);
            AddObjectToMap(currentSandX, currentSandY, true, ObjectType.AIR);
            currentSandX = targetX;
            currentSandY = targetY;
            return ReachedBottem();
        }

        private bool ReachedBottem()
        {
            return currentSandY == FullMap.GetLength(0) -1;
        }

        private bool ReachedTop()
        {
            return currentSandY == 0 && currentSandX == 500;
        }

        private void AddObjectToMap(int x, int y, bool addtomaxwall, ObjectType obj = ObjectType.ROCK)
        {
            //if(x > FullMap.GetLength(1) || y > FullMap.GetLength(0))
            //{
            //    var tmpmap= ResizeArray(FullMap, x + 1, y + 1);
            //}
            if (addtomaxwall)
            {
                if (x+1 < leftMostRock)
                    leftMostRock = x+1;
                if(x+1 > rightMostRock) 
                    rightMostRock = x+1;

            }
            FullMap[y, x] = obj;
        }

        //private ObjectType[,] ResizeArray(ObjectType[,] original, int x, int y)
        //{
        //    ObjectType[,] newArray = new ObjectType[x, y];
        //    int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
        //    int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

        //    for (int i = 0; i < minY; ++i)
        //        Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);


        //    newArray[y - 1, x - 1] = ObjectType.ROCK;
        //    return newArray;
        //}

        public void PrintMap()
        {
            for(int i = 0; i < FullMap.GetLength(0); i++) 
            {
                for(int j = leftMostRock > 0 ? leftMostRock-1 : 0; j < rightMostRock; j++)
                {
                    Console.Write(ObjectTypeToString(FullMap[i,j]));
                }
                Console.WriteLine();
            }
        }

        private string ObjectTypeToString(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.AIR:
                    return ".";
                    break;
                case ObjectType.ROCK:
                    return "#";
                    break;
                case ObjectType.SAND:
                    return "o";
                    break;
            }
            return ".";
        }
    }
}
