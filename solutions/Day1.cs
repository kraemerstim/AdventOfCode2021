using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class Day1 : ProjectDay
    {
        public override void Run()
        {
            List<int> depthList = AOCUtility.FileLines(@"resources/day1/input.txt").Select(int.Parse).ToList();

            int depthCounter1 = Enumerable.Range(0, depthList.Count - 1)
                .Count(x => (depthList[x] < depthList[x + 1]));
            int depthCounter2 = Enumerable.Range(0, depthList.Count - 3)
                .Count(x => (depthList[x] < depthList[x + 3]));

            Log(Convert.ToString(depthCounter1), Main.LogLevel.Result1);
            Log(Convert.ToString(depthCounter2), Main.LogLevel.Result2);
        }
    }
}