using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day12
{
    class Program
    {
        const string INITIAL_STATE = "...#..#.#..##......###...###...........";
        static readonly IEnumerable<(string currentState, string nextState)> rules = new (string currentState, string nextState)[]
        {
            (currentState: @"\.\.\.##", nextState: "..#.."),
            (currentState: @"\.\.#\.\.", nextState: "..#.."),
            (currentState: @"\.#\.\.\.", nextState: "..#.."),
            (currentState: @"\.#\.#\.", nextState: "..#.."),
            (currentState: @"\.#\.##", nextState: "..#.."),
            (currentState: @"\.##\.\.", nextState: "..#.."),
            (currentState: @"\.####", nextState: "..#.."),
            (currentState: @"#\.#\.#", nextState: "..#.."),
            (currentState: @"#\.###", nextState: "..#.."),
            (currentState: @"##\.#\.", nextState: "..#.."),
            (currentState: @"##\.##", nextState: "..#.."),
            (currentState: @"###\.\.", nextState: "..#.."),
            (currentState: @"###\.#", nextState: "..#.."),
            (currentState: @"####\.", nextState: "..#..")
        };

        static void Main(string[] args)
        {
            string finalState = INITIAL_STATE;
            Console.WriteLine(finalState);

            for (int i = 0; i < 20; i++)
            {
                List<int> plantIndices = new List<int>();
                foreach (var rule in rules)
                {
                    Regex ruleMatcher = new Regex(rule.currentState);
                    foreach (Match match in ruleMatcher.Matches(finalState))
                    {
                        if (match.Success)
                        {
                            plantIndices.Add(match.Index + 2);
                        }
                    }
                }
                char[] stateArray = new string('.', finalState.Length).ToCharArray();
                for (int p = 0; p < plantIndices.Count; p++)
                {
                    stateArray[plantIndices[p]] = '#';
                }
                finalState = new string(stateArray);
                Console.WriteLine(finalState);
            }
        }
    }
}
