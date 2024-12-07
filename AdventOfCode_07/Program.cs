using AdventOfCode_Helper;

namespace AdventOfCode_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = Helper.ReadFileStringArray(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));


            List<string> incorrect = [];
            ulong sum = 0;
            foreach (string line in data)
            {
                string[] split = line.Split(':');
                ulong result = ulong.Parse(split[0]);
                List<ulong> numbers = split[1].TrimStart().Split(' ').ToList().ConvertAll(ulong.Parse);

                if (CalculateEquation(numbers[0], result, 0, numbers))
                {
                    sum += result;
                }
                else
                {
                    incorrect.Add(line);
                }
                ;
            }

            //PART 2

            foreach (string line in incorrect)
            {
                string[] split = line.Split(':');
                ulong result = ulong.Parse(split[0]);
                List<ulong> numbers = split[1].TrimStart().Split(' ').ToList().ConvertAll(ulong.Parse);

                if (CalculateEquation(numbers[0], result, 0, numbers, true))
                {
                    sum += result;
                }
                ;
            }
            ;
        }

        static bool CalculateEquation(ulong sum, ulong result, int i, List<ulong> numbers, bool part2 = false)
        {
            if (i + 1 == numbers.Count)
            {
                if (sum == result)
                {
                    return true;
                }
                return false;
            }


            ulong add = sum + numbers[i + 1];

            if (CalculateEquation(add, result, i + 1, numbers, part2))
            {
                return true;
            }

            ulong mul = sum * numbers[i + 1];

            if (CalculateEquation(mul, result, i + 1, numbers, part2))
            {
                return true;
            }

            if (part2)
            {
                ulong concat = ulong.Parse($"{sum}{numbers[i + 1]}");

                if (CalculateEquation(concat, result, i + 1, numbers, part2))
                {
                    return true;
                } 
            }

            return false;
        }
    }
}
