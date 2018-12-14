using System;
using System.Collections.Generic;

namespace AdventOfCode.Day14
{
    class Program
    {
        static void PrintScoreboard(List<int> scoreboard, int firstElfReceipe, int secondElfReceipe)
        {
            for (int i = 0; i < scoreboard.Count; i++)
            {
                if (i == firstElfReceipe)
                {
                    Console.Write($"({scoreboard[i]}) ");
                }
                else if (i == secondElfReceipe)
                {
                    Console.Write($"[{scoreboard[i]}] ");
                }
                else
                {
                    Console.Write($"{scoreboard[i]} ");
                }
            }
            Console.WriteLine();
        }

        static IEnumerable<int> SolveFirst()
        {
            const int receipesToMake = 286051;
            const int receipesToRead = 10;

            int firstElfReceipe = 0;
            int secondElfReceipe = 1;

            List<int> scoreboard = new List<int>(receipesToMake + receipesToRead);
            scoreboard.Add(3);
            scoreboard.Add(7);

            //PrintScoreboard(scoreboard, firstElfReceipe, secondElfReceipe);

            while (scoreboard.Count < receipesToMake + 10)
            {
                int sum = scoreboard[firstElfReceipe] + scoreboard[secondElfReceipe];
                if (sum >= 10)
                {
                    scoreboard.Add(1);
                    scoreboard.Add(sum % 10);
                }
                else
                {
                    scoreboard.Add(sum);
                }

                firstElfReceipe = (firstElfReceipe + 1 + scoreboard[firstElfReceipe]) % scoreboard.Count;
                secondElfReceipe = (1 + secondElfReceipe + scoreboard[secondElfReceipe]) % scoreboard.Count;

                //PrintScoreboard(scoreboard, firstElfReceipe, secondElfReceipe);
            }

            for (int i = receipesToMake; i < receipesToMake + receipesToRead; i++)
            {
                yield return scoreboard[i];
            }
        }

        static int CheckPattern(int score, int patternIndex, int[] patternToFind)
        {
            int newIndex = CheckScore(score, patternIndex, patternToFind);
            if (patternIndex > 0 && newIndex == 0)
            {
                return CheckScore(score, 0, patternToFind);
            }
            else
            {
                return newIndex;
            }
        }

        static int CheckScore(int score, int patternIndex, int[] patternToFind)
        {
            if (score == patternToFind[patternIndex])
            {
                return ++patternIndex;
            }
            else
            {
                return 0;
            }
        }

        static int SolveSecond()
        {
            int[] patternToFind = new int[] { 2, 8, 6, 0, 5, 1 };
            int patternIndex = 0;

            int firstElfReceipe = 0;
            int secondElfReceipe = 1;

            List<int> scoreboard = new List<int>();
            scoreboard.Add(3);
            scoreboard.Add(7);

            while (patternIndex != patternToFind.Length)
            {
                int score = scoreboard[firstElfReceipe] + scoreboard[secondElfReceipe];
                if (score >= 10)
                {
                    int firstDigit = 1;
                    scoreboard.Add(firstDigit);
                    patternIndex = CheckPattern(firstDigit, patternIndex, patternToFind);
                    if (patternIndex == patternToFind.Length)
                    {
                        break;
                    }

                    int secondDigit = score % 10;
                    scoreboard.Add(secondDigit);
                    patternIndex = CheckPattern(secondDigit, patternIndex, patternToFind);
                }
                else
                {
                    scoreboard.Add(score);
                    patternIndex = CheckPattern(score, patternIndex, patternToFind);
                }

                firstElfReceipe = (firstElfReceipe + 1 + scoreboard[firstElfReceipe]) % scoreboard.Count;
                secondElfReceipe = (1 + secondElfReceipe + scoreboard[secondElfReceipe]) % scoreboard.Count;
            }

            return scoreboard.Count - patternToFind.Length;
        }

        static void Main(string[] args)
        {
            var firsResult = SolveFirst();
            Console.WriteLine(string.Join("", firsResult));

            var secondResult = SolveSecond();
            Console.WriteLine(secondResult);
        }
    }
}
