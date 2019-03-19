using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var part1 = GetLength(string.Copy(input));

            var part2 = Part2(input);

            Console.WriteLine("Part1: {0}", part1);
            Console.WriteLine("Part2: {0}", part2);
            Console.ReadKey();
        }

        private static int Part2(string input)
        {
            var part2 = int.MaxValue;
            for (char i = 'a'; i <= 'z'; i++)
            {
                var current = string.Copy(input);
                current = current.Replace(i.ToString(), String.Empty);
                current = current.Replace(char.ToUpper(i).ToString(), String.Empty);

                var currentResult = GetLength(current);
                if (currentResult < part2)
                {
                    part2 = currentResult;
                }
            }

            return part2;
        }

        private static int GetLength(string input)
        {
            var found = true;
            var index = 0;
            while (found)
            {
                found = false;
                for (int i = index; i < input.Length - 1; i++)
                {
                    if (PolymerReacts(input[i], input[i + 1]))
                    {
                        found = true;
                        index = i == 0 ? 0 : i - 1;
                        input = input.Substring(0, i) + input.Substring(i + 2);
                        break;
                    }
                }
            }

            return input.Length;
        }

        private static bool PolymerReacts(char a, char b)
        {
            if (char.ToLower(a) != char.ToLower(b))
            {
                return false;
            }

            if ((char.IsLower(a) && char.IsLower(b)) || (!char.IsLower(a) && !char.IsLower(b)))
            {
                return false;
            }

            return true;
        }
    }
}
