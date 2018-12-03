using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day01
{
    class Day01Solver : ISolver
    {
        public int Compute(IEnumerable<int> frequencyChanges)
        {
            var changes = frequencyChanges.ToList();
            int numberOfChanges = changes.Count;

            int currentFrequency = 0;

            HashSet<int> frequencies = new HashSet<int>();
            frequencies.Add(0);

            int i = 0;
            for (i = 0; ; i++)
            {
                currentFrequency += changes[i % numberOfChanges];
                if (!frequencies.Contains(currentFrequency))
                {
                    frequencies.Add(currentFrequency);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Current frequency: {currentFrequency} found in iteration: {i / numberOfChanges}");

            return currentFrequency;
        }
    }
}
