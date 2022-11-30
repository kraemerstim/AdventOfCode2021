using System;
using System.Collections.Generic;

namespace AdventOfCode2021.solutions
{
    public class Day17 : ProjectDay
    {
        //target area: x=14..50, y=-267..-225
        private (int min, int max) xRange = (14,50);
        private (int min, int max) yRange = (-267, -225);
        public override void Run()
        {
            var xValues = new List<int>();
            var yValues = new List<int>();

            
            for (var i = 1; i <= xRange.max; i++)
            {
                if (IsXValuePossible(i))
                {
                    xValues.Add(i);
                }
            }
            
            for (var i = yRange.min; i <= -yRange.min; i++)
            {
                if (IsYValuePossible(i))
                {
                    yValues.Add(i);
                }
            }

            var possibleValues = new List<(int x, int y)>();
            int maxY = 0;
            foreach (var xValue in xValues)
            {
                foreach (var yValue in yValues)
                {
                    if (IsXYValuePossible(xValue, yValue))
                    {
                        maxY = Math.Max(maxY, yValue);
                        possibleValues.Add((xValue, yValue));
                    }
                }
            }
            Log($"{GetMaxHeightWithY(maxY)}", Main.LogLevel.Result1);
            Log($"{possibleValues.Count}", Main.LogLevel.Result2);
        }

        private long GetMaxHeightWithY(int y)
        {
            long result = 0;
            for (int i = y; i > 0; i--)
            {
                result += i;
            }

            return result;
        }
        private bool IsXYValuePossible(int xValue, int yValue)
        {
            var tempX = xValue;
            var tempY = yValue;
            while (tempX <= xRange.max  && tempY >= yRange.min)
            {
                if (tempX >= xRange.min && tempX <= xRange.max && tempY >= yRange.min && tempY <= yRange.max) return true;
                xValue = Math.Max(0, --xValue);
                yValue--;
                tempX += xValue;
                tempY += yValue;
                
            }
            return false;
        }

        public bool IsXValuePossible(int x)
        {
            var int1 = -1;
            var int2 = 0;

            while (int1 != int2 && int2 < xRange.min)
            {
                int1 = int2;
                int2 += x;
                x--;
                if (int2 >= xRange.min && int2 <= xRange.max) return true;
            }

            return false;
        }
        
        public bool IsYValuePossible(int y)
        {
            var int1 = 0;
            var int2 = 0;

            while (int2 > yRange.min)
            {
                int1 = int2;
                int2 += y;
                y--;
                if (int2 >= yRange.min && int2 <= yRange.max) return true;
            }

            return false;
        }
        
    }
}