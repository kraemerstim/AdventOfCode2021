using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;

namespace AdventOfCode2021.solutions
{
    public class Day14 : ProjectDay
    {
        public override void Run()
        {
            var lines = AOCUtility.FileLines(@"resources/day14/input.txt");
            var sequenceRegex = new Regex(@"^(\w+)$");
            var ruleRegex = new Regex(@"^(\w)(\w) -> (\w)$");
            
            var rules = new List<((char first, char second) initial, char result)>();
            var sequence = "";

            foreach (var line in lines)
            {
                var match = ruleRegex.Match(line);
                if (match.Success)
                {
                    rules.Add(((match.Groups[1].Value[0], match.Groups[2].Value[0]), match.Groups[3].Value[0]));
                    continue;
                }
                
                match = sequenceRegex.Match(line);
                if (match.Success)
                {
                    sequence = match.Groups[0].Value;
                }
            }

            var sequencePartsDict = new Dictionary<(char first, char second), SequencePart>();
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                var curPart = sequencePartsDict.GetValueOrDefault((sequence[i], sequence[i + 1]));
                if (curPart == null)
                {
                    curPart = new SequencePart((sequence[i], sequence[i + 1]), 0);
                    sequencePartsDict.Add(curPart.Initial, curPart);
                }

                curPart.Count++;
            }

            var curList = sequencePartsDict.Values;
            for (var i = 1; i <= 40; i++)
            {
                var newDict = new Dictionary<(char first, char second), SequencePart>();
                foreach (var sequencePart in curList)
                {
                    var handled = false;
                    foreach (var rule in rules)
                    {
                        var resultList = sequencePart.Transform(rule);
                        foreach (var part in resultList)
                        {
                            var curPart = newDict.GetValueOrDefault(part.Initial);
                            if (curPart == null)
                            {
                                newDict.Add(part.Initial, part);
                            }
                            else
                            {
                                curPart.Count += part.Count;
                            }
                        }

                        if (resultList.Count > 0)
                        {
                            handled = true;
                            break;
                        }
                    }

                    if (!handled)
                    {
                        var curPart = newDict.GetValueOrDefault(sequencePart.Initial);
                        if (curPart == null)
                        {
                            newDict.Add(sequencePart.Initial, sequencePart);
                        }
                        else
                        {
                            curPart.Count += sequencePart.Count;
                        }
                    }
                }
                curList = newDict.Values;
                if (i == 10)
                {
                    Log($"{LogAndCalculateResult(curList, sequence)}", Main.LogLevel.Result1);
                }
            }
            Log($"{LogAndCalculateResult(curList, sequence)}", Main.LogLevel.Result2);
        }

        private long LogAndCalculateResult(
            Dictionary<(char first, char second), SequencePart>.ValueCollection valueCollection, string sequence)
        {
            var countDict = new Dictionary<char, long> {{sequence[^1], 1}};
            foreach (var part in valueCollection)
            {
                if (countDict.ContainsKey(part.Initial.first))
                {
                    countDict[part.Initial.first] += part.Count;
                }
                else
                {
                    countDict.Add(part.Initial.first, part.Count);
                }
            }

            foreach (var pair in countDict)
            {
                Log($"{pair.Key} = {pair.Value}");
            }

            return countDict.Values.Max() - countDict.Values.Min();
        }
        public class SequencePart
        {
            public (char first, char second) Initial { get; set; }
            public long Count { get; set; }

            public SequencePart((char first, char second) initial, long count)
            {
                Initial = initial;
                Count = count;
            }

            public List<SequencePart> Transform(((char first, char second) initial, char result) rule)
            {
                var returnList = new List<SequencePart>();
                if (rule.initial.Equals(Initial))
                {
                    returnList.Add(new SequencePart((Initial.first, rule.result), Count));
                    returnList.Add(new SequencePart((rule.result, Initial.second), Count));
                }

                return returnList;
            }
        }
    }
}