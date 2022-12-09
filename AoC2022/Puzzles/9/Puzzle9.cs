using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles
{
    public class Puzzle9
    {
        string[] input;
        Head head;
        Tail tail;
        public Puzzle9()
        {
        }

        private void InitDataPt1()
        {
            int gridSize = 20;
            input = DataReader.ReadForDay(9, true);


            head = new Head(gridSize / 2, gridSize / 2); //1
            tail = new Tail(gridSize / 2, gridSize / 2); //10
            tail.Head = head;
            tail.positions = new int[gridSize, gridSize];
        }

        private void InitDataPt2()
        {
            bool dummtInput = false;

            int gridSize = dummtInput ?40 : 400;
            input = DataReader.ReadForDay(9, dummtInput);


            head = new Head(gridSize / 2, gridSize / 2); //1
            var two = new Knot(gridSize / 2, gridSize / 2); //2
            var three = new Knot(gridSize / 2, gridSize / 2); //3
            var fo = new Knot(gridSize / 2, gridSize / 2); //4
            var fi = new Knot(gridSize / 2, gridSize / 2); //5
            var siz = new Knot(gridSize / 2, gridSize / 2); //6
            var svn = new Knot(gridSize / 2, gridSize / 2); //7
            var ate = new Knot(gridSize / 2, gridSize / 2); //8
            var nine = new Knot(gridSize / 2, gridSize / 2); //9
            tail = new Tail(gridSize / 2, gridSize / 2); //10

            two.Head = head;
            three.Head = two;
            fo.Head = three;
            fi.Head = fo;
            siz.Head = fi;
            svn.Head = siz;
            ate.Head = svn;
            nine.Head = ate;
            tail.Head = nine;
            tail.positions = new int[gridSize, gridSize];
            tail.SetPosition();
        }


        public void SolvePartOne()
        {
            return;
            InitDataPt1();
            foreach (var movement in input)
            {
                MoveHead(movement);
            }
            PrintGrid();
            int total = 0;
            foreach(int pos in tail.positions)
            {
                if (pos != 0)
                    total++;
            }
            Console.WriteLine(total);
        }

        public void SolvePartTwo() 
        {
            InitDataPt2();

            foreach (var movement in input)
            {
                MoveHead(movement);
            }
            PrintGrid();
            int total = 0;
            foreach (int pos in tail.positions)
            {
                if (pos != 0)
                    total++;
            }
            Console.WriteLine(total);
        }

        private void MoveHead(string movement)
        {
            var split = movement.Split(' ');
            int amount = int.Parse(split[1]);
            string direction = split[0];

            int directionx = 0;
            int directiony = 0;
            
            if(direction == "U")
                directiony = 1;
            if(direction == "D")
                directiony = -1;
            if (direction == "R")
                directionx = 1;
            if (direction == "L")
                directionx = -1;

            for(int i = 0; i < amount; i++)
            {
                head.MoveHead(directionx, directiony);
                //LogPositions();
            }
        }

        private void PrintGrid()
        {
            for(int i = 0; i < tail.positions.GetLength(0); i++)
            {
                string line = "";
                for (int j = 0; j < tail.positions.GetLength(1); j++)
                {
                    string add = tail.positions[tail.positions.GetLength(0) - (i+1), j] == 0 ? "." : "\'";
                    line += add;
                }
                Console.WriteLine(line);
            }
        }

        private void LogPositions()
        {
            Console.WriteLine($"HEAD{head.x},{head.y} TAIL{tail.x},{tail.y}");
        }


        internal class Knot
        {
            public Knot head;
            public Knot Tail;

            public Knot Head
            {
                get { return head; }
                set { 
                    head = value;
                    head.Tail = this;
                }
            }

            public Knot(int x, int y)
            {
                this.x = x; 
                this.y = y;
            }
            public int x;
            public int y;
            

            public virtual void Move()
            {
                if (!hasToMove())
                    return;

                if (this.x < Head.x)
                    this.x++;
                else if (this.x != Head.x)
                    this.x--;
                if (this.y < Head.y)
                    this.y++;
                else if (this.y != Head.y)
                    this.y--;

                PostMove();
            }

            public virtual void PostMove()
            {
                Tail.Move();
                return;
            }

            private bool hasToMove()
            {
                return Head.y > this.y + 1 || Head.y < this.y - 1 || Head.x > this.x + 1 || Head.x < this.x - 1;
            }
        }

        internal class Head : Knot
        {
            public Head(int x, int y) : base(x, y) { }

            public void MoveHead(int dirx, int diry)
            {
                this.x += dirx;
                this.y += diry;
                Tail.Move();
            }
        }

        internal class Tail : Knot
        {

            public int[,] positions;
            public Tail(int x, int y) : base (x, y) { }

            public override void PostMove()
            {
                SetPosition();
            }

            public void SetPosition()
            {
                positions[y, x] = 1;
            }
        }

    }
}
