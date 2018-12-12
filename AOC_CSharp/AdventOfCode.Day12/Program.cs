using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day12
{
    class Program
    {
        const string INITIAL_STATE = "...#..#.#..##......###...###.....................";
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
                foreach (var rule in rules)
                {
                    Regex ruleMatcher = new Regex(rule.currentState);
                    finalState = ruleMatcher.Replace(finalState, rule.nextState);
                }
                Console.WriteLine(finalState);
            }            
        }
    }
}
