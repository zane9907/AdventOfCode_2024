namespace AdventOfCode_Helper
{
    public static class Helper
    {
        public static string[] ReadFileStringArray(string path)
        {
            return File.ReadAllLines(path);
        }

        public static string ReadFileString(string path)
        {
            return File.ReadAllText(path);
        }

        public static List<int> ReadFileIntList(string path)
        {
            return File.ReadAllLines(path).ToList().ConvertAll(int.Parse);
        }
    }
}
