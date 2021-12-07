using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class Day7 : ProjectDay
    {
        public override void Run()
        {
            var xCoords = AOCUtility.FileLines(@"resources/day7/input.txt")[0].Split(',').Select(int.Parse).ToList();
            
            var result = Enumerable.Range(xCoords.Min(), xCoords.Max() - xCoords.Min()).Select(n => CalculateFuel(n, xCoords)).Min();
            Log($"{result}", Main.LogLevel.Result1);
            result = Enumerable.Range(xCoords.Min(), xCoords.Max() - xCoords.Min()).Select(n => CalculateFuel2(n, xCoords)).Min();
            Log($"{result}", Main.LogLevel.Result2);
        }

        private static long CalculateFuel(int coordToCheck, List<int> xCoords)
        {
            return xCoords.Select(e => Math.Abs(coordToCheck - e)).Sum();
        }
        
        private static long CalculateFuel2(int coordToCheck, List<int> xCoords)
        {
            return xCoords.Select(e =>
            {
                var n = Math.Abs(coordToCheck - e);
                return (n * (n + 1)) / 2;
            }).Sum();
        }
    }
}