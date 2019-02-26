using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
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

            foreach (var claim in claims)
            {
                foreach (var claimInch in claim.Inches)
                {
                    if (fabric[claimInch.X, claimInch.Y] == 0)
                    {
                        fabric[claimInch.X, claimInch.Y] = claim.Id;
                    }
                    else
                    {
                        fabric[claimInch.X, claimInch.Y] = -1;
                    }
                }
            }

            foreach (var claim in claims)
            {
                var claimNotOverlap = true;
                foreach (var claimInch in claim.Inches)
                {
                    if (fabric[claimInch.X, claimInch.Y] != claim.Id)
                    {
                        claimNotOverlap = false;
                        break;
                    }
                }

                if (claimNotOverlap)
                {
                    Console.WriteLine(claim.Id);
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}
