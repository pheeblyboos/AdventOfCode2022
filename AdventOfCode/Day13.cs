using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace AdventOfCode
{
    internal class Day13
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay13")
            .Split("\r\n\r\n")
            .Select(x => x.Split("\r\n"))
            .ToList();

            var subLists = new List<List<ArrayList>>();

            lines.ForEach((lineSet) => {
                var subList = new List<ArrayList>();
                for (int i = 0; i < lineSet.Length; i++)
                {
                    subList.Add(JsonConvert.DeserializeObject<ArrayList>(lineSet[i]));
                }
                subLists.Add(subList);
            });

            var answer = new List<int>();
            var currentIndex = 0;

            subLists.ForEach((list) =>
            {
                var leftList = list[0];
                var rightList = list[1];

                for (int i = 0; i < Math.Max(leftList.Count, rightList.Count); i++)
                {
                    if (i >= rightList.Count)
                    {
                        break;
                    }

                    if (i >= leftList.Count)
                    {
                        Console.WriteLine("Correct");
                        answer.Add(currentIndex + 1);
                        break;
                    }
                    Console.WriteLine($"Compare {JsonConvert.SerializeObject(leftList)} with {JsonConvert.SerializeObject(rightList)}");
                    var result = CompareLists(leftList, rightList, i, answer);
                    
                    if (result == 1)
                    {
                        answer.Add(currentIndex + 1);
                        break;
                    }
                    if (result == -1)
                    {
                        break;
                    }
                }
                currentIndex++;
            });

            var answer1 = answer.Sum();
        }

        private static int CompareLists(ArrayList leftList, ArrayList rightList, int index, List<int> answer)
        {
            Console.WriteLine($"Compare {JsonConvert.SerializeObject(leftList)} with {JsonConvert.SerializeObject(rightList)}");
            //If one of the items is a number and the other is not, wrap the number in a list
            leftList[index] = isNumber(leftList[index]) && !isNumber(rightList[index]) ? new JArray() { leftList[index] } : leftList[index];
            rightList[index] = isNumber(rightList[index]) && !isNumber(leftList[index]) ? new JArray() { rightList[index] } : rightList[index];

            var left = leftList[index];
            var right = rightList[index];

            Console.WriteLine($"Compare {JsonConvert.SerializeObject(left)} with {JsonConvert.SerializeObject(right)}");


            if (isNumber(left) && isNumber(right))
            {
                Console.WriteLine("Both are numbers");
                if ((long)left > (long)right)
                {
                    Console.WriteLine("Not correct");
                    return -1;
                }
                else if ((long)left < (long)right)
                {
                    Console.WriteLine("Correct");
                    return 1;
                }
            }

            if (isList(left) && isList(right))
            {
                Console.WriteLine("Both are lists");
                var listLeft = (ArrayList)((JArray)left).ToObject(typeof(ArrayList));
                var listRight = (ArrayList)((JArray)right).ToObject(typeof(ArrayList));
                for (int j = 0; j < Math.Max(listLeft.Count, listRight.Count); j++)
                {
                    if (j >= listLeft.Count && listLeft.Count == listRight.Count) return 0;
                    if (j >= listRight.Count && listLeft.Count != listRight.Count)
                    {
                        Console.WriteLine("Not correct");
                        return -1;
                    }
                    if (j >= listLeft.Count && listLeft.Count != listRight.Count)
                    {
                        Console.WriteLine("Correct");
                        return 1;
                    }

                    var result = CompareLists(listLeft, listRight, j, answer);

                    if (result == 1 || result == -1) return result;

                }
            }

            return 0;
        }

        private static bool isList(object item)
        {
            return item.GetType() == typeof(JArray);
        }

        private static bool isNumber(object item)
        {
            if (item == null) throw new InvalidOperationException("Item is null");
            return item is long;
        }
    }
}
