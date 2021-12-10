using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class HeightPoint
    {
        public HeightPoint(int height, (int x, int y) coordinate)
        {
            Height = height;
            Coordinate = coordinate;
            surroundingPoints = new List<HeightPoint>();
        }

        public void AddSurroundingPoint(HeightPoint heightpoint)
        {
            if (surroundingPoints.Contains(heightpoint)) return;
            surroundingPoints.Add(heightpoint);
            heightpoint.AddSurroundingPoint(this);
        }

        public int GetRiskLevel()
        {
            foreach (var point in surroundingPoints)
            {
                if (point.Height <= Height)
                {
                    return 0;
                }
            }

            return Height + 1;
        }

        public void FillBasinPoints(List<HeightPoint> alreadyCheckedPoints)
        {
            foreach (var point in surroundingPoints)
            {
                if (point.Height < 9 && !alreadyCheckedPoints.Contains(point))
                {
                    alreadyCheckedPoints.Add(point);
                    point.FillBasinPoints(alreadyCheckedPoints);
                }
            }
        }
        
        
        private readonly List<HeightPoint> surroundingPoints;
        public int Height { get; }
        private (int x, int y) Coordinate { get; }
    }

    public class Day9 : ProjectDay
    {
        public override void Run()
        {
            var heightmap = new List<List<HeightPoint>>();
            var inputLines = AOCUtility.FileLines(@"resources/day9/input.txt");
            for (var i = 0; i < inputLines.Count; i++)
            {
                var curList = new List<HeightPoint>();
                heightmap.Add(curList);
                var inputLine = inputLines[i];
                for (int j = 0; j < inputLine.Length; j++)
                {
                    var newHeightPoint = new HeightPoint(inputLine[j] - '0', (j, i));
                    curList.Add(newHeightPoint);
                    if (i > 0)
                    {
                        newHeightPoint.AddSurroundingPoint(heightmap[i - 1][j]);
                    }

                    if (j > 0)
                    {
                        newHeightPoint.AddSurroundingPoint(heightmap[i][j - 1]);
                    }
                }
            }

            var sum = heightmap.Sum(list => list.Sum(point => point.GetRiskLevel()));
            
            Log($"{sum}", Main.LogLevel.Result1);

            var alreadyChecked = new List<HeightPoint>();
            var basinSizes = new List<int>();
            foreach (var pointsList in heightmap)
            {
                foreach (var point in pointsList)
                {
                    if (point.Height != 9 && !alreadyChecked.Contains(point))
                    {
                        var curList = new List<HeightPoint>() {point};
                        point.FillBasinPoints(curList);
                        basinSizes.Add(curList.Count);
                        alreadyChecked.AddRange(curList);
                    }
                }
            }

            var enumerable = basinSizes.OrderByDescending(i => i).Take(3);

            var result2 = 1;
            foreach (var num in enumerable)
            {
                result2 *= num;
            }
            Log($"{result2}", Main.LogLevel.Result2);
        }
    }
}