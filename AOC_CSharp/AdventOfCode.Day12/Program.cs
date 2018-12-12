using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day12
{
    class Program
    {
        const string TEST_INITIAL_STATE = "#..#.#..##......###...###";
        const string INITIAL_STATE = "#.##.###.#.##...##..#..##....#.#.#.#.##....##..#..####..###.####.##.#..#...#..######.#.....#..##...#";

        static readonly HashSet<string> TEST_RULE_SET = new HashSet<string>()
        {
            "...##",
            "..#..",
            ".#...",
            ".#.#.",
            ".#.##",
            ".##..",
            ".####",
            "#.#.#",
            "#.###",
            "##.#.",
            "##.##",
            "###..",
            "###.#",
            "####."
        };

        static readonly HashSet<string> RULE_SET = new HashSet<string>()
        {
            //".#.#.",
            "...#.",
            //"..##.",
            //"....#",
            "##.#.",
            ".##.#",
            ".####",
            "#.#.#",
            "#..#.",
            //"##..#",
            //"#####",
            //"...##",
            //".#...",
            "###..",
            //"#..##",
            //"#...#",
            ".#..#",
            //".#.##",
            "#.#..",
            //".....",
            //"####.",
            "##.##",
            "..###",
            //"#....",
            //"###.#",
            ".##..",
            "#.###",
            //"..#.#",
            ".###.",
            "##...",
            "#.##.",
            "..#.."
        };

        static void Main(string[] args)
        {
            //HashSet<string> rules = TEST_RULE_SET;
            //string initialState = TEST_INITIAL_STATE;

            HashSet<string> rules = RULE_SET;
            string initialState = INITIAL_STATE;

            int expandedLength = (int)(initialState.Length * 0.5);

            string currentState = new string('.', expandedLength) + initialState + new string('.', expandedLength);

            Console.WriteLine(currentState);

            int maxIteration = 20;
            for (int t = 0; t < maxIteration; t++)
            {
                List<int> plantIndices = new List<int>();   
                
                int start = Math.Max(0, currentState.IndexOf('#') - 4);
                int end = Math.Min(currentState.Length, currentState.LastIndexOf('#') + 4);

                for (int i = start; i < end - 2; i++)
                {
                    string pattern = new string(new char[] { currentState[i - 2], currentState[i - 1], currentState[i], currentState[i + 1], currentState[i + 2], });
                    if (rules.Contains(pattern))
                    {
                        plantIndices.Add(i);
                    }                    
                }                                

                char[] stateArray = new string('.', currentState.Length).ToCharArray();
                for (int p = 0; p < plantIndices.Count; p++)
                {
                    stateArray[plantIndices[p]] = '#';
                }
                currentState = new string(stateArray);

                Console.WriteLine(currentState);                                
            }
                        
            Console.WriteLine(currentState);

            int result = 0;
            int firstPlanIndex = currentState.IndexOf('#');
            int lastPlanIndex = currentState.LastIndexOf('#');
            for (int i = firstPlanIndex; i <= lastPlanIndex; i++)
            {
                if (currentState[i] == '#')
                {
                    result += i - expandedLength;
                }
            }

            Console.WriteLine(result);
        }
    }
}
