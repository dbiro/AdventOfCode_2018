using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] ids = File.ReadAllLines("input.txt");

            int checksum = ComputeChecksum(ids);
            string commonLetters = ComputeCommonLetters(ids);

            Console.WriteLine($"checksum: {checksum}");
            Console.WriteLine($"common letters: {commonLetters}");
        }

        private static string ComputeCommonLetters(string[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    int index = CompareIds(ids[i], ids[j]);
                    if (index != -1)
                    {
                        List<char> result = new List<char>(ids[i].Length - 1);
                        for (int k = 0; k < ids[i].Length; k++)
                        {
                            if (k == index)
                            {
                                continue;
                            }
                            result.Add(ids[i][k]);
                        }
                        return new string(result.ToArray());
                    }
                }
            }

            throw new ArgumentException("fuck fuck fuck");
        }
                
        private static int CompareIds(string a, string b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Fuck");
            }

            int index = -1;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    if (index != -1)
                    {
                        return -1;
                    }
                    index = i;
                }
            }

            return index;
        }

        private static int ComputeChecksum(string[] ids)
        {
            int numberOfTwos = 0;
            int numberOfThrees = 0;

            foreach (var id in ids)
            {
                var scanResult = ScanIdentity(id);
                if (scanResult.twos)
                {
                    numberOfTwos++;
                }
                if (scanResult.threes)
                {
                    numberOfThrees++;
                }
            }

            return numberOfTwos * numberOfThrees;
        }

        private static (bool twos, bool threes) ScanIdentity(string identity)
        {
            Dictionary<char, int> occurenceOfLetters = new Dictionary<char, int>();
            foreach (var c in identity)
            {
                if (!occurenceOfLetters.ContainsKey(c))
                {
                    occurenceOfLetters[c] = 1;
                }
                else
                {
                    occurenceOfLetters[c]++;
                }
            }
            return (occurenceOfLetters.Values.Any(o => o == 2), occurenceOfLetters.Values.Any(o => o == 3));
        }
    }
}
