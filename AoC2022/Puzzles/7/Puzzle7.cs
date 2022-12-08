using AoC2022.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022.Puzzles._7
{
    public class Puzzle7
    {
        private Directory root;
        public void SolvePartOne()
        {
            InitializeData();
            var alldirs = root.GetDirectories();
            var filtereddirs = alldirs.Where(d => d.GetSizeWithChildren() < 100000);
            var sum = filtereddirs.Sum(d => d.GetSizeWithChildren());
            Console.WriteLine(sum);
        }

        public void SolvePartTwo()
        {
            int totalspace = 70000000;
            int requiredspace = 30000000;
            int usedspace = root.GetSizeWithChildren();
            int needtodelete = requiredspace + usedspace - totalspace;
            var alldirs = root.GetDirectories();
            var mydir = alldirs.Where(d => d.GetSizeWithChildren() > needtodelete).OrderBy(d => d.GetSizeWithChildren()).FirstOrDefault();
            Console.WriteLine(mydir.GetSizeWithChildren());
        }

        private void InitializeData()
        {
            var input = DataReader.ReadForDay(7, false);
            Directory currentDirectory;
            root = new Directory("/");
            currentDirectory = root;
            for (int i = 1; i < input.Length; i++)
            {
                string command = input[i];
                if (command == "$ cd /")
                {
                    currentDirectory = root;
                }
                else if (command.StartsWith("$ cd"))
                {
                    currentDirectory = currentDirectory.GoTo(command.Split(' ')[2]);
                }
                else if (!command.Equals("$ ls"))
                {
                    if (command.StartsWith("dir"))
                    {
                        currentDirectory.AddDirectory(command.Split(' ')[1]);
                    }
                    else
                    {
                        currentDirectory.AddFile(command.Split(' ')[1], int.Parse(command.Split(' ')[0]));
                    }
                }
            }
        }
    }

    public class Directory
    {
        public List<File> Files;
        public Directory Parent;
        public List<Directory> Directorys;
        public string Name;

        public Directory(string name)
        {
            Name = name;
            Files = new List<File>();
            Directorys = new List<Directory>();
        }

        public Directory GoTo(string input)
        {
            if (input == "..")
                return Parent;
            return Directorys.Where(d => d.Name == input).FirstOrDefault();
        }

        public void AddDirectory(string name)
        {
            var newdir = new Directory(name) { Parent = this };
            Directorys.Add(newdir);
        }

        public void AddFile(string name, int size)
        {
            Files.Add(new File() { Name = name, Size = size });
        }

        public int GetSize()
        {
            return Files.Sum(f => f.Size);
        }

        public int GetSizeWithChildren()
        {
            return Files.Sum(f => f.Size) + Directorys.Sum(d => d.GetSizeWithChildren());
        }

        public List<Directory> GetDirectories()
        {
            var list = new List<Directory>();
            foreach (Directory dir in Directorys)
            {
                list.AddRange(dir.GetDirectories());
            }
            if (!list.Contains(this))
            {
                list.Add(this);
            }
            return list;
        }

    }

    public class File
    {
        public int Size;
        public string Name;
    }
}
