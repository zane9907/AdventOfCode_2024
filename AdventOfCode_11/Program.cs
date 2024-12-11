using AdventOfCode_Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> data = Helper.ReadFileStringArraySplit(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt")).ToList();

            Dictionary<(string, int), long> count = [];

            int count1 = CalculateStonesParallel(new(data), 25); //Part 1
            Console.Clear();
            int count2 = CalculateStonesParallel(new(data), 75); //Part 2


            ;
        }

        static int CalculateStonesDict(string number, Dictionary<(string, int), long> dict, int blink)
        {
            if (blink == 0)
            {
                return 1;
            }

            if (number == "0")
            {
                
            }

        }







        static int CalculateStones(List<string> data, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(i);
                List<string> result = [];
                for (int j = 0; j < data.Count; j++)
                {
                    var asd = data[j];
                    if (data[j] == "0")
                    {
                        result.Add("1");
                    }
                    else if (data[j].Length % 2 == 0)
                    {
                        string left = data[j].Substring(0, data[j].Length / 2);
                        string right = data[j].Substring(data[j].Length / 2).All(x => x.Equals('0'))
                            ? "0"
                            : data[j].Substring(data[j].Length / 2).TrimStart('0');

                        result.Add(left);
                        result.Add(right);
                    }
                    else
                    {
                        result.Add($"{long.Parse(data[j]) * 2024}");
                    }
                }

                data = new(result);
                result.Clear();
            }

            return data.Count;
        }


        static object LockObject = new();

        static int CalculateStonesParallel(List<string> input, int length)
        {
            List<Stone> data = [];
            for (int d = 0; d < input.Count; d++)
            {
                data.Add(new Stone
                {
                    Index = d,
                    Value = input[d]
                });
            }

            int sum = 0;

            for (int s = 0; s < data.Count; s++)
            {
                List<Stone> oneStone = [data[s]];
                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine(i);


                    List<Stone> result = [];
                    foreach (Stone stone in oneStone)
                    {
                        if (stone.Value == "0")
                        {
                            lock (LockObject)
                            {
                                result.Add(new Stone
                                {
                                    Index = stone.Index,
                                    Value = "1"
                                });
                            }
                        }
                        else if (stone.Value.Length % 2 == 0)
                        {
                            string left = stone.Value.Substring(0, stone.Value.Length / 2);
                            string right = stone.Value.Substring(stone.Value.Length / 2).All(x => x.Equals('0'))
                                ? "0"
                                : stone.Value.Substring(stone.Value.Length / 2).TrimStart('0');

                            lock (LockObject)
                            {
                                result.Add(new Stone
                                {
                                    Index = stone.Index,
                                    Value = left
                                });
                                result.Add(new Stone
                                {
                                    Index = stone.Index,
                                    Value = right
                                });
                            }
                        }
                        else
                        {
                            lock (LockObject)
                            {
                                result.Add(new Stone
                                {
                                    Index = stone.Index,
                                    Value = $"{long.Parse(stone.Value) * 2024}"
                                });
                            }
                        }
                    }

                    oneStone = new(result);
                    result.Clear();
                }

                sum += oneStone.Count;
            }

            return sum;
        }


    }

    public class Stone
    {
        public int Index { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
