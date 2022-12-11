namespace AdventOfCode
{
    internal class Day8
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay8");
            var list = lines.Split("\n")
                            .Select(x => x.Trim().ToCharArray().Select(y => int.Parse(y.ToString())).ToList())
                            .ToList();

            var scoresLeft = new List<List<(int left, int right, int up, int down)>>();

            for (int i = 0; i < list.Count; i++)
            {
                var scoresHorizontalRow = new List<(int left, int right, int up, int down)>();
                for (int j = 0; j < list[i].Count; j++)
                {
                    var directions = CompareDirections(list, i, j);
                    scoresHorizontalRow.Add(directions);
                }

                scoresLeft.Add(scoresHorizontalRow);
            }

            var answer1 = scoresLeft.SelectMany(x => x).Where(x => x.left == 1 || x.right == 1 || x.up == 1 || x.down == 1).ToList().Count;

            for (int i = 0; i < scoresLeft.Count; i++)
            {
                for (int j = 0; j < scoresLeft[i].Count; j++)
                {
                    Console.Write($"({scoresLeft[i][j].left}, {scoresLeft[i][j].right}, {scoresLeft[i][j].up}, {scoresLeft[i][j].down}) ");
                }
                Console.WriteLine("");
            }
        }

        private static (int left, int right, int up, int down) CompareDirections(List<List<int>> list, int row, int current)
        {
            (int left, int right, int up , int down) directions = (0, 0, 0, 0);
            directions.left = list[row].Take(current).All(x => x < list[row][current]) ? 1 : 0;
            directions.right = list[row].Skip(current + 1).All(x => x < list[row][current]) ? 1 : 0;
            var verticalSet = list.Select(x => x[current]).ToList();
            directions.up = verticalSet.Take(row).All(x => x < list[row][current]) ? 1 : 0;
            directions.down = verticalSet.Skip(row + 1).All(x => x < list[row][current]) ? 1 : 0;

            return directions;
        }
    }
}
