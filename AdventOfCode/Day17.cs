using AdventOfCode.Day17Lib;

namespace AdventOfCode
{
    internal class Day17
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay17");
            var grid = new Grid();

            for (int i = 0; i < lines.Length; i++)
            {
               
                //if (grid.CurrentRock.HasCollision())
                //{
                //    // if the rock is hitting another rock or the floor, stop moving and select the next rock
                //    //get next rock shape

                //    grid.MakeSpace();
                //    //var rock = grid.GetNextRock(i);
                    
                //}
                grid.Draw();
                grid.BlowJet(lines[i]);
                grid.Draw();
                var isAtBottom = grid.MoveDown();
                grid.Draw();
                if (isAtBottom)
                {
                    grid.NextActiveRock();
                }
                //execute direction
                // draw rock
                //move down
            }
        }
    }
}
