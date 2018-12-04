using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day04
{
    class Program
    {
        class GuardRecord
        {
            public GuardRecord(int guradId)
            {
                GuardId = guradId;
                FallsAsleepAt = new List<int>();
                SleepsAt = new List<int>();
                WakeUpsAt = new List<int>();
            }
            public int GuardId { get; }
            public List<int> FallsAsleepAt { get; }
            public List<int> SleepsAt { get; }
            public List<int> WakeUpsAt { get; }

            public override string ToString()
            {
                return GuardId.ToString();
            }
        }

        private static ImmutableList<GuardRecord> ReadInput()
        {
            string[] lines = File.ReadAllLines("input.txt");
            List<(DateTime timestamp, string record)> lineWithTimestamps = new List<(DateTime, string)>();

            foreach (var line in lines)
            {
                string timestampPart = line.Substring(1, 16);
                DateTime timestamp = DateTime.ParseExact(timestampPart, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                lineWithTimestamps.Add((timestamp: timestamp, record: line.Substring(19)));
            }

            var orderedRecords = lineWithTimestamps.OrderBy(l => l.timestamp).ToList();
            File.WriteAllLines("input_ordered.txt", orderedRecords.Select(l => $"{l.timestamp.ToString("[yyyy-MM-dd HH-mm]")} {l.record}"));

            GuardRecord currentGuardRecord = null;
            Dictionary<int, GuardRecord> guardRecords = new Dictionary<int, GuardRecord>();

            foreach (var line in orderedRecords)
            {
                if (line.record.StartsWith("Guard"))
                {
                    string[] parts = line.record.Split(" ");
                    int guardId = int.Parse(parts[1].Substring(1, parts[1].Length - 1));
                    if (!guardRecords.ContainsKey(guardId))
                    {
                        currentGuardRecord = new GuardRecord(guardId);
                        guardRecords[guardId] = currentGuardRecord;
                    }
                    else
                    {
                        currentGuardRecord = guardRecords[guardId];
                    }
                }
                else if (line.record.StartsWith("falls"))
                {
                    currentGuardRecord.FallsAsleepAt.Add(line.timestamp.Minute);
                }
                else if (line.record.StartsWith("wakes"))
                {
                    int lastAsleepAt = currentGuardRecord.FallsAsleepAt[currentGuardRecord.FallsAsleepAt.Count - 1];
                    for (int j = lastAsleepAt; j < line.timestamp.Minute; j++)
                    {
                        currentGuardRecord.SleepsAt.Add(j);
                    }
                    currentGuardRecord.WakeUpsAt.Add(line.timestamp.Minute);
                }
                else
                {
                    throw new ArgumentException($"{line.timestamp.ToString("[yyyy-MM-dd HH-mm]")} {line.record}");
                }
            }

            return ImmutableList.Create(guardRecords.Values.ToArray());
        }

        static int SolveFirst(IEnumerable<GuardRecord> records)
        {            
            var mostSleepyGuardRecord = records.OrderByDescending(r => r.SleepsAt.Sum()).First();
            var sleeps = mostSleepyGuardRecord.SleepsAt.GroupBy(a => a).ToDictionary(a => a.Key, a => a.Count());
            var mostSleepyMinute = sleeps.OrderByDescending(s => s.Value).First();

            return mostSleepyMinute.Key * mostSleepyGuardRecord.GuardId;
        }

        static int SolveSecond(IEnumerable<GuardRecord> records)
        {
            int mostSleepyAt = 0;
            int mostSleepyAtCount = 0;
            int guardId = 0;

            foreach (var record in records)
            {                
                if (record.SleepsAt.Count == 0)
                {
                    continue;
                }

                var groupedSleeps = record.SleepsAt.GroupBy(a => a).ToDictionary(a => a.Key, a => a.Count());
                KeyValuePair<int, int> mostSleepyMinute = groupedSleeps.OrderByDescending(s => s.Value).First();
                                
                if (mostSleepyAtCount < mostSleepyMinute.Value)
                {
                    mostSleepyAt = mostSleepyMinute.Key;
                    mostSleepyAtCount = mostSleepyMinute.Value;
                    guardId = record.GuardId;
                }
            }

            return mostSleepyAt * guardId;
        }

        static void Main(string[] args)
        {
            var orderedRecords = ReadInput();

            int firstResult = SolveFirst(orderedRecords);
            Console.WriteLine(firstResult);

            int secondResult = SolveSecond(orderedRecords);
            Console.WriteLine(secondResult);
        }
    }
}
