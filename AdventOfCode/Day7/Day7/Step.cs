using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Day7
{
    public class Step
    {
        private static readonly List<char> Chars = new List<char>{' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public Step(char value)
        {
            Value = value;
            NextSteps = new List<Step>();
            Duration = 60 + Chars.IndexOf(value);
        }

        public int Duration { get; set; }

        public char Value { get; set; }
        public List<Step> NextSteps { get; set; }
    }
}