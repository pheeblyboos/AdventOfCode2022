namespace AdventOfCode
{
    internal class Day3
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay3");
            var list = lines.Split("\n")
                        .Select(x => new string[] { new string(x.Take(x.Length / 2).Distinct().ToArray()), new string(x.Skip(x.Length / 2).Distinct().ToArray()) })
                        .ToList();
            var charList = new List<char>();
            var answer1 = 0;

            list.ForEach((set) =>
            {
                charList = set[0].Intersect(set[1]).ToList();
                charList.ForEach((item) =>
                {
                    //use char code offset to calculate the correct items
                   answer1 += item > 96 ? item - 96 : item - 38; 
                });
            });



            var groupsOfThree = lines.Split("\r")
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(x => x.Select(v => v.Value.Trim().Distinct()).ToList())
                .ToList();

            var answer2 = 0;
            groupsOfThree.ForEach((set) =>
            {
                var substr = set[0].Intersect(set[1]).Intersect(set[2]).ToList();
                answer2 += substr[0] > 96 ? substr[0] - 96 : substr[0] - 38;

            });



            Console.WriteLine($"Day 3: {answer1} {answer2}");
        }
    
    }
}
