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
            var rootObject = new DirectoryObject() { Name = "/", Parent = null, SubObjects = new List<DirectoryObject>() };
            var resultList = new List<DirectoryObject>();
            var totalList = new List<string>();

            var isListing = false;
            var currentObject = rootObject;
            list.ForEach(instruction =>
            {
                var subInstructions = instruction.Split(" ");
                if (subInstructions[0].Contains('$'))
                {
                    isListing = false;
                    var command = subInstructions[1] ?? throw new InvalidOperationException();
                    switch (command)
                    {
                        case "ls":
                            isListing = true;
                            break;
                        case "cd":
                            var subDirectory = subInstructions[2];
                            if(subDirectory == "..")
                            {
                                currentObject = currentObject.Parent;
                            } else if (subDirectory == "/")
                            {
                                currentObject = rootObject;
                            } else
                            {
                                currentObject = currentObject.SubObjects.First(s => s.Name.EndsWith(subDirectory));
                            }
                        break;

                    }
                }
                else if (isListing)
                {
                    
                    if(subInstructions[0] == "dir")
                    {
                        currentObject.SubObjects.Add(CreateDirectory(subInstructions[1], currentObject));
                        totalList.Add(subInstructions[1]);
                    }
                    else if (int.TryParse(subInstructions[0], out int result))
                    {
                        currentObject.SubObjects.Add(CreateFile(result, currentObject, subInstructions[1]));
                        AddSizeToDirectories(resultList, currentObject, result);
                    }
                }
                
            });

            Console.WriteLine(resultList.Sum(e => e.Size));

        }

        private static void AddSizeToDirectories(List<DirectoryObject> resultList, DirectoryObject? currentObject, int result)
        {
            var tempObject = currentObject;
            while (tempObject != null)
            {
                tempObject.Size += result;
                if (tempObject.Size <= 100000)
                {
                    if (resultList.Any(x => x.Name == tempObject.Name))
                    {
                        var index = resultList.FindIndex(x => x.Name == tempObject.Name);
                        resultList[index] = tempObject;
                    }
                    else
                    {
                        resultList.Add(tempObject);
                    }
                }
                else
                {
                    var x = resultList.Remove(tempObject);
                }
                tempObject = tempObject.Parent;
            }
        }

        private static DirectoryObject CreateFile(int result, DirectoryObject currentObject, string fileName)
        {
            return new DirectoryObject() { Name = fileName, Parent = currentObject, Size = result };
        }

        private static DirectoryObject CreateDirectory(string directoryName, DirectoryObject currentObject)
        {
            var name = GenerateDirectoryName(currentObject, directoryName);
            return new DirectoryObject() { Name = name, Parent = currentObject, SubObjects = new List<DirectoryObject>() };
        }

        private static string GenerateDirectoryName(DirectoryObject currentObject, string directoryName)
        {
            var result = directoryName;
            var tempObject = currentObject;
            while(tempObject.Name != "/")
            {
                result = $"{tempObject.Name}/{result}";
                tempObject = tempObject.Parent;

            }

            return result;
        }
    }

    internal class DirectoryObject
    {
        public int Size { get; set; }
        public List<DirectoryObject> SubObjects { get; set; }
        public string Name { get; set; }
        public DirectoryObject? Parent { get; set; }
    }
}
