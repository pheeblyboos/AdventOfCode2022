namespace AdventOfCode
{
    internal class Day7
    {
        public static void Run()
        {
            var lines = FileLoaderService.LoadFile("puzzleInputDay7");
            var list = lines.Split("\n")
                            .Select(x => x.Trim())
                            .ToList();
            var rootObject = new DirectoryObject("/") { Parent = null, SubObjects = new List<DirectoryObject>() };
            var resultPartOneList = new List<DirectoryObject>();
            var resultPartTwoList = new List<DirectoryObject>();

            var isListing = false;
            var currentObject = rootObject;
            list.ForEach(instruction =>
            {
                var subInstructions = instruction.Split(" ");
                if (subInstructions[0].Contains('$'))
                {
                    isListing = false;
                    var command = subInstructions[1] ?? throw new InvalidOperationException();

                    if (command == "ls") isListing = true;
                    if (command == "cd")
                    {
                        var subDirectory = subInstructions[2];
                        currentObject = subDirectory switch
                        {
                            ".."    => currentObject.Parent,
                            "/"     => rootObject,
                             _      => currentObject.SubObjects.First(s => s.Name.EndsWith(subDirectory))
                         };

                    }
                }
                else if (isListing)
                {
                    
                    if(subInstructions[0] == "dir")
                    {
                        currentObject.SubObjects.Add(CreateDirectory(subInstructions[1], currentObject));
                    }
                    else if (int.TryParse(subInstructions[0], out int result))
                    {
                        currentObject.SubObjects.Add(CreateFile(result, currentObject, subInstructions[1]));
                        AddSizeToDirectories(resultPartOneList, resultPartTwoList, currentObject, result);
                    }
                }
                
            });

            var answer1 = resultPartOneList.Sum(e => e.Size);
            var TOTAL_SPACE = 70000000;
            var freeSpace = TOTAL_SPACE - rootObject.Size;
            var REQUIRED_SPACE = 30000000;
            var spaceToFree = REQUIRED_SPACE - freeSpace;
            var answer2 = resultPartTwoList.Where(x => x.Size >= spaceToFree).OrderBy(x => x.Size).ToList();

            Console.WriteLine($"{answer1} {answer2}");

        }

        private static void AddSizeToDirectories(List<DirectoryObject> resultListPartOne, List<DirectoryObject> resultListPartTwo, DirectoryObject? currentObject, int result)
        {
            var tempObject = currentObject;
            while (tempObject != null)
            {
                tempObject.Size += result;
                if (tempObject.Size <= 100000)
                {
                    AddOrUpdateList(resultListPartOne, tempObject);
                    
                } else
                {
                    resultListPartOne.Remove(tempObject);

                    AddOrUpdateList(resultListPartTwo, tempObject);

                }

                    
                tempObject = tempObject.Parent;
            }
        }

        private static void AddOrUpdateList(List<DirectoryObject> resultList, DirectoryObject tempObject)
        {
            if (resultList.Any(x => x.Name == tempObject.Name))
            {
                var index = resultList.FindIndex(x => x.Name == tempObject.Name);
                resultList[index] = tempObject;
                return;
            }

            resultList.Add(tempObject);
        }

        private static DirectoryObject CreateFile(int result, DirectoryObject currentObject, string fileName)
        {
            return new DirectoryObject(fileName) { Parent = currentObject, Size = result };
        }

        private static DirectoryObject CreateDirectory(string directoryName, DirectoryObject currentObject)
        {
            var name = GenerateDirectoryName(currentObject, directoryName);
            return new DirectoryObject(name) { Parent = currentObject, SubObjects = new List<DirectoryObject>() };
        }

        private static string GenerateDirectoryName(DirectoryObject currentObject, string directoryName)
        {
            var result = directoryName;
            var tempObject = currentObject;
            while(tempObject?.Name != "/")
            {
                result = $"{tempObject?.Name}/{result}";
                tempObject = tempObject?.Parent;

            }

            return result;
        }
    }
}
