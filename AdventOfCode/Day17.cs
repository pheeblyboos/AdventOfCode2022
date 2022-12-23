using AdventOfCode.Day17Lib;

namespace AdventOfCode
{
    internal class Day17
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay17");
            var grid = new Grid();
            var counter = 0;
            var isAtBottom = false;
            while (grid.RockCounter < 2022)
            {
                grid.BlowJet(lines[counter]);
                
                isAtBottom = grid.MoveDown();
                if (isAtBottom)
                {
                    if (grid.RockCounter >= 2022) break;
                    grid.NextActiveRock();
                }
                if (counter >= lines.Length - 1)
                {
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
            var answer1 = grid.GridField.Count(x => x.Contains('#'));
            Console.WriteLine(answer1);
            grid.Draw();
            Console.WriteLine("done");
        }
    }
}
