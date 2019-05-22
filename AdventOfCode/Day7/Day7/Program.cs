using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawInput = File.ReadAllLines("input.txt");
            var steps = CreateSteps(rawInput);
            AssignNextSteps(rawInput, steps);

            var part1 = GetPart1(steps);
            Console.WriteLine("Part1:{0}", part1);

            var part2 = GetPart2(steps);
            Console.WriteLine("Part2:{0}", part2);

            Console.ReadKey();
        }

        private static long GetPart2(List<Step> steps)
        {
            var workers = new List<Worker>();
            for (int i = 0; i < 5; i++)
            {
                workers.Add(new Worker());
            }
            var seconds = 0;
            var clonedSteps = steps.Select(s => s).ToList();
            var result = new List<Step>();
            var values = clonedSteps.SelectMany(s => s.NextSteps).Select(s => s.Value).Distinct().ToList();
            var nextSteps = clonedSteps.Where(s => !values.Contains(s.Value)).ToList();

            while (result.Count != steps.Count)
            {
                if (workers.Any(w=>w.IsIdle()))
                {
                    if (nextSteps.Count > 0)
                    {
                        nextSteps = nextSteps.OrderBy(s => s.Value).ToList();
                        while (nextSteps.Any())
                        {
                            if (workers.All(w=>!w.IsIdle()))
                            {
                                break;
                            }

                            var currentStep = GetNextStep(nextSteps, result, steps);
                            if (currentStep == null)
                            {
                                break;
                            }
                            var currentWorker = workers.First(w=>w.IsIdle());
                            currentWorker.Step = currentStep;
                            clonedSteps.Remove(currentStep);
                            nextSteps.Remove(currentStep);
                        }
                    }
                }

                Console.Write(seconds + " ");
                foreach (var worker in workers)
                {
                    worker.UpdateWorker();

                    if (worker.Step == null)
                    {
                        Console.Write(" . ");
                    }
                    else
                    {
                        Console.Write(' ' + worker.Step.Value.ToString() + ' ');
                    }

                    if (worker.IsFinished())
                    {
                        result.Add(worker.Step);
                        var remainingValues = clonedSteps.SelectMany(s => s.NextSteps).Select(s => s.Value).Distinct().ToList();
                        nextSteps.AddRange(clonedSteps.Where(s => !remainingValues.Contains(s.Value)));
                        nextSteps = nextSteps.GroupBy(s => s.Value).Select(g => g.First()).ToList();
                        worker.Step = null;
                    }
                }

                var res = string.Join(string.Empty, result.Select(r => r.Value));
                Console.WriteLine("   " + res);
                
                seconds++;
            }

            return seconds;
        }

        private static Step GetNextStep(List<Step> nextSteps, List<Step> result, List<Step> steps)
        {
            foreach (var nextStep in nextSteps)
            {
                var stepsBefore = steps.Where(s => s.NextSteps.Contains(nextStep)).ToList();
                var isValid = stepsBefore.All(n => result.Contains(n));
                if (isValid)
                {
                    return nextStep;
                }
            }

            return null;
        }

        private static string GetPart1(List<Step> steps)
        {
            var clonedSteps = steps.Select(s => s).ToList();
            var result = string.Empty;
            var values = clonedSteps.SelectMany(s => s.NextSteps).Select(s => s.Value).Distinct().ToList();
            var nextSteps = clonedSteps.Where(s => !values.Contains(s.Value)).ToList();

            while (nextSteps.Any())
            {
                var currentStep = nextSteps.OrderBy(s => s.Value).First();
                result += currentStep.Value;
                nextSteps.Remove(currentStep);
                clonedSteps.Remove(currentStep);
                var remainingValues = clonedSteps.SelectMany(s => s.NextSteps).Select(s => s.Value).Distinct().ToList();
                nextSteps.AddRange(clonedSteps.Where(s => !remainingValues.Contains(s.Value)));
                nextSteps = nextSteps.GroupBy(s => s.Value).Select(g => g.First()).ToList();
            }

            return result;
        }

        private static void AssignNextSteps(string[] rawInput, List<Step> steps)
        {
            foreach (var inputLine in rawInput)
            {
                var firstStepValue = GetFirstStep(inputLine);
                var secondStepValue = GetSecondStep(inputLine);

                var firstStep = steps.First(s => s.Value == firstStepValue);
                var secondStep = steps.First(s => s.Value == secondStepValue);
                firstStep.NextSteps.Add(secondStep);
            }
        }

        private static List<Step> CreateSteps(string[] rawInput)
        {
            var steps = new List<Step>();
            foreach (var inputLine in rawInput)
            {
                var firstStep = GetFirstStep(inputLine);
                if (steps.FirstOrDefault(s => s.Value == firstStep) == null)
                {
                    steps.Add(new Step(firstStep));
                }

                var secondStep = GetSecondStep(inputLine);
                if (steps.FirstOrDefault(s => s.Value == secondStep) == null)
                {
                    steps.Add(new Step(secondStep));
                }
            }

            return steps;
        }

        private static char GetSecondStep(string inputLine)
        {
            return Convert.ToChar(inputLine.Substring(36, 1));
        }

        private static char GetFirstStep(string inputLine)
        {
            return Convert.ToChar(inputLine.Substring(5, 1));
        }
    }
}
