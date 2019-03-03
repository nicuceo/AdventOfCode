using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawInput = File.ReadAllLines("input.txt");
            var events = rawInput.Select(line => new Event(line)).OrderBy(line => line.Date).ToList();

            var currentId = events.ElementAt(0).GuardId;
            foreach (var @event in events)
            {
                if (!@event.GuardId.HasValue)
                {
                    @event.GuardId = currentId;
                }
                else
                {
                    currentId = @event.GuardId;
                }
            }

            var part1 = Part1(events);
            var part2 = Part2(events);
            Console.WriteLine("Part1: " + part1);
            Console.WriteLine("Part2: " + part2);
            Console.ReadKey();
        }

        private static int Part2(List<Event> events)
        {
            var guardsSleepTime = new List<GuardSleepTime>();

            for (var index = 0; index < events.Count; index++)
            {
                if (events[index].Action == Action.WakesUp)
                {
                    var guardSleepTime =
                        guardsSleepTime.FirstOrDefault(guard => guard.GuardId == events[index].GuardId);

                    if (guardSleepTime != null)
                    {
                        for (var date = events[index - 1].Date; date < events[index].Date; date = date.AddMinutes(1))
                        {
                            guardSleepTime.Minutes[date.Minute]++;
                        }
                    }
                    else
                    {
                        var sleepTime = new GuardSleepTime
                        {
                            GuardId = events[index].GuardId.Value
                        };

                        for (var date = events[index - 1].Date; date < events[index].Date; date = date.AddMinutes(1))
                        {
                            sleepTime.Minutes[date.Minute]++;

                        }
                        guardsSleepTime.Add(sleepTime);
                    }
                }
            }

            var maxMinute = 0;
            var minuteId = 0;
            var guardId = 0;
            foreach (var guardSleepTime in guardsSleepTime)
            {
                for (var index = 0; index < guardSleepTime.Minutes.Length; index++)
                {
                    if (guardSleepTime.Minutes[index] > maxMinute)
                    {
                        maxMinute = guardSleepTime.Minutes[index];
                        minuteId = index;
                        guardId = guardSleepTime.GuardId;
                    }
                }
            }

            return minuteId * guardId;
        }

        private static int Part1(List<Event> events)
        {
            var guardsSleepTime = new List<GuardSleepTime>();

            for (var index = 0; index < events.Count; index++)
            {
                if (events[index].Action == Action.WakesUp)
                {
                    var minutes = (events[index].Date - events[index - 1].Date).Minutes;
                    var guardSleepTime =
                        guardsSleepTime.FirstOrDefault(guard => guard.GuardId == events[index].GuardId);

                    if (guardSleepTime != null)
                    {
                        guardSleepTime.TotalMinutes += minutes;
                        if (guardSleepTime.MaxMinutes < minutes)
                        {
                            guardSleepTime.MaxMinutes = minutes;
                        }
                    }
                    else
                    {
                        guardsSleepTime.Add(new GuardSleepTime
                        {
                            GuardId = events[index].GuardId.Value,
                            TotalMinutes = minutes,
                            MaxMinutes = minutes
                        });
                    }
                }
            }

            var result = guardsSleepTime.OrderByDescending(g => g.TotalMinutes).FirstOrDefault();
            return result.GuardId * result.MaxMinutes;
        }
    }
}
