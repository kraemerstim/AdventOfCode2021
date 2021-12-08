using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.solutions
{
    public class Puzzleinput
    {
        private static readonly List<int> UniqueCounts = new List<int>() {2, 4, 3, 7};
        private readonly string[] _stringsTranslation = new string[10];
        private readonly Dictionary<string, int> _stringMap = new Dictionary<string, int>();

        private List<string> SignalPatterns { get; set; }
        private List<string> OutputValue { get; set; }

        public Puzzleinput(string s)
        {
            Regex searchTerm = new Regex(@"^(.*) \| (.*)");
            var match = searchTerm.Match(s);
            SignalPatterns = match.Groups[1].Value.Split(' ').Select(SortString).ToList();
            OutputValue = match.Groups[2].Value.Split(' ').Select(SortString).ToList();
            FillStringsTranslation();
        }

        private static string SortString(string input)
        {
            var characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        private void FillStringsTranslation()
        {
            _stringsTranslation[1] = SignalPatterns.Find(s => s.Length == 2);
            _stringsTranslation[7] = SignalPatterns.Find(s => s.Length == 3);
            _stringsTranslation[4] = SignalPatterns.Find(s => s.Length == 4);
            _stringsTranslation[8] = SignalPatterns.Find(s => s.Length == 7);
            _stringsTranslation[6] = SignalPatterns.Where(s => s.Length == 6).First(s => removeLettersFrom(_stringsTranslation[1], s).Length == 1);
            _stringsTranslation[5] = SignalPatterns.Where(s => s.Length == 5).First(s => removeLettersFrom(s, _stringsTranslation[6]).Length == 0);
            _stringsTranslation[9] = SignalPatterns.Where(s => s.Length == 6).First(s => removeLettersFrom(_stringsTranslation[4], s).Length == 0);
            _stringsTranslation[3] = SignalPatterns.Where(s => s.Length == 5).First(s => removeLettersFrom(_stringsTranslation[7], s).Length == 0);
            _stringsTranslation[2] = SignalPatterns.Where(s => s.Length == 5).First(s => removeLettersFrom(_stringsTranslation[5], s).Length == 2);
            _stringsTranslation[0] = SignalPatterns.First(s => !_stringsTranslation.Contains(s));

            for (int i = 0; i < 10; i++)
            {
                _stringMap.Add(_stringsTranslation[i], i);
            }
        }

        public int GenerateOutputNumber()
        {
            var valueFactor = 1000;
            var number = 0;
            foreach (var value in OutputValue)
            {
                number += _stringMap.GetValueOrDefault(value) * valueFactor;
                valueFactor /= 10;
            }

            return number;
        }

        private string removeLettersFrom(string fromString, string removeString)
        {
            return new string(fromString.Where(s => !removeString.Contains(s)).ToArray());
        }
        public int GetUniqueOutputPatterns()
        {
            return OutputValue.Count(s => UniqueCounts.Contains(s.Length));
        }
    }
    public class Day8 : ProjectDay
    {
        public override void Run()
        {
            var inputs = new List<Puzzleinput>();
            AOCUtility.FileLines(@"resources/day8/input.txt").ForEach(s => inputs.Add(new Puzzleinput(s)));

            var result1 = inputs.Sum(i => i.GetUniqueOutputPatterns());
            Log($"{result1}", Main.LogLevel.Result1);

            var result2 = inputs.Sum(puzzleinput => puzzleinput.GenerateOutputNumber());
            Log($"{result2}", Main.LogLevel.Result2);
        }
    }
}