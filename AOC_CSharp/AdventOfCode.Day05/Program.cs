using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day05
{
    static class PolymerExtensions
    {
        public static int React(this IEnumerable<char> polymer, Func<char, char, bool> canReact)
        {
            var polymerList = polymer.ToList();

            int i = 0;
            while (i < polymerList.Count - 1)
            {
                if (canReact(polymerList[i], polymerList[i + 1]))
                {
                    polymerList.RemoveRange(i, 2);
                    if (i > 0)
                    {
                        i--;
                    }
                }
                else
                {
                    i++;
                }
            }
            return polymerList.Count;
        }

        public static IEnumerable<Char> Remove(this IEnumerable<Char> polymer, char c)
        {
            foreach (var u in polymer)
            {
                if (char.ToLower(c) != char.ToLower(u))
                    yield return u;
            }
        }
    }

    class Program
    {
        static IEnumerable<char> ReadInput()
        {
            string input = File.ReadAllText("input.txt");
            return input.ToList();
        }

        static bool CanReact(char unitA, char unitB)
        {
            return Math.Abs(unitA - unitB) == 32;
        }                

        static IEnumerable<char> GetAlphabet()
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                yield return c;
            }
        }

        static void Main(string[] args)
        {
            IEnumerable<char> polymer = ReadInput();

            int firsResult = polymer.React(CanReact);
            Console.WriteLine(firsResult);
                        
            int secondResult = GetAlphabet()
                .AsParallel()
                .Select(r => polymer.Remove(r))
                .Min(p => p.React(CanReact));
            Console.WriteLine(secondResult);
        }
    }
}
