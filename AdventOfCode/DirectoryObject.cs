namespace AdventOfCode
{
    internal class DirectoryObject
    {
        public DirectoryObject(string name)
        {
            Name = name;
            SubObjects = new List<DirectoryObject>();
        }

        public int Size { get; set; }
        public List<DirectoryObject> SubObjects { get; set; }
        public string Name { get; set; }
        public DirectoryObject? Parent { get; set; }

        public static DirectoryObject CreateFile(int result, DirectoryObject currentObject, string fileName)
        {
            return new DirectoryObject(fileName) { Parent = currentObject, Size = result };
        }

        public static DirectoryObject CreateDirectory(string directoryName, DirectoryObject currentObject)
        {
            var name = GenerateDirectoryName(currentObject, directoryName);
            return new DirectoryObject(name) { Parent = currentObject, SubObjects = new List<DirectoryObject>() };
        }


        public static int CalculateSpaceToFree(DirectoryObject rootObject)
        {
            var TOTAL_SPACE = 70000000;
            var REQUIRED_SPACE = 30000000;

            return REQUIRED_SPACE - (TOTAL_SPACE - rootObject.Size);
        }
        
        private static string GenerateDirectoryName(DirectoryObject currentObject, string directoryName)
        {
            var result = directoryName;
            var tempObject = currentObject;
            while (tempObject?.Name != "/")
            {
                result = $"{tempObject?.Name}/{result}";
                tempObject = tempObject?.Parent;

            }

            return result;
        }
    }
}
