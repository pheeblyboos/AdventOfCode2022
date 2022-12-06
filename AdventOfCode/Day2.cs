namespace AdventOfCode
{
    internal class Day2
    {
        public static void Run()
        {
            var linesDay2 = FileLoaderService.LoadFile("puzzleInputDay2");
            var listDay2 = linesDay2.Split("\n")
                            .Select(x => x.Split(" "))
                            .ToList();

            var answerDay2 = listDay2.Select(x => DetermineScore(x)).Sum();
            Console.WriteLine($"Day 2: {answerDay2}");
        }
        public static int DetermineScore(string[] input)
        {
            var opponentPlay = input[0];
            var myPlay = input[1];
            var total = 0;

            total += MatchOutcome(myPlay, opponentPlay);

            Console.WriteLine(total);

            return total;
        }

        private static int MatchOutcome(string myPlay, string opponentPlay)
        {
            var total = 0;
            switch (myPlay)
            {
                // Lose
                case "X":
                    total += (opponentPlay == "A") ? 3 : (opponentPlay == "B") ? 1 : 2;
                    break;
                // Draw
                case "Y":
                    total += (opponentPlay == "A") ? 1 : (opponentPlay == "B") ? 2 : 3;
                    total += 3;
                    break;
                case "Z":
                    total += (opponentPlay == "A") ? 2 : (opponentPlay == "B") ? 3 : 1;
                    total += 6;
                    break;
            }

            return total;
        }
    }
}
