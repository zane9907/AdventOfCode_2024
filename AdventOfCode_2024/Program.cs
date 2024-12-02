namespace AdventOfCode_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> left = [];
            List<int> right = [];

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt");
            string[] lines = File.ReadAllLines(path);

            foreach (string oneline in lines)
            {
                left.Add(int.Parse(oneline.Split(' ')[0]));
                right.Add(int.Parse(oneline.Split(' ')[1]));
            }

            int sum = 0;

            while (left.Count > 0)
            {
                int leftMin = left.Min();
                int rightMin = right.Min();

                sum += Math.Abs(leftMin - rightMin);
                left.Remove(leftMin);
                right.Remove(rightMin);
            }


            Console.WriteLine(sum);

            //Part 2

            foreach (string oneline in lines)
            {
                left.Add(int.Parse(oneline.Split(' ')[0]));
                right.Add(int.Parse(oneline.Split(' ')[1]));
            }

            sum = 0;

            for (int i = 0; i < left.Count; i++)
            {
                int count = right.Count(x => x == left[i]);
                sum += left[i] * count;
            }


            Console.WriteLine(sum);

            ;
        }
    }
}
