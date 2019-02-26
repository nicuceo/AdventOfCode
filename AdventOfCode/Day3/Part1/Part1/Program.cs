using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawInput = File.ReadAllLines("input.txt");
            var claims = rawInput.Select(claim => new Claim(claim)).ToList();
            var maxX = claims.SelectMany(claim => claim.Inches).Max(inch => inch.X);
            var maxY = claims.SelectMany(claim => claim.Inches).Max(inch => inch.Y);

            var fabric = new int[maxX + 1, maxY + 1];

            for (var i = 0; i <= maxX; i++)
            {
                for (var j = 0 + 1; j <= maxY; j++)
                {
                    fabric[i, j] = 0;
                }
            }

            var inches = claims.SelectMany(claim => claim.Inches).ToList();

            foreach (var inch in inches)
            {
                fabric[inch.X, inch.Y]++;
            }

            var result = 0;
            for (var i = 0; i <= maxX; i++)
            {
                for (var j = 0 + 1; j <= maxY; j++)
                {
                    if (fabric[i, j] > 1)
                    {
                        result++;
                    }
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
