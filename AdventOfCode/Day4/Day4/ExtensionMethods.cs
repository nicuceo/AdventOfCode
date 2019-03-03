using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public static class ExtensionMethods
    {
        public static string SubstringBetweenChars(this string input, char first, char last)
        {
            return input.Substring(input.IndexOf(first) + 1, input.IndexOf(last) - input.IndexOf(first) - 1);
        }
    }
}