namespace AdventOfCode.Day11Lib
{
    internal class Monkey
    {
        private readonly char _operation;
        private readonly int _operationNumber;
        private readonly int _test;
        private readonly string _testTrue;
        private readonly string _testFalse;

        public Monkey((char operation, int number) operations, (int condition, string testTrue, string testFalse) test)
        {
            _operation = operations.operation;
            _operationNumber = operations.number;
            _test = test.condition;
            _testTrue = test.testTrue;
            _testFalse = test.testFalse;
        }
        public static List<Monkey> MonkeyList { get; set; } = new List<Monkey>();
        
        public string Name { get; set; }
        public Queue<long> ItemsHolding { get; set; }
        public string NextTarget { get; set; }
        public long InspectCounter { get; set; }
        

        public void DetermineNextTarget(long worryLevel)
        {
            NextTarget = worryLevel % _test == 0 ? _testTrue : _testFalse;
        }
        
        public void Throw(long threwOut)
        {
            var nextMonkey = MonkeyList.First(x => x.Name.EndsWith(NextTarget));
            nextMonkey.ItemsHolding.Enqueue(threwOut);
            Console.WriteLine($"{Name} threw {threwOut} to {nextMonkey.Name}");
            
        }

        public void ProcessRound()
        {
            while (ItemsHolding.Any())
            {
                InspectCounter++;
                var worryLevelItem = ItemsHolding.Dequeue();
                worryLevelItem = CalculateWorryLevel(worryLevelItem);
                DetermineNextTarget(worryLevelItem);
                Throw(worryLevelItem);
            }
        }

        private long CalculateWorryLevel(long worryLevel)
        {
            var value = _operationNumber == 0 ? worryLevel : _operationNumber;
            var normalizer = MonkeyList.Select(x => x._test).Aggregate((a, b) => a * b);
            if(_operation == '*')
            {
                return (worryLevel * value) % normalizer;
            }

            return (worryLevel + value) % normalizer;
            
        }
    }
}
