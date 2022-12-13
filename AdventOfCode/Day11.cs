using AdventOfCode.Day11Lib;

namespace AdventOfCode
{
    internal class Day11
    {
        /// <summary>
        /// https://youtube.com/shorts/X4UdnWoK754?feature=share
        /// </summary>
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay11");
            Monkey.MonkeyList = lines.Split("\r\n\r\n")
                                  .Select(x => MonkeyFactory.CreateMonkey(x.Split("\r\n")))
                                 .ToList();
            var rounds = 10000;
            for (int i = 1; i <= rounds; i++)
            {
                ListMonkeyItems();

                Console.WriteLine($"Round {i}:");
                Monkey.MonkeyList.ForEach((monkeh) =>
                {
                    Console.Write($"{monkeh.Name} is holding: ");
                    monkeh.ItemsHolding.ToList().ForEach((item) =>
                    {
                        Console.Write($" {item} ");
                    });
                    Console.WriteLine("");
                    monkeh.ProcessRound();
                });
            }


            Monkey.MonkeyList.ForEach((monkey) =>
            {
                Console.WriteLine($"{monkey.Name} inspected {monkey.InspectCounter} items");
            });


            var answer1 = Monkey.MonkeyList
                                .OrderByDescending(x => x.InspectCounter)
                                .Take(2)
                                .Select((x) => x.InspectCounter)
                                .Aggregate((a, b) => a * b);
        }

        private static void ListMonkeyItems()
        {
            Monkey.MonkeyList.ForEach((monkeh) =>
            {
                Console.Write($"{monkeh.Name} is holding: ");
                monkeh.ItemsHolding.ToList().ForEach((item) =>
                {
                    Console.Write($" {item} ");
                });
                Console.WriteLine("");
            });
        }
    }
}
