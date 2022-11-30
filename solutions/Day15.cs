using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021.solutions
{
    public class Day15 : ProjectDay
    {
        public class RiskArea
        {
            public int AreaRisk { get; }
            public int CalculatedRisk { get; set; }

            public List<RiskArea> SurroundingAreas { get; }

            public int RandomSeed { get; }
            public RiskArea Predecessor { get; set; }

            public (int x, int y) Coordinates { get; set; }
            public RiskArea(int x, int y, int areaRisk)
            {
                AreaRisk = areaRisk;
                SurroundingAreas = new List<RiskArea>();
                CalculatedRisk = 0;
                var randomizer = new Random(Guid.NewGuid().GetHashCode());
                RandomSeed = randomizer.Next();
                Coordinates = (x, y);
            }

            public void AddSurroundingArea(RiskArea area)
            {
                if (SurroundingAreas.Contains(area)) return;
                SurroundingAreas.Add(area);
                area.AddSurroundingArea(this);
            }
        }

        internal class RiskAreaComparer : IComparer<RiskArea>
        {
            public int Compare(RiskArea x, RiskArea y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                var CalculatedRiskComparison = x.CalculatedRisk.CompareTo(y.CalculatedRisk);
                if (CalculatedRiskComparison != 0) return CalculatedRiskComparison;
                return x.RandomSeed.CompareTo(y.RandomSeed);
            }
        }

        private RiskArea Calculate(int repeats)
        {
            var inputLines = AOCUtility.FileLines(@"resources/day15/input.txt");
            var riskMap = new List<List<RiskArea>>();

            for (int k = 0; k < repeats; k++)
            {
                for (var i = 0; i < inputLines.Count; i++)
                {
                    var resultingIndexY = k * inputLines.Count + i;
                    var curList = new List<RiskArea>();
                    riskMap.Add(curList);
                    var inputLine = inputLines[i];
                    for (int l = 0; l < repeats; l++)
                    {
                        for (int j = 0; j < inputLine.Length; j++)
                        {
                            var resultingIndexX = l * inputLine.Length + j;

                            var newRiskArea =
                                new RiskArea(resultingIndexX, resultingIndexY, ((inputLine[j] - '0' + k + l-1) % 9) +1);

                            curList.Add(newRiskArea);
                            if (resultingIndexY > 0)
                            {
                                newRiskArea.AddSurroundingArea(riskMap[resultingIndexY - 1][resultingIndexX]);
                            }

                            if (resultingIndexX > 0)
                            {
                                newRiskArea.AddSurroundingArea(riskMap[resultingIndexY][resultingIndexX - 1]);
                            }
                        }
                    }
                }
            }


            var stopwatchContainsHashset = new Stopwatch();
            var stopwatchContainsInSortedSet = new Stopwatch();
            var stopwatchAll = new Stopwatch();
            
            var startArea = riskMap[0][0];
            startArea.CalculatedRisk = 0;
            var endArea = riskMap[^1][^1];

            var doneSet = new HashSet<RiskArea>(){startArea};
            //var doneSet = new List<RiskArea>(){startArea};
            var checkSet = new SortedSet<RiskArea>(new RiskAreaComparer());
            
            foreach (var area in startArea.SurroundingAreas)
            {
                area.CalculatedRisk = area.AreaRisk;
                area.Predecessor = startArea;
                checkSet.Add(area);
            }

            while (!doneSet.Contains(endArea))
            {
                var curArea = checkSet.Min;
                Debug.Assert(curArea != null, nameof(curArea) + " != null");
                curArea.CalculatedRisk = curArea.Predecessor.CalculatedRisk + curArea.AreaRisk;
                foreach (var area in curArea.SurroundingAreas)
                {
                    stopwatchContainsHashset.Start();
                    var contains = doneSet.Contains(area);
                    stopwatchContainsHashset.Stop();
                    if (contains) continue;
                    var risk = curArea.CalculatedRisk + area.AreaRisk;
                    stopwatchContainsInSortedSet.Start();
                    contains = checkSet.Contains(area);
                    stopwatchContainsInSortedSet.Stop();
                    if (!contains || area.CalculatedRisk > risk)
                    {
                        area.CalculatedRisk = risk;
                        area.Predecessor = curArea;
                        checkSet.Add(area);
                    }
                }

                doneSet.Add(curArea);
                if (doneSet.Count % 1000 == 0)
                {
                    stopwatchAll.Stop();
                    Log($"progress: {doneSet.Count} -- ContainsStopwatch = {stopwatchContainsHashset.Elapsed}, stopwatch2 = {stopwatchContainsInSortedSet.Elapsed}... Out of {stopwatchAll.Elapsed}");
                    stopwatchContainsHashset.Reset();
                    stopwatchContainsInSortedSet.Reset();
                    stopwatchAll.Restart();
                }
                checkSet.Remove(curArea);
            }

            
            return endArea;
        }
        public override void Run()
        {
            Task.Run(() =>
            {
                var stopper = Stopwatch.StartNew();
                var result1 = Calculate(1);
                Log($"{result1.CalculatedRisk}", Main.LogLevel.Result1);

                Log($"stopped at: {stopper.Elapsed}");
                stopper.Restart();

                var resul2 = Calculate(5);
                Log($"{resul2.CalculatedRisk}", Main.LogLevel.Result2);
                Log($"stopped at: {stopper.Elapsed}");
            });
        }
    }
}