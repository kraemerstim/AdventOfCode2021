using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.solutions
{
    public class Line
    {
        private (int x, int y) Begin { get; set; }
        private (int x, int y) End { get; set; }

        public Line(string line)
        {
            var searchTerm = new Regex(@"^(?'b1'\d*),(?'b2'\d*) -> (?'e1'\d*),(?'e2'\d*)");
            var match = searchTerm.Match(line);
            Begin = (Convert.ToInt32(match.Groups["b1"].Value), Convert.ToInt32(match.Groups["b2"].Value));
            End = (Convert.ToInt32(match.Groups["e1"].Value), Convert.ToInt32(match.Groups["e2"].Value));
        }

        public void DrawLineOnBoard(int[,] board, bool allowDiagonal = false)
        {
            if (!allowDiagonal && Begin.x != End.x && Begin.y != End.y)
            {
                return;
            }

            var incrementX = 0;
            var incrementY = 0;
            
            if (Begin.y != End.y)
            {
                incrementY = Begin.y < End.y ? 1 : -1;
            }
            if (Begin.x != End.x)
            {
                incrementX = Begin.x < End.x ? 1 : -1;
            }

            var currentX = Begin.x;
            var currentY = Begin.y;
            
            while (NumberIsInRange(Begin.x, End.x,currentX) && NumberIsInRange(Begin.y, End.y,currentY))
            {
                board[currentX, currentY]++;
                currentX += incrementX;
                currentY += incrementY;
            }
        }

        private static bool NumberIsInRange(int num1, int num2, int valueToCheck)
        {
            if (num1 > num2)
            {
                return num2 <= valueToCheck && valueToCheck <= num1;
            }

            return num1 <= valueToCheck && valueToCheck <= num2;

        } 
    }

    public class Day5 : ProjectDay
    {
        public override void Run()
        {
            var boardsize = 1000;
            var input = AOCUtility.FileLines(@"resources/day5/input.txt");
            var lines = input.Select(line => new Line(line)).ToList();
            var board = new int[boardsize, boardsize];
            foreach (var line in lines)
            {
                line.DrawLineOnBoard(board);
            }

            var counter = board.Cast<int>().Count(coordinate => coordinate > 1);
            Log($"{counter}", Main.LogLevel.Result1);
            
            var board2 = new int[boardsize, boardsize];
            foreach (var line in lines)
            {
                line.DrawLineOnBoard(board2, true);
            }
            counter = board2.Cast<int>().Count(coordinate => coordinate > 1);
            Log($"{counter}", Main.LogLevel.Result2);
        }
    }
}