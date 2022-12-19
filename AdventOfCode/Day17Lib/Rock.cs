namespace AdventOfCode.Day17Lib
{
    public class Rock
    {
        private const int DEFAULT_OFFSET = 3;
        public List<(int x, int y)> RockShape { get; set; }
        public int Y { get; set; }
        public int X { get; set; } = DEFAULT_OFFSET;
        
        public Rock(RockType rockType)
        {
            RockShape = GenerateRockShape(rockType);
        }

        private new List<(int x, int y)> GenerateRockShape(RockType rockType)
        {
            return rockType switch
            {
                RockType.HorizontalBar => new List<(int x, int y)>() {(0, 0), (1, 0), (2, 0), (3, 0)},
                RockType.Cross => new List<(int x, int y)>() { (1, 0), (0, 1), (1, 1), (2, 1), (1, 2) },
                RockType.InvertedL => new List<(int x, int y)>() { (2, 0), (2, 1), (0, 2), (1, 2), (2, 2) },
                RockType.VerticalBar => new List<(int x, int y)>() { (0, 0), (0, 1), (0, 2), (0, 3) },
                RockType.Block => new List<(int x, int y)>() { (0, 0), (1, 0), (0, 1), (1, 1) },
                _ => throw new ArgumentOutOfRangeException(nameof(rockType), rockType, null)
            };
        }

        public static Rock GetNextRock(int index)
        {
            var rockType = (RockType)(index % 5);
            return new Rock(rockType);
        }

        internal int GetLength()
        {
            return RockShape.Select((r) => r.y).Distinct().Count();
        }
    }
}