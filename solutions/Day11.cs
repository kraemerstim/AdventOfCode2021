using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class Octopus
    {
        public Octopus()
        {
            SurroundingOctopuses = new List<Octopus>();
        }

        public int EnergyLevel { get; set; }
        private bool Flashed { get; set; }
        public List<Octopus> SurroundingOctopuses { get; }

        public void AddSurroundingOctopus(Octopus octo)
        {
            if (SurroundingOctopuses.Contains(octo)) return;
            SurroundingOctopuses.Add(octo);
            octo.AddSurroundingOctopus(this);
        }

        public void IncreaseEnergyLevel()
        {
            EnergyLevel++;
            if (EnergyLevel > 9 && !Flashed)
            {
                Flashed = true;
                foreach (var octo in SurroundingOctopuses)
                {
                    octo.IncreaseEnergyLevel();
                }
            }
        }

        public int ResetFlash()
        {
            if (!Flashed) return 0;
            EnergyLevel = 0;
            Flashed = false;
            return 1;
        }
    }
    
    public class Day11 : ProjectDay
    {
        public override void Run()
        {
            var inputLines = AOCUtility.FileLines(@"resources/day11/input.txt");
            var allOctopuses = new List<Octopus>();
            var octoMap = new List<List<Octopus>>();
            for (var i = 0; i < inputLines.Count; i++)
            {
                var octoLine = new List<Octopus>();
                octoMap.Add(octoLine);
                var inputLine = inputLines[i];
                for (int j = 0; j < inputLine.Length; j++)
                {
                    var curOctopus = new Octopus();
                    octoLine.Add(curOctopus);
                    allOctopuses.Add(curOctopus);
                    
                    curOctopus.EnergyLevel = inputLine[j] - '0';
                    if (i > 0)
                    {
                        curOctopus.AddSurroundingOctopus(octoMap[i - 1][j]);
                        if (j > 0)
                        {
                            curOctopus.AddSurroundingOctopus(octoMap[i-1][j-1]);
                        }
                        if (j < inputLine.Length - 1)
                        {
                            curOctopus.AddSurroundingOctopus(octoMap[i-1][j+1]);
                        }
                    }

                    if (j > 0)
                    {
                        curOctopus.AddSurroundingOctopus(octoMap[i][j-1]);
                    }
                }

            }

            int sumFlash = 0;
            PrintOctoMap(octoMap);
            for (var i = 1; ; i++)
            {
                Log($"");
                var flashes = DoStep(allOctopuses);
                sumFlash += flashes;
                PrintOctoMap(octoMap);
                Log($"{i}: {sumFlash}");

                if (i == 100)
                {
                    Log($"{sumFlash}", Main.LogLevel.Result1);
                }

                if (flashes == allOctopuses.Count)
                {
                    Log($"{i}", Main.LogLevel.Result2);
                    break;
                }
            }
        }

        private void PrintOctoMap(List<List<Octopus>> octoMap)
        {
            foreach (var octoList in octoMap)
            {
                Log( string.Join("", octoList.Select(octopus => octopus.EnergyLevel.ToString())));
            }
        }
        private int DoStep(List<Octopus> octoList)
        {
            foreach (var octo in octoList)
            {
                octo.IncreaseEnergyLevel();
            }

            return octoList.Sum(octopus => octopus.ResetFlash());
        }
    }
}