using AoC2022.Common;
using AoC2022.Puzzles._5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._6
{
    public class Puzzle6
    {
        string[] input;
        public void SolvePartOne()
        {
            initializeData();
            foreach(string s in input)
            {
                Console.WriteLine(GetStarterPacket2(s, 4));
            }            
        }
        public void SolvePartTwo()
        {
            foreach (string s in input)
            {
                Console.WriteLine(GetStarterPacket2(s, 14));
            }
        }

        private void initializeData()
        {
            input = DataReader.ReadForDay(6);
        }

        private int GetStarterPacket(string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                bool pass1 = s[i] != s[i+1] && s[i] != s[i + 2] && s[i] != s[i + 3];
                bool pass2 = s[i + 1] != s[i + 2] && s[i + 1] != s[i + 3];
                bool pass3 = s[i + 2] != s[i + 3];
                if (pass1 && pass2 && pass3)
                    return i + 4;
            }
            return 0;
        }

        private int GetStarterPacket2(string s, int amount)
        {
            for (int i = 0; i < s.Length; i++)
            {
                bool pass = true;
                string sub = s.Substring(i, amount);
                foreach(char c in sub)
                {
                    if (sub.Where(ch => ch == c).Count() != 1)
                        pass = false;
                }
                if (pass)
                    return i + amount;
            }

            return 0;
        }
    }
}
