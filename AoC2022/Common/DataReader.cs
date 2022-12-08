using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Common
{
    public class DataReader
    {
        public static string[] ReadFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public static string[] ReadForDay(int day, bool test = false)
        {
            string testi = test ? "Test" : "";
            string filepath = $"C:\\Users\\rvanes\\Documents\\Repos\\Other Programming\\AoC\\AoC2022\\AoC2022\\Puzzles\\{day}\\Input\\{testi}Input.txt";
            return File.ReadAllLines(filepath);
        }
    }
}
