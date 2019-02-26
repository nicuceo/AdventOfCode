using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class Claim
    {
        public Claim(string claim)
        {
            Id = int.Parse(claim.SubstringBetweenChars('#', '@'));
            DistanceFromLeft = int.Parse(claim.SubstringBetweenChars('@', ','));
            DistanceFromTop = int.Parse(claim.SubstringBetweenChars(',', ':'));
            Width = int.Parse(claim.SubstringBetweenChars(':', 'x'));
            Height = int.Parse(claim.Substring(claim.IndexOf('x') + 1));
            Inches = CalculateInches();
        }

        private List<Inch> CalculateInches()
        {
            var result = new List<Inch>();

            for (var x = DistanceFromLeft; x < DistanceFromLeft + Width; x++)
            {
                for (var y = DistanceFromTop; y < DistanceFromTop + Height; y++)
                {
                    result.Add(new Inch
                    {
                        X = x,
                        Y = y
                    });
                }
            }

            return result;
        }

        public int Id { get; set; }
        public int DistanceFromLeft { get; set; }
        public int DistanceFromTop { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Inch> Inches { get; set; }
    }
}
