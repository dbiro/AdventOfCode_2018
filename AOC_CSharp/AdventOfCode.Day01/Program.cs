using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day01
{
    class Program
    {
        private static int[] ReadFequencyChanges()
        {
            string[] lines = File.ReadAllLines("input.txt");
            return lines.Select(l => int.Parse(l)).ToArray();
        }

        static void Main(string[] args)
        {
            int[] changes = ReadFequencyChanges();

            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            ISolver solver = new Day01Solver();
            int result = solver.Compute(changes);

            Console.ForegroundColor = currentColor;

            Console.WriteLine($"Result: {result}");
        }
    }
}
