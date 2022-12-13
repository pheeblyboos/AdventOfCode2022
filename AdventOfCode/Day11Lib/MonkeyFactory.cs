using System.Text.RegularExpressions;

namespace AdventOfCode.Day11Lib
{
    internal class MonkeyFactory
    {
        public static Monkey CreateMonkey(string[] lines)
        {

            (char operation, int number) operations = SetOperation(lines[2]);
            (int condition, string testTrue, string testFalse) test = SetTest(lines);
            
            var monkey = new Monkey(operations, test);
            monkey.Name = lines[0].Replace(':', ' ').Trim();
            monkey.ItemsHolding = new Queue<long>(lines[1].Replace("Starting items:", "").Trim().Split(',').Select(long.Parse).ToList());

            return monkey;
        }

        private static (int condition, string testTrue, string testFalse) SetTest(string[] lines)
        {
            Regex regex = new Regex(@"\d+$");
            var result = lines.Skip(3)
                .Select(x => int.Parse(regex.Match(x).Value)).ToList();
            return (result[0], $"Monkey {result[1]}", $"Monkey {result[2]}");
        }
        
        private static (char operation, int number) SetOperation(string line)
        {
            var opstring = line.Replace("Operation: new = old ", "").Trim().Split(' ');
            
            var operationNumber = int.TryParse(opstring[1], out var result) ? result : 0;
            
            return (char.Parse(opstring[0]), operationNumber);
        }
    }
}
