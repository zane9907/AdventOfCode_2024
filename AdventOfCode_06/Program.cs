using System.Numerics;

namespace AdventOfCode_06
{
    internal class Program
    {
        static List<List<char>> map = [];
        static List<List<char>> loops = [];
        static int obstruction = 0;
        static bool validLoop = false;
        static bool guardLeft = false;
        static int turns = 0;

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

            int obstacleX = 0;
            int obstacleY = 0;
            //while (!guardLeft)
            //{
            //    if (map[obstacleX][obstacleY] != '#')
            //    {
            //        map[obstacleX][obstacleY] = 'O';
            //    }
            //    char guard = map[i][j];
            //    switch (map[i][j])
            //    {
            //        case '^':
            //            {
            //                if (MoveUp(ref i, ref j))
            //                {
            //                    guardLeft = true;
            //                    continue;
            //                }
            //                break;
            //            }
            //        case '>':
            //            {
            //                if (MoveRight(ref i, ref j))
            //                {
            //                    guardLeft = true;
            //                    continue;
            //                }
            //                break;
            //            }
            //        case 'V':
            //            {
            //                if (MoveDown(ref i, ref j))
            //                {
            //                    guardLeft = true;
            //                    continue;
            //                }
            //                break;
            //            }
            //        case '<':
            //            {
            //                if (MoveLeft(ref i, ref j))
            //                {
            //                    guardLeft = true;
            //                    continue;
            //                }
            //                break;
            //            }
            //    }

            //    Console.Clear();
            //    for (int r = 0; r < map.Count; r++)
            //    {
            //        for (int c = 0; c < map[r].Count; c++)
            //        {
            //            Console.Write(map[r][c]);
            //        }
            //        Console.WriteLine();
            //    }
            //}


            //map[i][j] = 'X';

            int visited = 0;

            //for (int r = 0; r < map.Count; r++)
            //{
            //    for (int c = 0; c < map[r].Count; c++)
            //    {
            //        if (map[r][c] == 'X')
            //        {
            //            visited++;
            //        }
            //        Console.Write(map[r][c]);
            //    }
            //    Console.WriteLine();
            //}

            List<string> places = [];
            i = guardPositon.pos1;
            j = guardPositon.pos2;
            while (obstacleX < map.Count && obstacleY < map.Count)
            {

                if (map[obstacleX][obstacleY] != '#')
                {
                    if (obstacleX == guardPositon.pos1 && obstacleY == guardPositon.pos2)
                    {
                        map.Clear();
                        for (int k = 0; k < data.Length; k++)
                        {
                            map.Add([.. data[k]]);
                        }

                        i = guardPositon.pos1;
                        j = guardPositon.pos2;

                        validLoop = false;
                        guardLeft = false;
                        obstacleY++;
                        if (obstacleY == map[0].Count)
                        {
                            obstacleX++;
                            obstacleY = 0;
                        }

                        continue;
                    }
                    else
                    {
                        map[obstacleX][obstacleY] = '#';
                    }
                }
                char guard = map[i][j];
                switch (map[i][j])
                {
                    case '^':
                        {
                            if (MoveUp(ref i, ref j))
                            {
                                validLoop = true;
                            }
                            break;
                        }
                    case '>':
                        {
                            if (MoveRight(ref i, ref j))
                            {
                                validLoop = true;
                            }
                            break;
                        }
                    case 'V':
                        {
                            if (MoveDown(ref i, ref j))
                            {
                                validLoop = true;
                            }
                            break;
                        }
                    case '<':
                        {
                            if (MoveLeft(ref i, ref j))
                            {
                                validLoop = true;
                            }
                            break;
                        }
                }



                if (validLoop)
                {
                    map.Clear();
                    for (int k = 0; k < data.Length; k++)
                    {
                        map.Add([.. data[k]]);
                    }

                    obstruction++;
                    places.Add($"{obstacleX}#{obstacleY}");
                    validLoop = false;
                    guardLeft = false;
                    i = guardPositon.pos1;
                    j = guardPositon.pos2;

                    turns = 0;
                    obstacleY++;

                    if (obstacleY == map[0].Count)
                    {
                        obstacleX++;
                        obstacleY = 0;
                    }
                }
                if (guardLeft)
                {
                    map.Clear();
                    for (int k = 0; k < data.Length; k++)
                    {
                        map.Add([.. data[k]]);
                    }

                    i = guardPositon.pos1;
                    j = guardPositon.pos2;
                    turns = 0;

                    validLoop = false;
                    guardLeft = false;
                    obstacleY++;
                    if (obstacleY == map[0].Count)
                    {
                        obstacleX++;
                        obstacleY = 0;
                    }
                }




                //Console.SetCursorPosition(0, 0);
                //for (int r = 0; r < map.Count; r++)
                //{
                //    for (int c = 0; c < map[r].Count; c++)
                //    {
                //        Console.Write(map[r][c]);
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine(); Console.WriteLine("obstructions: " + obstruction);
                //foreach (var item in places)
                //{
                //    Console.WriteLine(item);
                //}
                //Thread.Sleep(5);
                ;
            }


