namespace AdventOfCode
{
    internal class Day4
    {
        public static void Run()
        {
            var file = FileLoaderService.LoadFile("puzzleInputDay4");
            var list = file.Split("\n")
                           .Select(x => x.Trim().Split(",").Select(y =>y.Split("-").ToList()))
                           .Select(x => x.Select(y => Enumerable.Range(int.Parse(y[0]), (int.Parse(y[1]) - int.Parse(y[0])) + 1).ToList()).ToList())
                           .ToList();

            var answer1 = list.Where(x => x[0].All(b => x[1].Contains(b) || x[1].All(c => x[0].Contains(c)))).ToList().Count;
            var answer2 = list.Where(x => x[0].Any(b => x[1].Contains(b) || x[1].Any(c => x[0].Contains(c)))).ToList().Count;

        }
    }
}
