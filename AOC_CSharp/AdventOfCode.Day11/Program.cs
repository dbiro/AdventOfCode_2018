using System;

namespace AdventOfCode.Day11
{
    class Program
    {
        static int CalculatePowerLevel(int x, int y, int gridSerialNumber)
        {
            int rackId = x + 10;
            int powerLevel = rackId * y;
            powerLevel += gridSerialNumber;
            powerLevel *= rackId;
            int hundredDigit = Math.Abs((powerLevel / 100) % 10);
            return hundredDigit - 5;
        }

        static int CalculateSquareTotalPowerLevel(int startX, int startY, int squareSize, int[,] grid)
        {
            int sum = 0;
            for (int i = startX; i < startX + squareSize; i++)
            {
                for (int j = startY; j < startY + squareSize; j++)
                {
                    sum += grid[i, j];
                }
            }
            return sum;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine(CalculatePowerLevel(122, 79, 57));
            //Console.WriteLine(CalculatePowerLevel(217, 196, 39));
            //Console.WriteLine(CalculatePowerLevel(101, 153, 71));

            const int gridSerialNumber = 3613;
            const int gridDimension = 300; 

            int[,] grid = new int[300, 300];

            for (int i = 0; i < gridDimension; i++)
            {
                for (int j = 0; j < gridDimension; j++)
                {
                    grid[i, j] = CalculatePowerLevel(i + 1, j + 1, gridSerialNumber);
                }
            }

            int maxTotalPowerLevel = 0;
            int foundSquareX = 0;
            int foundSquareY = 0;
            int foundSquareSize = 0;

            for (int s = 1; s <= gridDimension; s++)
            {
                for (int i = 0; i < gridDimension - s; i++)
                {
                    for (int j = 0; j < gridDimension - s; j++)
                    {
                        int currentTotalPowerLevel = CalculateSquareTotalPowerLevel(i, j, s, grid);
                        if (maxTotalPowerLevel < currentTotalPowerLevel)
                        {
                            maxTotalPowerLevel = currentTotalPowerLevel;
                            foundSquareX = i;
                            foundSquareY = j;
                            foundSquareSize = s;
                        }
                    }
                }
            }

            Console.WriteLine($"Total power: {maxTotalPowerLevel}, {foundSquareX + 1},{foundSquareY + 1},{foundSquareSize}");
        }
    }
}
