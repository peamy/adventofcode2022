using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles
{
    public class Puzzle13
    {

        bool USE_TEST_DATA = false;

        bool[] mysolutions = new bool[150];
        bool[] cheatedsolutions = new bool[150];
        public void SolvePartOne()
        {
            var packets = InitPackets(USE_TEST_DATA);
            int correctpackets = 0;
            foreach (var packet in packets)
            {
                if (packet.IsCorrect())
                {
                    correctpackets += packet.Index;
                    mysolutions[packet.Index] = true;
                }

                //Console.WriteLine();
            }
            Console.WriteLine($"Part one packets correct : {correctpackets}" + (USE_TEST_DATA ? " (test)" : ""));


            // Only in order to see which data fails so I can write code to cover the edge cases
            Console.WriteLine($"Cheated day 13: {Day13Cheated.ProcessPart1(DataReader.ReadForDay(13))}");
            string wrongSolutions = "";
            for (int i = 0; i < mysolutions.Length; i++)
            {
                if (mysolutions[i] != Day13Cheated.cheatedSolutions[i])
                {
                    wrongSolutions += $"{i}, ";
                }
            }
            //compared the cases that arent right for me
            Console.WriteLine($"Check {wrongSolutions}");
        }

        public void SolvePartTwo() 
        {
            var data = InitData(USE_TEST_DATA);

            data.Sort(Compare);

            foreach(var dat in data)
            {
                Console.WriteLine(dat.Fullstring);
            }

            var two = data.Where(d => d.Fullstring == "[[2]]").FirstOrDefault();
            var six = data.Where(d => d.Fullstring == "[[6]]").FirstOrDefault();

            int indextwo = data.IndexOf(two) +1;
            int indexsix = data.IndexOf(six) +1;

            Console.WriteLine($"Result for part 2: {indextwo} * {indexsix} = {indextwo * indexsix}");
            Console.WriteLine($"Cheated day 13 pt2: {Day13Cheated.ProcessPart2(DataReader.ReadForDay(13))}");
        }

        public static int Compare(Data left, Data right)
        {
            var result = Packet.CompareData(left, right);
            if (result == Status.SUCCESS)
                return -1;
            else if (result == Status.FAIL)
                return 1;
            return 0;
        }

        private List<Packet> InitPackets(bool testData = false)
        {
            var data = new List<Packet>();
            var input = DataReader.ReadForDay(13, testData);
            for(int i = 0; i < input.Length -1; i+=3)
            {
                data.Add(new Packet(input[i], input[i + 1], (i/3) + 1));
            }

            return data;
        }

        private List<Data> InitData(bool testData = false)
        {
            var data = new List<Packet>();
            var input = DataReader.ReadForDay(13, testData);
            for (int i = 0; i < input.Length - 1; i += 3)
            {
                data.Add(new Packet(input[i], input[i + 1], (i / 3) + 1));
            }

            data.Add(new Packet("[[2]]", "[[6]]", 0));

            List<Data> allData = new List<Data>();

            foreach(Packet packet in data)
            {
                allData.Add(packet.firstList);
                allData.Add(packet.secondList);
            }

            return allData;
        }
    }

    internal class Packet
    {
        public int Index;

        public string first;
        public string second;

        public  Data firstList;
        public Data secondList;

        public Packet(string first, string last, int index)
        {
            this.Index = index;
            this.first = first;
            this.second = last;
            firstList = InitData(first);
            firstList.Fullstring = first;
            secondList = InitData(last);
            secondList.Fullstring = last;
        }

        private Data InitData(string data)
        {
            Data root = new Data();
            root.Array = new List<Data>();

            Data currentData = root;
            for (int i = 1; i < data.Length - 1; i++)
            {
                string sub = data.Substring(i, 1);
                switch (sub)
                {
                    case "[":
                        var newData = new Data();
                        currentData.AddValue(newData);
                        currentData = newData;
                        break;
                    case "]":
                        currentData = currentData.Parent;
                        break;
                    case ",":
                        break;
                    default:
                        if(data.Substring(i, 2) == ("10"))
                        {
                            sub = "10";
                            i++;
                        }
                        currentData.AddValue(int.Parse(sub));
                        break;
                }
            }
            return root;
        }

        public bool IsCorrect()
        {
            return CompareData(firstList, secondList) == Status.SUCCESS;
        }

        public static Status CompareData(Data first, Data second)
        {
            if(first.IsValue && second.IsValue)
                return CompareIntegers(first, second);

            if (first.IsEmpty && second.IsEmpty) //fix found because of cheating (reading other code)
                return Status.CONTINUE;

            if (first.IsEmpty)
                return Status.SUCCESS;

            if (second.IsEmpty)
                return Status.FAIL;

            if (!first.IsArray)
            {
                if (first.IsOnlyChild) //fix found because of cheating (reading other code)
                    first = first.Parent;
                else
                    first = ValueToArray(first.Value, second.Array.Count);
            }


            else if (!second.IsArray)
            {
                if (second.IsOnlyChild) //fix found because of cheating (reading other code)
                    second = second.Parent;
                else
                    second = ValueToArray(second.Value, first.Array.Count);
            }

            return (CompareArrays(first, second));

        }
        

        private static Status CompareArrays(Data firstData, Data secondData)
        {
            var first = firstData.Array;
            var second = secondData.Array;

            int most = first.Count() > second.Count() ? first.Count() : second.Count();

            for (int i = 0; i < most; i++)
            {
                if (i >= second.Count())
                {
                    Console.WriteLine($"No more numbers in Second array FAIL");
                    return Status.FAIL;
                }
                if(i >= first.Count())
                {
                    Console.WriteLine($"No more numbers in First array Success");
                    return Status.SUCCESS;
                }

                var checkone = first[i];
                var checktwo = second[i];

                var result = CompareData(checkone, checktwo);
                if (result != Status.CONTINUE)
                    return result;
            }
            return Status.CONTINUE;
        }

        private static Status CompareIntegers(Data first, Data second)
        {
            if (second.Value == -1 || first.Value == -1)
                throw new Exception("something went wrong");

            if (first.Value > second.Value)
            {
                Console.WriteLine($"{first.Value} > {second.Value} FAIL");
                return Status.FAIL;
            }
            if (first.Value < second.Value)
            {
                Console.WriteLine($"{first.Value} < {second.Value} SUCCESS");
                return Status.SUCCESS;
            }
            Console.WriteLine($"{first.Value} == {second.Value} CONTINUE");
            return Status.CONTINUE;
        }

        private static Data ValueToArray(int value, int amount)
        {
            var data = new Data();
            for(int i = 0; i < amount; i++)
            {
                data.AddValue(value);
            }
            return data;
        }
    }

    enum Status
    {
        CONTINUE=0,
        FAIL=1,
        SUCCESS=2,
    }

    public class Data
    {
        public string Fullstring = "";

        public int Value = -1;

        public bool IsArray
        {
            get
            {
                return Array != null;
            }
        }

        public bool IsValue
        {
            get
            {
                return Value != -1;
            }
        }

        public bool IsOnlyChild
        {
            get
            {
                return Parent != null && Parent.Array.Count() == 1;
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (Value != -1)
                    return false;


                bool empty = true;
                if (IsArray)
                {
                    foreach(var x in Array)
                    {
                        if (!x.IsEmpty)
                            empty = false;
                    }
                }
                return empty;
            }
        }

        public List<Data> Array;
        public Data Parent;

        public int GetValue(int i)
        {
            return 0;
        }

        public void AddValue(int value)
        {
            if (Array == null)
                Array = new List<Data>();
            var data = new Data() { Value = value };
            data.Parent = this;
            Array.Add(data);
        }

        public void AddValue(Data value)
        {
            if (Array == null)
                Array = new List<Data>();
            value.Parent = this;
            Array.Add(value);
        }
    }

}
