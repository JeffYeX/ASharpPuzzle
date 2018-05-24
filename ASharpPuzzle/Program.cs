using CsvHelper;
using Puzzle;
using System;
using System.IO;

namespace ASharpPuzzle
{
    class Program
    {

        static void Main(string[] args)
        {
            //Heuristic mHeuristic;
            PuzzleStrategy mStrategy = new PuzzleStrategy();
            //var t = new FileInfo(Directory.GetCurrentDirectory() + "\\length25data.csv");
            //var textWriter = t.CreateText();
            //var csv = new CsvWriter(textWriter);
            using (TextWriter writer = File.CreateText(Directory.GetCurrentDirectory() + "\\length25data.csv"))
            {
                var csv = new CsvWriter(writer);
                //var worksheet = workbook.Worksheets.Add("Sheet1");
                var text = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\10.pl");

                foreach (Heuristic mHeuristic in Enum.GetValues(typeof(Heuristic)))
                {
                    foreach (var problem in text)
                    {
                        var bracketIndex = problem.IndexOf("(");
                        var problemType = problem.Substring(bracketIndex + 1, 2);
                        var spaceIndex = problem.IndexOf(" ");
                        var initialState = problem.Substring(spaceIndex + 1, 9);

                        var mInitialState = new int[9];
                        for (int i = 0; i < initialState.Length; i++)
                        {
                            //var blah = initialState.Substring(1, 2);
                            var position = initialState.Substring(i, 1);
                            if (i == 0)
                            {
                                mInitialState[Int32.Parse(position)] = -1;
                            }
                            else
                            {
                                mInitialState[Int32.Parse(position)] = i;
                            }
                            //int blah = initialState[1];
                            //var blah1 = blah - 1;

                        }
                        //Console.WriteLine(mInitialState);

                        var record = new { Algorithm = "A#", Heuristic = mHeuristic.ToString(), Problem = problemType };
                        csv.WriteRecord(record);
                        mStrategy.Solve(mInitialState, mHeuristic, csv);

                    }
                }
            }
            //var mInitialState = new int[] { 5, 4, 2, 7, 1, 3, -1, 8, 6 };
            

            //mStrategy.Solve(mInitialState, mHeuristic);


        }
    }
}
