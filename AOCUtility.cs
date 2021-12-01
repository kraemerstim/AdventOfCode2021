using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2021
{
    public static class AOCUtility
    {
        public static IEnumerable<string> FileLines(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                yield return line;
            }
        }
    }
}