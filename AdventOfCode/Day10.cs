namespace AdventOfCode
{
    internal class Day10
    {
        private static int _currentRow = 0;
        private static Dictionary<int, int> _resultList = new Dictionary<int, int>();
        private static List<List<char>> _crtRender = new List<List<char>>()
        {
            new List<char>(),
            new List<char>(),
            new List<char>(),
            new List<char>(),
            new List<char>(),
            new List<char>()
        };

        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay10");
            var instructions = lines.Split("\n")
                            .Select(x => x.Trim())
                            .ToList();
            var cycleCounter = 1;
            var totalScore = 1;
            instructions.ForEach((instruction) =>
            {
                var cycles = 0;
                var amount = 0;
                if (instruction.Contains("noop"))
                {
                    cycles++;
                }
                if (instruction.Contains("addx"))
                {
                    amount = int.Parse(instruction.Split(" ")[1]);
                    cycles += 2;
                }

                cycleCounter = ProcessCycles(cycleCounter, totalScore, cycles);
                totalScore += amount;
                
            });

            var answer1 = _resultList[20] + _resultList[60] + _resultList[100] + _resultList[140] + _resultList[180] + _resultList[220];

        }

        private static int ProcessCycles(int cycleCounter, int totalScore, int cycles)
        {
            int counter = cycleCounter;
            for (int i = 0; i < cycles; i++)
            {
                var isPixel = _crtRender[_currentRow].Count >= totalScore - 1 && _crtRender[_currentRow].Count <= totalScore + 1;
                _crtRender[_currentRow].Add(isPixel ? '#' : '.');
                
                Console.Write(_crtRender[_currentRow][_crtRender[_currentRow].Count-1]);
                if (counter % 20 == 0)
                {
                    _resultList.Add(counter, totalScore * counter);
                }
                if (counter % 40 == 0)
                {
                    Console.WriteLine("");
                    _currentRow++;
                }
                counter++;
            }
            return counter;
        }
    }
}
