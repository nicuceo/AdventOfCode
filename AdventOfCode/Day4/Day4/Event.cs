using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public class Event
    {
        public DateTime Date { get; set; }
        public Action Action { get; set; }
        public int? GuardId { get; set; }

        public Event(string eventStr)
        {
            var date = eventStr.SubstringBetweenChars('[', ']');
            Date = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var action = eventStr.Substring(eventStr.IndexOf(']') + 1);
            SetAction(action);
        }

        private void SetAction(string input)
        {
            Action action;
            switch (input)
            {
                case " wakes up":
                    action = Action.WakesUp;
                    break;
                case " falls asleep":
                    action = Action.FallsAsleep;
                    break;
                default:
                    action = Action.BeginsShift;
                    GuardId = int.Parse(input.SubstringBetweenChars('#',  'b'));
                    break;
            }

            Action = action;
        }
    }

    public enum Action
    {
        BeginsShift,
        FallsAsleep,
        WakesUp
    }
}
