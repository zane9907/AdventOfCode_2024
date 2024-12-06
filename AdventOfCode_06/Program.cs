namespace AdventOfCode_06
{
    internal class Program
    {
        static List<List<char>> map = [];
        static List<List<char>> loops = [];
        static int obstruction = 0;
        static (int pos1, int pos2) guardPositon = (-1, -1);

        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt");

            string[] data = [.. File.ReadAllLines(path)];


            for (int k = 0; k < data.Length; k++)
            {
                loops.Add([.. data[k]]);
            }


            for (int k = 0; k < data.Length; k++)
            {
                map.Add([.. data[k]]);
            }


            int i = 0;
            int j = 0;

            //Find guard
            for (i = 0; i < data.Length; i++)
            {
                j = data[i].ToList().FindIndex(x => x.Equals('^'));
                if (j > 0)
                {
                    guardPositon = (i, j);
                    break;
                }
            }

            bool guardLeft = false;
            while (!guardLeft/* && i < data.Count || i >= 0 || j < data[i].Length || j >= 0*/)
            {
                char guard = map[i][j];
                switch (map[i][j])
                {
                    case '^':
                        {
                            if (MoveUp(ref i, ref j))
                            {
                                guardLeft = true;
                                continue;
                            }
                            break;
                        }
                    case '>':
                        {
                            if (MoveRight(ref i, ref j))
                            {
                                guardLeft = true;
                                continue;
                            }
                            break;
                        }
                    case 'V':
                        {
                            if (MoveDown(ref i, ref j))
                            {
                                guardLeft = true;
                                continue;
                            }
                            break;
                        }
                    case '<':
                        {
                            if (MoveLeft(ref i, ref j))
                            {
                                guardLeft = true;
                                continue;
                            }
                            break;
                        }
                }

                Console.Clear();
                for (int r = 0; r < map.Count; r++)
                {
                    for (int c = 0; c < map[r].Count; c++)
                    {
                        Console.Write(map[r][c]);
                    }
                    Console.WriteLine();
                }
                var asd = obstruction;
            }

            map[i][j] = 'X';

            int visited = 0;

            for (int r = 0; r < map.Count; r++)
            {
                for (int c = 0; c < map[r].Count; c++)
                {
                    if (map[r][c] == 'X')
                    {
                        visited++;
                    }
                    Console.Write(map[r][c]);
                }
                Console.WriteLine();
            }

            Console.Clear();

            for (int r = 0; r < loops.Count; r++)
            {
                for (int c = 0; c < loops[r].Count; c++)
                {
                    Console.Write(loops[r][c]);
                }
                Console.WriteLine();
            }




            ;
        }

        static bool MoveUp(ref int i, ref int j)
        {
            if (i - 1 < 0)
            {
                return true;
            }

            if (map[i - 1][j] == '.' || map[i - 1][j] == 'X')
            {
                map[i][j] = 'X';
                i--;

                if (map[i][j] != 'X' && map[i][j + 1] == 'X' && (i != guardPositon.pos1 ^ j + 1 != guardPositon.pos2))
                {
                    obstruction++;
                }

                map[i][j] = '^';
            }
            else
            {
                map[i][j] = '>';
            }

            return false;
        }

        static bool MoveRight(ref int i, ref int j)
        {
            if (j + 1 >= map[i].Count)
            {
                return true;
            }

            if (map[i][j + 1] == '.' || map[i][j + 1] == 'X')
            {
                map[i][j] = 'X';
                j++;

                if (map[i][j] != 'X' && map[i + 1][j] == 'X' && (i + 1 != guardPositon.pos1 ^ j != guardPositon.pos2))
                {
                    obstruction++;
                }

                map[i][j] = '>';
            }
            else
            {
                map[i][j] = 'V';
            }

            return false;
        }

        static bool MoveDown(ref int i, ref int j)
        {
            if (i + 1 >= map.Count)
            {
                return true;
            }

            if (map[i + 1][j] == '.' || map[i + 1][j] == 'X')
            {
                map[i][j] = 'X';
                i++;

                if (map[i][j] != 'X' && map[i][j - 1] == 'X' && (i != guardPositon.pos1 ^ j - 1 != guardPositon.pos2))
                {
                    obstruction++;
                }

                map[i][j] = 'V';
            }
            else
            {
                map[i][j] = '<';
            }

            return false;
        }

        static bool MoveLeft(ref int i, ref int j)
        {
            if (j - 1 < 0)
            {
                return true;
            }
            //TODO loop validation with turns
            if (map[i][j - 1] == '.' || map[i][j - 1] == 'X')
            {
                map[i][j] = 'X';
                j--;

                if (map[i][j - 1] != 'X' && map[i - 1][j] == 'X' && (i - 1 != guardPositon.pos1 ^ j != guardPositon.pos2))
                {
                    obstruction++;
                }

                map[i][j] = '<';
            }
            else
            {
                map[i][j] = '^';
            }

            return false;
        }
    }
}
