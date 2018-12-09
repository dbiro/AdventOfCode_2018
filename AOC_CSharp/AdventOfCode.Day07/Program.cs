using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day07
{
    class Worker
    {
        int timeToFinish;

        public bool Finished => timeToFinish == 0;
        public Step Step { get; }

        public Worker(Step step)
        {
            this.Step = step;
            this.timeToFinish = step.WorkTime;
        }

        public void DoWork()
        {
            if (timeToFinish > 0)
            {
                timeToFinish--;
            }
        }

        public override string ToString()
        {
            return $"Step: {Step}, Time: {timeToFinish}";
        }
    }

    class Step : IEquatable<Step>
    {
        public char Name { get; }
        public List<Step> Prerequisites { get; }
        public int WorkTime { get; set; }

        public Step(char name)
        {
            Name = name;
            Prerequisites = new List<Step>();
            WorkTime = name % 65 + 61;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Step);
        }

        public bool Equals(Step other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }

    class Program
    {
        static IEnumerable<(char ascendant, char descendant)> ReadInput()
        {
            var rules = new List<(char ascendant, char descendant)>();
            Regex lineMathcer = new Regex("Step (?<ascendant>.) must be finished before step (?<descendant>.) can begin.");

            string[] lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {
                char ascendant = char.Parse(lineMathcer.Match(line).Groups["ascendant"].Value);
                char descendant = char.Parse(lineMathcer.Match(line).Groups["descendant"].Value);
                rules.Add((ascendant, descendant));
            }

            return rules;
        }

        static IEnumerable<Step> SolverFirst(IEnumerable<Step> stepsInput)
        {
            List<Step> steps = stepsInput.OrderBy(s => s.Name).ToList();
            List<Step> finishedSteps = new List<Step>(steps.Count);
            Step current = null;
            while ((current = FindNextAvailableStep(finishedSteps, steps)) != null)
            {
                finishedSteps.Add(current);
            }

            return finishedSteps;
        }

        static Step FindNextAvailableStep(IEnumerable<Step> finishedSteps, List<Step> steps)
        {
            foreach (var step in steps.ToList())
            {
                if (!step.Prerequisites.Except(finishedSteps).Any())
                {
                    steps.Remove(step);
                    return step;
                }
            }

            return null;
        }

        static (IEnumerable<Step> finishedSteps, int totalSeconds) SolveSecond(IEnumerable<Step> stepsInput)
        {
            const int maxWorkerCount = 5;
            List<Worker> workers = new List<Worker>(maxWorkerCount);

            var steps = stepsInput.OrderBy(s => s.Name).ToList();
            int stepsCount = steps.Count();
            List<Step> finishedSteps = new List<Step>(stepsCount);
            Queue<int> availableSteps = new Queue<int>();

            int totalSeconds = 0;
            for (int time = 0; ; time++)
            {
                while (workers.Count < maxWorkerCount)
                {
                    Step nextStep = FindNextAvailableStep(finishedSteps, steps);
                    if (nextStep != null)
                    {
                        workers.Add(new Worker(nextStep));
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (var w in workers)
                {
                    w.DoWork();
                };

                var finishedWorkers = workers.Where(w => w.Finished).ToList();
                if (finishedWorkers.Any())
                {
                    foreach (var w in finishedWorkers)
                    {
                        finishedSteps.Add(w.Step);
                        workers.Remove(w);
                    }
                }

                if (finishedSteps.Count == stepsCount)
                {
                    totalSeconds = time + 1;
                    break;
                }
            }

            return (finishedSteps: finishedSteps, totalSeconds: totalSeconds);
        }

        static void Main(string[] args)
        {
            var rules = ReadInput();
            Dictionary<char, Step> steps = new Dictionary<char, Step>();
            foreach (var rule in rules)
            {
                if (!steps.ContainsKey(rule.descendant))
                {
                    steps[rule.descendant] = new Step(rule.descendant);
                }
                if (!steps.ContainsKey(rule.ascendant))
                {
                    steps[rule.ascendant] = new Step(rule.ascendant);
                }
                steps[rule.descendant].Prerequisites.Add(steps[rule.ascendant]);
            }

            var firstResult = SolverFirst(steps.Values);
            Console.WriteLine(string.Join("", firstResult.Select(s => s.Name)));

            var secondResult = SolveSecond(steps.Values);
            Console.WriteLine($"{string.Join("", secondResult.finishedSteps.Select(s => s.Name))} in {secondResult.totalSeconds} seconds");
        }
    }
}
