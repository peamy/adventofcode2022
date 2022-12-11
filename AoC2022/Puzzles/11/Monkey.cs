using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._11
{
    public class Monkey
    {
        public long MagicNumber;
        public int Inspections = 0;
        public int Number { get; set; }
        public Monkey IfTrue { get; set; }
        public Monkey IfFalse { get; set; }

        public List<Int64> Items { get; set; }
        public int DivisibleBy { get; set; }

        public Operation operation;
        public int Amount;

        public Monkey(int Number) 
        {
            this.Number = Number;
            Items = new List<Int64>();
        }

        public void CalculateAndThrow(bool partone = true)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                Items[i] = Inspect(Items[i], partone);

                ThrowItem(Items[i]);
            }
            Items = new List<Int64>();
        }

        private Int64 Inspect(Int64 item, bool partone)
        {
            Inspections++;
            switch (operation)
            {
                case Operation.MULTIPLY:
                    item = item * Amount;
                    break;
                case Operation.SQUARE:
                    item = item * item;
                    break;
                case Operation.ADD:
                    item = item + Amount;
                    break;
            }

            if (partone)
                return item / 3;

            long rest = item % MagicNumber;
            return rest;
        }

        private void ThrowItem(Int64 value)
        {
            if(value % DivisibleBy == 0)
            {
                IfTrue.Items.Add(value);
            }
            else
            {
                IfFalse.Items.Add(value);
            }
        }
    }

    public enum Operation
    {
        MULTIPLY,
        SQUARE,
        ADD
    }
}
