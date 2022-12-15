namespace AdventOfCode
{
    internal class Day12
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay12").Split("\r\n").Select((x) => x.ToCharArray().Select(y => GetCharValue(y)).ToArray()).ToArray();
            ShortestPath(lines);

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Console.Write($" {lines[i][j]} ");
                }
                Console.WriteLine("");
            }


        }

        private static int GetCharValue(char character)
        {
            return character > 96 ? character - 96 : 0;
        }

        /// <summary>
        /// Dijkstra's algorithm for finding the shortest path between two nodes
        /// </summary>
        public static void ShortestPath(int[][] input)
        {
            // I'm planning to use Dijkstra's algorithm to solve this, but I cba to write an implementation >.> 


        }

    }
}
