using System;

namespace AdventOfCode2021
{
    public class Day1 : ProjectDay
    {
        public override void Run()
        {
            foreach (var test in AOCUtility.FileLines(@"resources/day1/input.txt"))
            {
                Log(test);
            }
        }
    }
}