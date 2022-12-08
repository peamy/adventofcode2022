using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._5
{
    public class Puzzle5
    {
        private Dictionary<int, Stack<char>> Boxes;
        private Queue<Instruction> Instructions;

        public Puzzle5()
        {
        }

        public void SolvePartOne()
        {
            InitializeData(true);
            while (Instructions.Count > 0)
            {
                var instruciton = Instructions.Dequeue();
                ProcessInstruction(instruciton);
            }
            PrintFirstOfEveryStack();
        }

        public void SolvePartTwo()
        {
            InitializeData();
            while (Instructions.Count > 0)
            {
                var instruciton = Instructions.Dequeue();
                ProcessInstruction2(instruciton);
            }
            PrintFirstOfEveryStack();
        }

        private void PrintFirstOfEveryStack()
        {
            string result = "";
            foreach(var boxes in Boxes)
            {
                result += boxes.Value.Peek();
            }
            Console.WriteLine(result);
        }

        private void InitializeData(bool test = false)
        {
            Boxes = new Dictionary<int, Stack<char>>();
            Instructions = new Queue<Instruction>();

            string file = test ? "C:\\Users\\rvanes\\Documents\\Repos\\Other Programming\\AoC\\AoC2022\\AoC2022\\Puzzles\\5\\Input\\TestInput.txt" : "C:\\Users\\rvanes\\Documents\\Repos\\Other Programming\\AoC\\AoC2022\\AoC2022\\Puzzles\\5\\Input\\Input.txt";
            var input = DataReader.ReadFile(file);

            int i = 0;
            int amountOfRows = (input[0].Length + 1) / 4;

            for(int q = 0; q < amountOfRows; q++)
            {
                Boxes[q] = new Stack<char>();
            }

            for(i=i; i < input.Length; i++)
            {
                string line = input[i];

                if (line.Substring(1,1) == "1")
                {
                    i+=2;
                    break;
                }

                for (int j = 0; j < amountOfRows; j++)
                {
                    AddBoxForColumn(line, j);
                }
            }

            ReverseBoxes();


            for (i = i; i < input.Length; i++)
            {
                var linessplit = input[i].Split(' ');
                Instructions.Enqueue(new Instruction()
                {
                    Amount = int.Parse(linessplit[1]),
                    From = int.Parse(linessplit[3]) - 1,
                    To = int.Parse(linessplit[5]) - 1,

                });
            }
        }

        private void AddBoxForColumn(string line, int col)
        {
            char c = line.Substring((col * 4) + 1, 1)[0];
            if(c !=' ')
            {
                Boxes[col].Push(c);
            }
        }

        private void ReverseBoxes()
        {
            foreach(var box in Boxes)
            {
                var newchar = new Stack<char>();
                while(box.Value.Count > 0)
                {
                    newchar.Push(box.Value.Pop());
                }
                Boxes[box.Key] = newchar;
            }
        }

        private void ProcessInstruction(Instruction instruction)
        {
            for(int i = 0; i < instruction.Amount; i++)
            {
                Boxes[instruction.To].Push(Boxes[instruction.From].Pop());
            }
        }

        private void ProcessInstruction2(Instruction instruction)
        {
            Stack<char> tmpStack = new Stack<char>();
            for (int i = 0; i < instruction.Amount; i++)
            {
                tmpStack.Push(Boxes[instruction.From].Pop());
            }

            while(tmpStack.Count > 0) 
            {
                Boxes[instruction.To].Push(tmpStack.Pop());
            }
        }

    }

    public class Instruction
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Amount { get; set; }
    }
}
