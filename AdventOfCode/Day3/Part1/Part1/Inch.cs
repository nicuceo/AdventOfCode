using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class Inch
    {
        public int X { get; set; }
        public int Y { get; set; }

        public  bool Equals(Inch other)
        {
            return X == other.X && Y == other.Y;
        }
    }

}
