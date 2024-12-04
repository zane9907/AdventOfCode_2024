using System.Text.RegularExpressions;

namespace AdventOfCode_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt");

            string[] data = File.ReadAllLines(path);

            int sum = 0;
            Regex rg = new Regex("XMAS");
            foreach (string line in data)
            {
                var xmas = rg.Matches(line);
                sum += xmas.Count;
            }

            rg = new("SAMX");
            foreach (string line in data)
            {
                var samx = rg.Matches(line);
                sum += samx.Count;
            }


            string pattern = "XMAS";

            //DOWN

            int i = 0;
            int j = 0;

            while (j < data[i].Length)
            {
                while (i < data.Length)
                {
                    bool wordFound = true;
                    for (int w = 0; w < pattern.Length; w++)
                    {
                        if (i + w >= data.Length || data[i + w][j] != pattern[w])
                        {
                            wordFound = false;
                            break;
                        }
                    }

                    if (wordFound)
                    {
                        sum++;
                    }

                    i++;
                }

                i = 0;
                j++;
            }



            //UP

            i = data.Length - 1;
            j = 0;

            while (j < data[i].Length)
            {
                while (i != 0)
                {
                    bool wordFound = true;
                    for (int w = 0; w < pattern.Length; w++)
                    {
                        if (i - w < 0 || data[i - w][j] != pattern[w])
                        {
                            wordFound = false;
                            break;
                        }
                    }

                    if (wordFound)
                    {
                        sum++;
                    }

                    i--;
                }

                i = data.Length - 1;
                j++;
            }


            //diagonal top-left bottom-right
            i = 0;
            j = 0;

            while (i < data.Length)
            {
                while (j < data[i].Length)
                {
                    bool wordFound = true;
                    for (int w = 0; w < pattern.Length; w++)
                    {
                        if (i + w >= data.Length || j + w >= data[i].Length || data[i + w][j + w] != pattern[w])
                        {
                            wordFound = false;
                            break;
                        }
                    }

                    if (wordFound)
                    {
                        sum++;
                    }

                    j++;
                }

                j = 0;
                i++;
            }

            //diagonal top-right bottom-left
            i = 0;
            j = data[0].Length - 1;

            while (i < data.Length)
            {
                while (j != 0)
                {
                    var asd = data[i][j];
                    bool wordFound = true;
                    for (int w = 0; w < pattern.Length; w++)
                    {
                        if (i + w >= data.Length || j - w < 0 || data[i + w][j - w] != pattern[w])
                        {
                            wordFound = false;
                            break;
                        }
                    }

                    if (wordFound)
                    {
                        sum++;
                    }

                    j--;
                }

                j = data[0].Length - 1;
                i++;
            }

            //diagonal  bottom-left top-right
            i = data.Length - 1;
            j = 0;

            while (i != 0)
            {
                while (j < data[i].Length)
                {
                    bool wordFound = true;
                    for (int w = 0; w < pattern.Length; w++)
                    {
                        if (i - w < 0 || j + w >= data[i].Length || data[i - w][j + w] != pattern[w])
                        {
                            wordFound = false;
                            break;
                        }
                    }

                    if (wordFound)
                    {
                        sum++;
                    }

                    j++;
                }

                j = 0;
                i--;
            }

            //diagonal  bottom-right top-left
            i = data.Length - 1;
            j = data[0].Length - 1;

            while (i != 0)
            {
                while (j != 0)
                {
                    bool wordFound = true;
                    for (int w = 0; w < pattern.Length; w++)
                    {
                        if (i - w < 0 || j - w < 0 || data[i - w][j - w] != pattern[w])
                        {
                            wordFound = false;
                            break;
                        }
                    }

                    if (wordFound)
                    {
                        sum++;
                    }

                    j--;
                }

                j = data[0].Length - 1;
                i--;
            }


            //Part 2
            sum = 0;
            pattern = "MAS";
            string reversePattern = "SAM";
            i = 0;
            j = 0;
            while (i < data.Length)
            {
                while (j < data[i].Length)
                {
                    bool firstWordFound = FindFirstWord(pattern, reversePattern, i, j, data);
                    bool secondWordFound = FindSecondWord(pattern, reversePattern, i, j + 2, data);

                    if (firstWordFound && secondWordFound)
                    {
                        sum++;
                    }
                    j++;
                }
                j = 0;
                i++;
            }


            ;
        }

        static bool FindSecondWord(string pattern, string reversePattern, int i, int j, string[] data)
        {
            //MAS down
            bool WordFound = true;
            for (int w = 0; w < pattern.Length; w++)
            {
                if (i + w >= data.Length || j - w >= data[i].Length || data[i + w][j - w] != pattern[w])
                {
                    WordFound = false;
                    break;
                }
            }

            if (WordFound)
            {
                return WordFound;
            }

            //SAM down
            WordFound = true;
            for (int w = 0; w < reversePattern.Length; w++)
            {
                if (i + w >= data.Length || j - w >= data[i].Length || data[i + w][j - w] != reversePattern[w])
                {
                    WordFound = false;
                    break;
                }
            }

            return WordFound;
        }

        static bool FindFirstWord(string pattern, string reversePattern, int i, int j, string[] data)
        {
            //MAS down
            bool WordFound = true;
            for (int w = 0; w < pattern.Length; w++)
            {
                if (i + w >= data.Length || j + w >= data[i].Length || data[i + w][j + w] != pattern[w])
                {
                    WordFound = false;
                    break;
                }
            }

            if (WordFound)
            {
                return WordFound;
            }

            //SAM down
            WordFound = true;
            for (int w = 0; w < reversePattern.Length; w++)
            {
                if (i + w >= data.Length || j + w >= data[i].Length || data[i + w][j + w] != reversePattern[w])
                {
                    WordFound = false;
                    break;
                }
            }

            return WordFound;
        }
    }
}
