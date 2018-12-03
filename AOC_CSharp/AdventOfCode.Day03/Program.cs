using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace AdventOfCode.Day03
{
    class Claim
    {
        public int Id { get; }
        public int Left { get; }
        public int Top { get; }
        public int Width { get; }
        public int Height { get; }

        public Claim(int id, int left, int top, int width, int height)
        {
            Id = id;
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"(id: {Id}, left: {Left}, top: {Top}, width: {Width}, height: {Height})";
        }
    }

    class Program
    {
        private static ImmutableList<Claim> ReadInput()
        {
            string[] lines = File.ReadAllLines("input.txt");

            List<Claim> claims = new List<Claim>();

            foreach (var line in lines)
            {
                string[] lineParts = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                int id = int.Parse(lineParts[0].Substring(1));

                string[] positionParts = lineParts[2].Substring(0, lineParts[2].Length - 1).Split(',');
                int left = int.Parse(positionParts[0]);
                int top = int.Parse(positionParts[1]);

                string[] dimensionParts = lineParts[3].Split("x");
                int width = int.Parse(dimensionParts[0]);
                int height = int.Parse(dimensionParts[1]);

                claims.Add(new Claim(id, left, top, width, height));
            }

            return ImmutableList.Create(claims.ToArray());

        }

        private static void Main(string[] args)
        {
            const int fabricColumns = 1000;
            const int fabricRows = 1000;

            ImmutableList<Claim> claims = ReadInput();
            
            bool?[,] fabric = new bool?[fabricRows, fabricColumns];

            foreach (var claim in claims)
            {
                int row = claim.Top;
                int column = claim.Left;

                for (int i = row; i < row + claim.Height; i++)
                {
                    for (int j = column; j < column + claim.Width; j++)
                    {
                        if (!fabric[i, j].HasValue)
                        {
                            fabric[i, j] = false;
                        }
                        else
                        {
                            fabric[i, j] = true;
                        }
                    }
                }
            }

            int intersectcount = 0;
            for (int i = 0; i < fabricRows; i++)
            {
                for (int j = 0; j < fabricColumns; j++)
                {
                    if (fabric[i, j] == true)
                    {
                        intersectcount++;
                    }
                }
            }

            Console.WriteLine(intersectcount);

            int intactedClaimId = -1;
            bool isClaimIntersected = false;

            foreach (var claim in claims)
            {
                isClaimIntersected = false;

                int row = fabricRows - claim.Top - claim.Height;
                int column = claim.Left;

                for (int i = row; i < row + claim.Height; i++)
                {
                    for (int j = column; j < column + claim.Width; j++)
                    {
                        if (fabric[i, j] == true)
                        {
                            isClaimIntersected = true;
                            break;
                        }
                    }

                    if (isClaimIntersected)
                    {
                        break;
                    }
                }

                if (!isClaimIntersected)
                {
                    intactedClaimId = claim.Id;
                    break;
                }
            }

            Console.WriteLine(intactedClaimId);
        }
    }
}
