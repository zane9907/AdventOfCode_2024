﻿namespace AdventOfCode_Helper
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

        public static List<string> ReadFileStringList(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        public static char[,] ReadFileCharMatrixArray(string path)
        {
            string[] data = File.ReadAllLines(path);
            char[,] result = new char[data.Length,data[0].Length];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = data[i][j];
                }
            }

            return result;
        }

        public static List<List<char>> ReadFileCharMatrixList(string path)
        {
            string[] data = File.ReadAllLines(path);
            List<List<char>> result = [];

            for (int i = 0; i < data.Length; i++)
            {
                result.Add([.. data[i]]);
            }

            return result;
        }
    }
}
