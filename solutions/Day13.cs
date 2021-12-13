using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.solutions
{
    public class Day13 : ProjectDay
    {
        public override void Run()
        {
            var lines = AOCUtility.FileLines(@"resources/day13/input.txt");
            var pointRegex = new Regex(@"^(\d*),(\d*)");
            var foldRegex = new Regex(@"^fold along ([xy])=(\d*)");

            var points = new HashSet<(int x, int y)>();
            var instructions = new List<(string axis, int number)>();
            foreach (var line in lines)
            {
                var match = pointRegex.Match(line);
                if (match.Success)
                {
                    points.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
                    continue;
                }

                match = foldRegex.Match(line);
                if (match.Success)
                {
                    instructions.Add((match.Groups[1].Value, int.Parse(match.Groups[2].Value)));
                }
            }

            var curPoints = points;
            foreach (var instruction in instructions)
            {
                var newPoints = new HashSet<(int x, int y)>();
                foreach (var point in curPoints)
                {
                    newPoints.Add(FoldPoint(point, instruction));
                }

                if (instruction.Equals(instructions[0]))
                {
                    Log($"{newPoints.Count}", Main.LogLevel.Result1);
                }

                curPoints = newPoints;
            }

            Log("Result 2:");
            PrintPoints(curPoints);
        }

        private void PrintPoints(HashSet<(int x, int y)> curPoints)
        {
            var maxX = curPoints.Max(tuple => tuple.x);
            var maxY = curPoints.Max(tuple => tuple.y);

            for (int y = 0; y <= maxY; y++)
            {
                string line = "";
                for (int x = 0; x <= maxX; x++)
                {
                    line += curPoints.Contains((x, y)) ? "#" : " ";
                }
                Log(line);
            }
        }

        private static (int x, int y) FoldPoint((int x, int y) point, (string axis, int number) instruction)
        {
            switch (instruction.axis)
            {
                case "x":
                    if (point.x <= instruction.number)
                    {
                        return point;
                    }
                    else
                    {
                        int newX = point.x - (point.x - instruction.number) * 2;
                        return (newX, point.y);
                    }
                case "y":
                    if (point.y <= instruction.number)
                    {
                        return point;
                    }
                    else
                    {
                        int newY = point.y - (point.y - instruction.number) * 2;
                        return (point.x, newY);
                    }
            }

            return (-1, -1);
        }
    }
}