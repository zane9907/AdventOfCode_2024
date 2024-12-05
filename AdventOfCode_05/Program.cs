namespace AdventOfCode_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt");

            string[] data = File.ReadAllLines(path);

            Dictionary<int, List<int>> rules = [];

            int i = 0;
            while (data[i].Contains('|'))
            {
                List<int> split = data[i].Split('|').ToList().ConvertAll(int.Parse);
                if (rules.ContainsKey(split[0]))
                {
                    rules[split[0]].Add(split[1]);
                }
                else
                {
                    rules.Add(split[0], [split[1]]);
                }

                i++;
            }

            i++;

            int sum = 0;
            List<List<int>> incorrect = [];
            while (i < data.Length)
            {
                List<int> updates = data[i].Split(',').ToList().ConvertAll(int.Parse);

                bool isCorrect = true;

                for (int j = 0; j < updates.Count; j++)
                {
                    if (!rules.ContainsKey(updates[j]))
                    {
                        //if first number is not in the rules than the update is wrong
                        isCorrect = false;
                        break;
                    }
                    var sad = rules[updates[j]];//debug
                    List<int> others = updates.Skip(j + 1).ToList();
                    int k = 0;
                    while (k < others.Count && isCorrect)
                    {
                        if (!rules[updates[j]].Contains(others[k]))
                        {
                            isCorrect = false;
                        }

                        k++;
                    }
                    if (!isCorrect)
                    {
                        incorrect.Add(updates);
                        break;
                    }
                }
                if (isCorrect)
                {
                    sum += updates[updates.Count / 2];
                }


                i++;
            }


            //PART 2
            sum = 0;
            for (int j = 0; j < incorrect.Count; j++)
            {
                List<int> updates = incorrect[j];
                int resultLength = updates.Count;
                List<int> ordered = [];


                int k = 0;
                while (ordered.Count != resultLength)
                {
                    var rule = rules[updates[k]]; //debug
                    List<int> others = updates.Where(x => x != updates[k]).ToList();
                    bool isRightPlace = true;

                    int debugCounter = 0;
                    foreach (int item in others)
                    {
                        if (!rule.Contains(item))
                        {
                            isRightPlace = false;
                            break;
                        }
                        debugCounter++;
                    }

                    if (isRightPlace)
                    {
                        ordered.Add(updates[k]);
                        updates.RemoveAt(k);
                        k = 0;
                    }
                    else
                    {
                        k++;
                    }
                }

                sum += ordered[ordered.Count / 2];
            }

            ;
        }
    }
}
