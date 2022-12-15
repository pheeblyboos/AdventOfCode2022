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
            var arrayLists = new List<List<ArrayList>>();

            lines.ForEach((line) =>
            {
                var subList = new List<ArrayList>();
                // split both strings into array objects and sub arrays
                foreach (var subline in line)
                {
                    var item = ProcessCharacters(subline);
                    subList.Add(item);
                }
                arrayLists.Add(subList);

            });

            var answer = new List<int>();
            
            arrayLists.ForEach((sublist) =>
            {
                var numberA = GetNextNumber(sublist[0], 0);
                var numberB = GetNextNumber(sublist[1], 0);
                var index = 1;
                while (numberA >= numberB)
                {
                    if(sublist[0].Count > index || sublist[1].Count > index) 
                    if (numberA > numberB) break;
                    if (numberA == numberB)
                    {
                        numberA = GetNextNumber(sublist[0], index);
                        numberB = GetNextNumber(sublist[1], index);
                    }
                    index++;
                }
                
                if(numberA < numberB)
                {
                    answer.Add(arrayLists.IndexOf(sublist) + 1);
                }

            });


        }

        private static int GetNextNumber(ArrayList arrayList, int index)
        {
            if (arrayList[index].GetType() == typeof(ArrayList) && ((ArrayList) arrayList[index]).Count > 0)
            {
                return GetNextNumber((ArrayList)arrayList[index], index);
            }
            else
            {
                return (int)arrayList[index];
            }

        }

        public static ArrayList ProcessCharacters(string subline)
        {
            var arrayObject = new ArrayList();
            var currentNumber = "";
            var parent = arrayObject;
            var currentList = arrayObject;
            for (int i = 0; i < subline.Length; i++)
            {
                var character = subline[i];
                var previousCharacter = i - 1 >= 0 ? subline[i - 1] : '~';
                var nextCharacter = i + 1 > subline.Length ? subline[i + 1] : '~';
                if ((previousCharacter == '[' || previousCharacter == ',') && character == '[')
                {
                    var newArray = new ArrayList();
                    currentList.Add(newArray);
                    parent = currentList;
                    currentList = newArray;
                   
                }
                
                else if(character == ']')
                {
                    if(previousCharacter != character && previousCharacter != '[')
                    {
                        currentList.Add(int.Parse(currentNumber));
                        currentNumber = "";
                    }
                    currentList = parent;
                }
                
                else if(int.TryParse(character.ToString(), out int _))
                {
                    currentNumber += character;                 
                }
                else if (character == ',' && previousCharacter != ']')
                {
                    currentList.Add(int.Parse(currentNumber));
                    currentNumber = "";
                }

                
            }
            return arrayObject;
        }
    }
}
