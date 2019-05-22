using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6
{
    class Cell
    {
        public Cell(string line)
        {
            var splitted =  line.Split(',');
            X = int.Parse(splitted[0]);
            Y = int.Parse(splitted[1]);
            Value = Guid.NewGuid();
            IsPrincipal = true;
            Children = new List<Cell>();
        }

        public Cell()
        {
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Guid Value { get; set; }
        public int Distance { get; set; }
        public bool IsPrincipal { get; set; }
        public List<Cell> Children { get; set; }
    }
}
