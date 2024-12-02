using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt");

            string[] lines = File.ReadAllLines(path);

            int safe = 0;

            foreach (string line in lines)
            {
                List<int> data = line.Split(' ').ToList().ConvertAll(int.Parse);

                bool isSafe = is_safe(data);
                if (isSafe)
                {
                    safe++;
                }
            }

            //part 2

            safe = 0;
            foreach (string line in lines)
            {
                List<int> data = line.Split(' ').ToList().ConvertAll(int.Parse);

                //increasing
                for (int i = 0; i < data.Count; i++)
                {
                    List<int> newData = [.. data];
                    newData.RemoveAt(i);
                    bool isSafe = is_safe(newData);
                    if (isSafe)
                    {
                        safe++;
                        break;
                    }
                }
            }



            ;

        }

        private static bool is_safe(List<int> data)
        {
            bool isIncreasing = true;
            //increasing
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i] >= data[i + 1] || !Differ1to3(data[i], data[i + 1]))
                {
                    isIncreasing = false;
                    break;
                }
            }
            if (isIncreasing)
            {
                return true;
            }

            bool isDecreasing = true;
            //decreasing
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i] <= data[i + 1] || !Differ1to3(data[i], data[i + 1]))
                {
                    isDecreasing = false;
                    break;
                }
            }
            if (isDecreasing)
            {
                return true;
            }

            return false;
        }

        private static bool Differ1to3(int a, int b)
        {
            int result = Math.Abs(a - b);
            return result >= 1 && result <= 3;
        }
    }
}
