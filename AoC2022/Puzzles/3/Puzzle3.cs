using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._3
{
    public class Puzzle3
    {
        private string[] input;
        public Puzzle3() 
        {
            input = FileReader.GetInputForDay(3);
        }    
        public void SolvePartOne()
        {
            string result = "";
            foreach(string s in input)
            {
                result += GetMatchingCharacter(s);
            }
            var score = calculateScoreForAll(result);

            Console.WriteLine(score);
        }

        public void SolvePartTwo() 
        {
            string result = "";
            for(int i = 0; i < input.Length; i+=3 )
            {
                result += GetMatchingCharacterForThreeGroups(input[i], input[i+1], input[i+2]);
            }
            var score = calculateScoreForAll(result);

            Console.WriteLine(score);
        }

        private char GetMatchingCharacter(string input)
        {
            string one = input.Substring(input.Length / 2);
            string two = input.Substring(0, input.Length / 2);

            foreach(char c in one)
            {
                if (two.Contains(c))
                {
                    return c;
                }
            }

            return 'a';
        }

        private char GetMatchingCharacterForThreeGroups(string one, string two, string three)
        {
            foreach (char c in one)
            {
                if (two.Contains(c) && three.Contains(c))
                {
                    return c;
                }
            }

            return 'a';
        }

        private int calculateScoreForAll(string input)
        {
            int total = 0;
            foreach(char c in input)
            {
                total += CharacterToValue.GetValueForCharacter(c);
            }
            return total;
        }
        
    }
}
