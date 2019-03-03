using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public class GuardSleepTime
    {
        public GuardSleepTime()
        {
            Minutes = new int[60];   
        }

        public int GuardId { get; set; }
        public int TotalMinutes { get; set; }
        public int MaxMinutes { get; set; }
        public int[] Minutes { get; set; }
    }
}
