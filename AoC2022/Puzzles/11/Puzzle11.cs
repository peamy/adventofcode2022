using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AoC2022.Common;
using AoC2022.Puzzles._11;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2022.Puzzles
{
    public class Puzzle11
    {
        private List<Monkey> Monkeys;
        public void SolvePartOne()
        {
            //Solve(20);
        }

        public void SolvePartTwo()
        {
            Solve(10000, false);
            // 2981924385 too low (oops that was wrong data for test input)
        }

        private void Solve(int rounds, bool partone=true)
        {

            InitMonkeys();
            for (int i = 0; i < rounds; i++)
            {
                //LogMonkeys(i);
                DoARound(partone);
            }

            var orderedMonkeys = Monkeys.OrderByDescending(m => m.Inspections).ToList();
            Int64 monkeyOne = orderedMonkeys[0].Inspections;
            Int64 monkeyTwo = orderedMonkeys[1].Inspections;

            Int64 monkeyBusiness = monkeyOne * monkeyTwo;
            Console.WriteLine(monkeyBusiness);
        }

        private void DoARound(bool partone = true)
        {
            foreach(Monkey monkey in Monkeys)
            {
                monkey.CalculateAndThrow(partone);
            }
        }

        private void LogMonkeys(int roundNr)
        {
            Console.WriteLine($"Summary for round number {roundNr}");
            foreach(var monkey in Monkeys)
            {
                string items = "";
                foreach(var item in monkey.Items)
                {
                    items += item.ToString() + ", ";
                }
                Console.WriteLine($"Monkey: {monkey.Number} Inspections: {monkey.Inspections} Items: {items}");
            }
        }

        private void InitMonkeys(bool dummy = false)
        {
            long magicNumber = 1;
            Monkeys = new List<Monkey>();
            var input = DataReader.ReadForDay(11, dummy);
            for(int i = 0; i < input.Length-5; i+=7)
            {
                var newMonkey = new Monkey(int.Parse(input[i].Split(' ')[1].Substring(0,1)));
                Operation op = Operation.MULTIPLY;

                if (input[i + 2].Contains("+"))
                    op = Operation.ADD;
                if (input[i + 2].Contains("old * old"))
                    op = Operation.SQUARE;
                newMonkey.operation = op;

                if (op != Operation.SQUARE)
                    newMonkey.Amount = int.Parse(input[i + 2].Split(" ")[7]);

                newMonkey.DivisibleBy = int.Parse(input[i + 3].Split(" ")[5]);

                var itemsSplit = input[i + 1].Split(":")[1].Split(',');
                for(int j = 0; j<itemsSplit.Length; j++)
                {
                    newMonkey.Items.Add(Int64.Parse(itemsSplit[j]));
                }

                newMonkey.IfTrue = new Monkey(int.Parse(input[i + 4].Split(" ")[9]));
                newMonkey.IfFalse = new Monkey(int.Parse(input[i + 5].Split(" ")[9]));

                magicNumber *= newMonkey.DivisibleBy;

                Monkeys.Add(newMonkey);
            }

            foreach(Monkey monkey in Monkeys)
            {
                monkey.IfTrue = Monkeys.Where(m => m.Number == monkey.IfTrue.Number).FirstOrDefault();
                monkey.IfFalse = Monkeys.Where(m => m.Number == monkey.IfFalse.Number).FirstOrDefault();
                monkey.MagicNumber = magicNumber;
            }

            //Monkey 0:
            //Starting items: 63, 84, 80, 83, 84, 53, 88, 72
            //Operation: new = old * 11
            //Test: divisible by 13
            //  If true: throw to monkey 4
            //  If false: throw to monkey 7

            //0 1 2 3 4 5 
            //6 
            //7 8 9 10 11
            //12
        }


    }
}
