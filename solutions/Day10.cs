using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class Day10 : ProjectDay
    {
        public override void Run()
        {
            var inputLines = AOCUtility.FileLines(@"resources/day10/input.txt");
            var sum = 0;
            var sums = new List<long>();
            foreach (var line in inputLines)
            {
                var bracketStack = new Stack<int>();
                var incomplete = true;
                foreach (var bracket in line.Select(TranslateBracket))
                {
                    if (bracket < 0)
                    {
                        bracketStack.Push(bracket);
                    }
                    else
                    {
                        if (bracketStack.Pop() * -1 == bracket) continue;
                        sum += TranslateIntoScore(bracket);
                        incomplete = false;
                        break;
                    }
                }

                if (!incomplete) continue;
                long sum2 = 0;
                while (bracketStack.Count > 0)
                {
                    sum2 = sum2 * 5 + bracketStack.Pop() * -1;
                }
                sums.Add(sum2);
            }

            Log($"{sum}", Main.LogLevel.Result1);
            Log($"{sums.OrderBy(i => i).ToList()[sums.Count/2]}", Main.LogLevel.Result2);
        }

        private static int TranslateBracket(char c)
        {
            return c switch
            {
                '(' => -1,
                '[' => -2,
                '{' => -3,
                '<' => -4,
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => throw new Exception("yo Mr. White, thats no bracket bitch!")
            };
        }

        private static int TranslateIntoScore(int i)
        {
            return i switch
            {
                1 => 3,
                2 => 57,
                3 => 1197,
                4 => 25137,
                _ => throw new Exception("yo Mr. White, thats no reasonable score bitch!")
            };
        }
    }
}