namespace AdventOfCode
{
    internal class Day1
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay1");
            var list = lines.Split("\n\n")
                            .Select(x => x.Split("\n")
                            .Select(x => int.Parse(x)))
                            .ToList();

            var answer1 = list.Select(x => x.Sum())
                            .OrderByDescending(x => x)
                            .First();
            var answer2 = list.Select(x => x.Sum())
                            .OrderByDescending(x => x)
                            .Take(3)
                            .Sum();
            Console.WriteLine($"Day 1: {answer1} {answer2}");
        }
        
        
    }
}
