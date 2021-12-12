using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.solutions
{
    public class Cave
    {
        private readonly string _name;
        private readonly Action<string, Main.LogLevel> _logfunction;
        private readonly List<Cave> _accesableCaves;
        private readonly bool _isMajor;

        public Cave(string name, Action<string, Main.LogLevel> logfunction)
        {
            this._name = name;
            _logfunction = logfunction;
            _accesableCaves = new List<Cave>();
            _isMajor = name.ToUpper().Equals(name);
        }

        public void AddAccessableCave(Cave cave)
        {
            if (_accesableCaves.Contains(cave)) return;
            _accesableCaves.Add(cave);
        }

        public int TraverseThroughCave(List<Cave> caveList, bool allowSmallOnlyOnce)
        {
            switch (_name)
            {
                case "start" when caveList.Count > 0:
                    return 0;
                case "end":
                    _logfunction( string.Join("-", caveList.Select(cave => cave._name)) + "-end", Main.LogLevel.Normal);
                    return 1;
            }

            if (caveList.Contains(this) && !_isMajor)
            {
                if (allowSmallOnlyOnce)
                {
                    return 0;
                }

                allowSmallOnlyOnce = true;
            }

            caveList.Add(this);

            return _accesableCaves.Sum(accesableCave => accesableCave.TraverseThroughCave(new List<Cave>(caveList), allowSmallOnlyOnce));
        }
    }

    public class Day12 : ProjectDay
    {
        public override void Run()
        {
            var searchTerm = new Regex(@"^(\w*)-(\w*)");
            var caveDict = new Dictionary<string, Cave>();
            var lines = AOCUtility.FileLines(@"resources/day12/input.txt");
            foreach (var line in lines)
            {
                if (!searchTerm.IsMatch(line)) continue;
                var match = searchTerm.Match(line);
                var cave1 = caveDict.GetValueOrDefault(match.Groups[1].Value);
                if (cave1 == null)
                {
                    cave1 = new Cave(match.Groups[1].Value, Logfunction);
                    caveDict.Add(match.Groups[1].Value, cave1);
                }

                var cave2 = caveDict.GetValueOrDefault(match.Groups[2].Value);
                if (cave2 == null)
                {
                    cave2 = new Cave(match.Groups[2].Value, Logfunction);
                    caveDict.Add(match.Groups[2].Value, cave2);
                }

                cave1.AddAccessableCave(cave2);
                cave2.AddAccessableCave(cave1);
            }

            var startCave = caveDict.GetValueOrDefault("start");
            var result1 = startCave?.TraverseThroughCave(new List<Cave>(), true);
            var result2 = startCave?.TraverseThroughCave(new List<Cave>(), false);
            Log($"{result1}", Main.LogLevel.Result1);
            Log($"{result2}", Main.LogLevel.Result2);
        }
    }
}