using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class EvolutionClass
    {
        public int CurrentClass { get; set; }
        public long CurrentSize { get; set; }

        public EvolutionClass(int currentClass)
        {
            CurrentClass = currentClass;
            CurrentSize = 0;
        }
    }

    public class Day6 : ProjectDay
    {
        public override void Run()
        {
            var evolutionClasses = new List<EvolutionClass>();
            for (var i = 0; i <= 8; i++)
            {
                evolutionClasses.Add(new EvolutionClass(i));
            }

            var start = AOCUtility.FileLines(@"resources/day6/input.txt")[0];
            foreach (var number in start.Split(',').Select(int.Parse))
            {
                evolutionClasses.First(e => e.CurrentClass == number).CurrentSize++;
            }

            for (int i = 1; i <= 256; i++)
            {
                DoEvolutionStep(evolutionClasses);
                if (i == 80)
                {
                    Log("" + evolutionClasses.Select(e => e.CurrentSize).Sum(), Main.LogLevel.Result1);
                }
            }

            Log("" + evolutionClasses.Select(e => e.CurrentSize).Sum(), Main.LogLevel.Result2);
        }

        private void DoEvolutionStep(List<EvolutionClass> evolutionClasses)
        {
            var zeroEvoClass = evolutionClasses.First(e => e.CurrentClass == 0);
            foreach (var evoClass in evolutionClasses)
            {
                evoClass.CurrentClass--;
            }

            var sixEvoClass = evolutionClasses.First(e => e.CurrentClass == 6);
            sixEvoClass.CurrentSize += zeroEvoClass.CurrentSize;
            zeroEvoClass.CurrentClass = 8;
        }
    }
}