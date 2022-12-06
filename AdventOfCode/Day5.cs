using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day5
    {
        public static void Run()
        {
            {
                var lines = FileLoaderService.LoadFile("puzzleInputDay5");

                var stacks = new List<Stack<string>>{
                    new Stack<string>(new string[]{"F", "T", "C", "L", "R", "P", "G", "Q"}),
                    new Stack<string>(new string[]{"N","Q","H","W","R","F","S","J"}),
                    new Stack<string>(new string[]{"F","B","H","W","P","M","Q"}),
                    new Stack<string>(new string[]{"V", "S", "T", "D", "F"}),
                    new Stack<string>(new string[]{"Q", "L", "D", "W", "V", "F", "Z"}),
                    new Stack<string>(new string[]{"Z", "C", "L", "S"}),
                    new Stack<string>(new string[]{"Z", "B", "M", "V", "D", "F"}),
                    new Stack<string>(new string[]{"T", "J", "B"}),
                    new Stack<string>(new string[]{"Q", "N", "B", "G", "L", "S", "P", "H"})
                };

                
                
                var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
                var instructions = lines.Split("\n")
                    .Select(x => regex.Split(x.Trim())
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(y => int.Parse(y))
                                .ToArray()
                    )
                    .ToList();
                
                instructions.ToList().ForEach((instruction) =>
                {
                    var amount = instruction[0];
                    var stackFrom = stacks[instruction[1] - 1];                    
                    var stackTo = stacks[instruction[2] - 1];
                    var tempStack = new Stack<string>();

                    while (amount > 0)
                    {
                        var item = stackFrom.Pop();
                        tempStack.Push(item);
                        amount--;
                    }

                    while (tempStack.Any())
                    {
                        var item = tempStack.Pop();
                        stackTo.Push(item);
                    }
                });

                stacks.ForEach(x => Console.Write(x.Peek()));
                
            }
        }
    }
}
