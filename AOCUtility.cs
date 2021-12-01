using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2021
{
    public static class AOCUtility
    {
        public static List<String> FileLines(string filename)
        {
            try
            {
                return System.IO.File.ReadAllLines(filename).ToList();
            }
            catch
            {
                throw new FileNotFoundException($"File not found: {filename}");
            }
            
        }
    }
}