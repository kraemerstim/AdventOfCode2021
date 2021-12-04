using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class BingoNumber
    {
        public int Number { get; set; }

        public bool Selected { get; set; }

        public BingoNumber(string s)
        {
            Number = Convert.ToInt32(s);
            Selected = false;
        }
    }

    public class BingoSheet
    {
        private List<List<BingoNumber>> numbers;

        public BingoSheet()
        {
            numbers = new List<List<BingoNumber>>();
        }

        public void AddRow(List<BingoNumber> newRow)
        {
            numbers.Add(newRow);
        }

        public void SelectNumber(int number)
        {
            foreach (var row in numbers)
            {
                BingoNumber foundNumber = row.FirstOrDefault(b => b.Number == number);
                if (foundNumber != null)
                    foundNumber.Selected = true;
            }
        }

        public bool HasWon()
        {
            if (numbers.Any(row => row.All(b => b.Selected)))
            {
                return true;
            }

            for (int i = 0; i < 5; i++)
            {
                if (numbers.All(row => row[i].Selected))
                {
                    return true;
                }
            }
            
            return false;
        }

        public int GetSumOfNonSelectedNumbers()
        {
            return numbers.Select(row => row.Where(b => !b.Selected).Sum(b => b.Number)).Sum();
        }
    }

    public class Day4 : ProjectDay
    {
        public override void Run()
        {
            List<BingoSheet> bingoSheets = new List<BingoSheet>();
            var bingoList = AOCUtility.FileLines(@"resources/day4/input.txt");
            var numberList = bingoList[0].Split(',').Select(int.Parse).ToList();
            bingoList.RemoveAt(0);

            BingoSheet tempBingoSheet = null;
            foreach (var numbers in bingoList)
            {
                if (numbers == "")
                {
                    if (tempBingoSheet != null)
                    {
                        bingoSheets.Add(tempBingoSheet);
                    }

                    tempBingoSheet = new BingoSheet();
                }
                else
                {
                    tempBingoSheet.AddRow(numbers.Split(' ').Where(s => s.Trim()!="").Select(s => new BingoNumber(s)).ToList());
                }
            }

            BingoSheet winningSheet = null;
            BingoSheet losingSheet = null;
            foreach (var bingoNumber in numberList)
            {
                foreach (var curBingoSheet in bingoSheets)
                {
                    curBingoSheet.SelectNumber(bingoNumber);
                }

                List<BingoSheet> bingoSheetsToRemove = new List<BingoSheet>();
                foreach (var curBingoSheet in bingoSheets)
                {
                    if (curBingoSheet.HasWon())
                    {
                        if (winningSheet == null)
                        {
                            winningSheet = curBingoSheet;
                            Log("" + bingoNumber * curBingoSheet.GetSumOfNonSelectedNumbers(), Main.LogLevel.Result1);
                        }
                        
                        bingoSheetsToRemove.Add(curBingoSheet);
                    }
                }

                foreach (var sheet in bingoSheetsToRemove)
                {
                    bingoSheets.Remove(sheet);
                    if (bingoSheets.Count == 0)
                    {
                        losingSheet = sheet;
                        Log("" + bingoNumber * sheet.GetSumOfNonSelectedNumbers(), Main.LogLevel.Result2);
                    }
                }

                if (losingSheet != null) break;
            }
        }
    }
}