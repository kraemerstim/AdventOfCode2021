using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.solutions
{
    public class Day2 : ProjectDay
    {
        enum Direction
        {
            Forward,
            Down,
            Up
        }
        public override void Run()
        {
            Regex searchTerm = new Regex(@"^(?<direction>\S*) (?<value>\d*)");
            List<(Direction direction, int value)> directionList = new();
            foreach (var line in AOCUtility.FileLines(@"resources/day2/input.txt"))
            {
                if (searchTerm.IsMatch(line))
                {
                    var match = searchTerm.Match(line);
                    directionList.Add((TranslateToDirection(match.Groups["direction"].Value), Convert.ToInt32(match.Groups["value"].Value)));
                }
            }

            var horizontalPosition = 0;
            var depth = 0;
            var depth2 = 0;
            var aim = 0;
            foreach (var order in directionList)
            {
                switch (order.direction)
                {
                    case Direction.Forward:
                        horizontalPosition += order.value;
                        depth2 += aim * order.value;
                        break;
                    case Direction.Down:
                        depth += order.value;
                        aim += order.value;
                        break;
                    case Direction.Up:
                        depth -= order.value;
                        aim -= order.value;
                        break;
                }
            }
            Log(Convert.ToString(horizontalPosition * depth), Main.LogLevel.Result1);
            Log(Convert.ToString(horizontalPosition * depth2), Main.LogLevel.Result2);
        }

        private static Direction TranslateToDirection(string direction)
        {
            switch (direction)
            {
                case "forward":
                    return Direction.Forward;
                case "up":
                    return Direction.Up;
                case "down":
                    return Direction.Down;
                default:
                    throw new ArgumentException($"{direction} is no direction");
            }
        }
    }
}