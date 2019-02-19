using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = File.ReadAllLines("C:\\Users\\nick_\\source\\repos\\AdventOfCode\\Day2\\Day2\\input.txt");
            var twoLettersCounter = 0;
            var threeLettersCounter = 0;

            foreach (var word in words)
            {
                var letters = word.ToCharArray().Distinct();
                var twoLetterFound = false;
                var threeLetterFound = false;
                foreach (var letter in letters)
                {
                    var noOfPresence = word.Count(chr => letter.Equals(chr));

                    if (noOfPresence == 2 && !twoLetterFound)
                    {
                        twoLettersCounter++;
                        twoLetterFound = true;
                    }
                    else if (noOfPresence == 3 && !threeLetterFound)
                    {
                        threeLettersCounter++;
                        threeLetterFound = true;
                    }
                }
            }

            var result = twoLettersCounter * threeLettersCounter;
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
