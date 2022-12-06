namespace AdventOfCode
{
    internal class FileLoaderService
    {
        public static string LoadFile(string fileName)
        {
            return File.ReadAllText(@$"{Directory.GetCurrentDirectory()}\{fileName}.txt");
           
        }
    }
}
