using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day2_Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = File.ReadAllLines("C:\\Users\\nick_\\source\\repos\\AdventOfCode\\Day2_Part2\\Day2_Part2\\input.txt");
            var result = new StringBuilder();

            for (var i = 0; i < words.Length; i++)
            {
                var currentWord = words[i];
                var matchedWord = string.Empty;

                for (var j = i + 1; j < words.Length; j++)
                {
                    var differentCharsCounter = 0;
                    var comparedWord = words[j];

                    for (var k = 0; k < currentWord.Length; k++)
                    {
                        if (!currentWord[k].Equals(comparedWord[k]))
                        {
                            differentCharsCounter++;
                        }

                        if (differentCharsCounter > 1)
                        {
                            break;
                        }
                    }

                    if (differentCharsCounter == 1)
                    {
                        matchedWord = comparedWord;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(matchedWord))
                {
                    for (var k = 0; k < currentWord.Length; k++)
                    {
                        if (currentWord[k].Equals(matchedWord[k]))
                        {
                            result.Append(currentWord[k]);
                        }
                    }

                    break;
                }
            }

            Console.WriteLine(result.ToString());
            Console.ReadKey();
        }
    }
}
