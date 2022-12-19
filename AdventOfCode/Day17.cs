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
            while(grid.RockCounter < 2023)
            {
                grid.BlowJet(lines[counter]);
                var isAtBottom = grid.MoveDown();
                

                if (isAtBottom)
                {
                    if (grid.RockCounter >= 2023) break;
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
            grid.Draw();
            Console.WriteLine("done");
        }
    }
}