            ;
        }

        static bool MoveUp(ref int i, ref int j)
        {
            if (i - 1 < 0)
            {
                guardLeft = true;
                return false;
            }

            if (map[i - 1][j] == '.' || map[i - 1][j] == 'X')
            {
                map[i][j] = 'X';
                i--;

                //if (i - 1 >= 0 && map[i - 1][j] == 'O' && map[i][j + 1] == 'X')
                //{
                //    if (ValidateLoop(i, j, '>'))
                //    {
                //        return true;
                //    }
                //}
                turns++;
                if (turns > 7000)
                {
                    return true;
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
                guardLeft = true;
                return false;
            }

            if (map[i][j + 1] == '.' || map[i][j + 1] == 'X')
            {
                map[i][j] = 'X';
                j++;

                //if (j + 1 < map[0].Count && map[i][j + 1] == 'O' && map[i + 1][j] == 'X')
                //{
                //    if (ValidateLoop(i, j, 'V'))
                //    {
                //        return true;
                //    }
                //}

                map[i][j] = '>';
                turns++;
                if (turns > 7000)
                {
                    return true;
                }
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
                guardLeft = true;
                return false;
            }

            if (map[i + 1][j] == '.' || map[i + 1][j] == 'X')
            {
                map[i][j] = 'X';
                i++;

                //if (i + 1 < map.Count && map[i + 1][j] == 'O' && map[i][j - 1] == 'X')
                //{
                //    if (ValidateLoop(i, j, '<'))
                //    {
                //        return true;
                //    }
                //}

                map[i][j] = 'V';
                turns++;
                if (turns > 7000)
                {
                    return true;
                }
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
                guardLeft = true;
                return false;
            }

            if (map[i][j - 1] == '.' || map[i][j - 1] == 'X')
            {
                map[i][j] = 'X';
                j--;

                //if (j - 1 >= 0 && map[i][j - 1] == 'O' && map[i - 1][j] == 'X')
                //{
                //    if (ValidateLoop(i, j, '^'))
                //    {
                //        return true;
                //    }
                //}

                map[i][j] = '<';
                turns++;
                if (turns > 7000)
                {
                    return true;
                }
            }
            else
            {
                map[i][j] = '^';
            }

            return false;
        }


        static bool ValidateLoop(int i, int j, char direction)
        {
            int startPosX = i;
            int startPosY = j;

            int localTurns = 0;
            int Ohit = 0;
            while (i >= 0 || j >= 0 || i < map.Count || j < map[i].Count)
            {
                switch (direction)
                {
                    case '^':
                        {
                            if (i - 1 < 0)
                            {
                                return false;
                            }

                            if (map[i - 1][j] == 'X')
                            {
                                i--;
                            }
                            else
                            {
                                if (map[i - 1][j] == 'O')
                                {
                                    Ohit++;
                                }
                                localTurns++;
                                direction = '>';
                            }
                            break;
                        }
                    case '>':
                        {
                            if (j + 1 >= map[i].Count)
                            {
                                return false;
                            }

                            if (map[i][j + 1] == 'X')
                            {
                                j++;
                            }
                            else
                            {
                                if (map[i][j + 1] == 'O')
                                {
                                    Ohit++;
                                }
                                localTurns++;
                                direction = 'V';
                            }
                            break;
                        }
                    case 'V':
                        {
                            if (i + 1 >= map.Count)
                            {
                                return false;
                            }

                            if (map[i + 1][j] == 'X')
                            {
                                i++;
                            }
                            else
                            {
                                if (map[i + 1][j] == 'O')
                                {
                                    Ohit++;
                                }
                                localTurns++;
                                direction = '<';
                            }
                            break;
                        }
                    case '<':
                        {
                            if (j - 1 < 0)
                            {
                                return false;
                            }

                            if (map[i][j - 1] == 'X')
                            {
                                j--;
                            }
                            else
                            {
                                if (map[i][j - 1] == 'O')
                                {
                                    Ohit++;
                                }
                                localTurns++;
                                direction = '^';
                            }
                            break;
                        }
                    default:
                        break;
                }

                if (Ohit > 20)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
