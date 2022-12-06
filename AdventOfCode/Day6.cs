namespace AdventOfCode
{
    internal class Day6
    {
        public static void Run()
        {
            {
                var lines = FileLoaderService.LoadFile("puzzleInputDay6");

                GetPacketMarker(lines, 4);
                GetPacketMarker(lines, 14);


            }
        }

        private static void GetPacketMarker(string lines, int interval)
        {
            var chunkStart = 0;
            while (chunkStart <= lines.Length - interval)
            {
                var noDupes = lines
                     .Skip(chunkStart)
                     .Take(interval)
                     .GroupBy(x => x)
                     .Where(g => g.Count() == 1).ToList();
                if (noDupes.Count == interval)
                {
                    Console.WriteLine($"Answer is {chunkStart + interval}");
                    break;
                }

                chunkStart++;

            }
        }
    }
}
