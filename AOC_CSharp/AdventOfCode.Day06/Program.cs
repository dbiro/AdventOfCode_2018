using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day06
{
    class Point
    {
        public int Id { get; set; }
        public int X { get; }
        public int Y { get; }
        public List<Point> ClosestPoints { get; set; }

        private static int id = 1;

        public Point(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
            ClosestPoints = new List<Point>();
        }

        public override string ToString()
        {
            return $"(X: {X}, Y: {Y})";
        }

        public static Point Create(int x, int y)
        {
            return new Point(id++, x, y);
        }

        public int DistanceFrom(Point other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }
    }

    class Program
    {
        static IEnumerable<Point> ReadInput()
        {
            List<Point> points = new List<Point>();
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                string[] parts = line.Split(",");
                points.Add(Point.Create(int.Parse(parts[0].Trim()), int.Parse(parts[1].Trim())));
            }
            return points;
        }

        static int SolveFirst(IEnumerable<Point> points)
        {            
            int maxX = points.Select(p => p.X).Max();
            int maxY = points.Select(p => p.Y).Max();
            int minX = points.Select(p => p.X).Min();
            int minY = points.Select(p => p.Y).Min();

            var pointsById = points.ToDictionary(p => p.Id);

            int[,] grid = new int[maxX + 1, maxY + 1];

            foreach (var p in points)
            {
                grid[p.X, p.Y] = p.Id;
            }
                        
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if (grid[i, j] != 0)
                    {
                        pointsById[grid[i, j]].ClosestPoints.Add(pointsById[grid[i, j]]);
                        continue;
                    }

                    Dictionary<int, List<Point>> pointsByDistance = points                        
                        .GroupBy(p => p.DistanceFrom(Point.Create(i, j)))
                        .ToDictionary(g => g.Key, g => g.ToList());                    

                    List<Point> pointsWithMinimumDistance = pointsByDistance[pointsByDistance.Keys.Min()];

                    if (pointsWithMinimumDistance.Count == 1)
                    {
                        var p = pointsWithMinimumDistance[0];
                        grid[i, j] = p.Id;
                        p.ClosestPoints.Add(Point.Create(i, j));
                    }
                }
            }

            var pointWithFiniteAreas = points.Where(p => !p.ClosestPoints.Any(cp => cp.X == minX || cp.X == maxX || cp.Y == maxY || cp.Y == minY));
            return pointWithFiniteAreas.Max(p => p.ClosestPoints.Count);
        }

        static int SolveSecond(IEnumerable<Point> points)
        {
            const int maxDistance = 10000;
            int regionCoordinatesCount = 0;

            int m = Math.Max(points.Select(p => p.X).Max(), points.Select(p => p.Y).Max());
            int maxX = m;
            int maxY = m;

            int[,] grid = new int[maxX + 1, maxY + 1];

            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    if (points.Sum(p => p.DistanceFrom(Point.Create(i, j))) < maxDistance)
                    {
                        regionCoordinatesCount++;
                    }
                }
            }

            return regionCoordinatesCount;
        }

        static void Main(string[] args)
        {
            IEnumerable<Point> points = ReadInput();

            int maxFiniteAreaSize = SolveFirst(points);
            Console.WriteLine(maxFiniteAreaSize);

            int regionSize = SolveSecond(points);
            Console.WriteLine(regionSize);
        }
    }
}
