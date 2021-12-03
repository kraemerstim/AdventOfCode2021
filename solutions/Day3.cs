using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class Day3 : ProjectDay
    {
        public override void Run()
        {
            var binaryList = AOCUtility.FileLines(@"resources/day3/input.txt");
            
            //Part 1
            var result1 = "";
            var result2 = "";

            for (var i = 0; i < binaryList[0].Length; i++)
            {
                var oneCount = binaryList.Count(s => s[i] == '1');
                var zeroCount = binaryList.Count(s => s[i] == '0');
                result1 += oneCount > zeroCount ? "1" : "0";
                result2 += oneCount > zeroCount ? "0" : "1";
            }
            Log(Convert.ToString(Convert.ToInt32(result1, 2) * Convert.ToInt32(result2, 2)), Main.LogLevel.Result1);
            
            //Part 2
            result1 = CollapseToSingleString(binaryList, 0, (one, zero) => one >= zero);
            result2 = CollapseToSingleString(binaryList, 0, (one, zero) => one < zero);
            Log(Convert.ToString(Convert.ToInt32(result1, 2) * Convert.ToInt32(result2, 2)), Main.LogLevel.Result2);
        }

        private string CollapseToSingleString(List<string> strings, int depth, Func<int, int, bool> useOneFunc)
        {
            if (strings.Count == 1)
            {
                return strings[0];
            }
            
            var oneList = strings.FindAll(s => s[depth] == '1');
            var zeroList = strings.FindAll(s => s[depth] == '0');
            return CollapseToSingleString(useOneFunc(oneList.Count, zeroList.Count) ? oneList : zeroList, depth + 1,
                useOneFunc);
        }
    }
}