using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day10
{
    class Point
    {
        private readonly int velocityX;
        private readonly int velocityY;

        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y, int vx, int vy)
        {
            X = x;
            Y = y;
            velocityX = vx;
            velocityY = vy;
        }

        public void MoveForward()
        {
            X += velocityX;
            Y += velocityY;
        }

        public void MoveBackward()
        {
            X -= velocityX;
            Y -= velocityY;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, VX: {velocityX}, VY: {velocityY}";
        }
    }

    class Program
    {
        static IEnumerable<Point> ReadInput()
        {
            List<Point> points = new List<Point>();
            Regex lineParser = new Regex("position=<(?<positionX>.+[0-9]+),(?<positionY>.+[0-9]+)> velocity=<(?<velocityX>.+[0-9]+),(?<velocityY>.+[0-9]+)>");

            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                Match lineExtractor = lineParser.Match(line);
                points.Add(new Point(
                    int.Parse(lineExtractor.Groups["positionX"].Value), int.Parse(lineExtractor.Groups["positionY"].Value),
                    int.Parse(lineExtractor.Groups["velocityX"].Value), int.Parse(lineExtractor.Groups["velocityY"].Value)));
            }

            return points;
        }

        static void PrintPoints(IEnumerable<Point> points)
        {
            int maxX = points.Max(p => p.X);
            int maxY = points.Max(p => p.Y);
            int minX = points.Min(p => p.X);
            int minY = points.Min(p => p.Y);

            Console.BufferWidth = Console.BufferWidth < maxX - minX + 1 ? maxX - minX + 1 : Console.BufferWidth;
            Console.BufferHeight = Console.BufferHeight < maxY - minY + 1 ? maxY - minY + 1 : Console.BufferHeight;

            foreach (var p in points)
            {
                Console.CursorLeft = p.X - minX;
                Console.CursorTop = p.Y - minY;
                Console.Write("#");
            }

            Console.CursorLeft = 0;
            Console.CursorTop = maxY - minY + 1;
        }

        static void Main(string[] args)
        {
            IEnumerable<Point> points = ReadInput();

            int time = 0;
            int maxTime = 15000;

            List<int> rowsWithPointsCount = new List<int>(maxTime);
                        
            while (true)
            {
                if (time == maxTime)
                {
                    break;
                }

                foreach (var p in points)
                {
                    p.MoveForward();
                }

                int currentCount = points
                    .Select(p => p.X)
                    .Distinct()
                    .Count();

                rowsWithPointsCount.Add(currentCount);

                time++;
            }

            int minCount = rowsWithPointsCount.Min();
            int index = rowsWithPointsCount.IndexOf(minCount);

            for (int i = 0; i < maxTime - index - 1; i++)
            {
                foreach (var p in points)
                {
                    p.MoveBackward();
                }
            }

            PrintPoints(points);

            Console.WriteLine(index + 1);
        }
    }
}
