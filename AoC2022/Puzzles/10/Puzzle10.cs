using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC2022.Common;

namespace AoC2022.Puzzles
{
    public  class Puzzle10
    {
        private int[] signalsPartOne = new int[] { 20, 60, 100, 140, 180, 220 };
        private Command currentCommand;

        private Queue<Command> commands;

        public void SolvePartOne()
        {
            return;
            InitializeData();
            int cycles = 0;
            int x = 1;
            int result = 0;
            while(commands.Count > 0)
            {
                if (signalsPartOne.Contains(cycles))
                {
                    Console.WriteLine($"{cycles} {x} {x * cycles}");
                    result += x * cycles;
                }

                if (currentCommand == null)
                {
                    NextCommand();
                }
                else if(currentCommand.cycles == 0)
                {
                    x += currentCommand.addition;
                    NextCommand();
                }
                currentCommand.cycles--;
                cycles++;
            }
            Console.WriteLine($"Result pt1: {result}");
        }

        public void SolvePartTwo()
        {
            InitializeData();
            int cycles = 0;
            int witdh = 40;
            int x = 1;
            string result = "\n";
            string tmpresult = "";
            while (commands.Count > 0)
            {
                if (currentCommand == null)
                {
                    NextCommand();
                }
                else if (currentCommand.cycles == 0)
                {
                    x += currentCommand.addition;
                    NextCommand();
                }

                if (x <= cycles + 1 && x >= cycles - 1)
                    tmpresult += "#";
                else
                    tmpresult += ".";


                currentCommand.cycles--;
                cycles++;

                // counter for 20 pixels
                if (cycles == witdh)
                {
                    result += tmpresult + "\n";
                    tmpresult = "";
                    cycles = 0;
                    Console.WriteLine(result);
                }

            }
            Console.WriteLine($"Result pt2: {result}");
        }

        private void NextCommand()
        {
            currentCommand = commands.Dequeue();
        }

        private void InitializeData(bool dummy = false)
        {
            commands = new Queue<Command>();
            var input = DataReader.ReadForDay(10, dummy);
            foreach(string s in input)
            {
                if(s == "noop")
                {
                    commands.Enqueue(new Command(1, 0));
                }
                else
                {
                    commands.Enqueue(new Command(2, int.Parse(s.Split(' ')[1])));
                }
            }
        }
    }

    internal class Command
    {
        public int cycles;
        public int addition;
        public Command(int cycles, int addition)
        {
            this.cycles = cycles;
            this.addition = addition;
        }
    }
}
