using AdventOfCode_Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode_08
{
    internal class Program
    {
        static List<List<char>> data = [];
        static void Main(string[] args)
        {
            data = Helper.ReadFileCharMatrixList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Puzzle.txt"));

            Dictionary<char, List<(int i, int j)>> antennas = [];
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    if (data[i][j] == '.')
                    {
                        continue;
                    }

                    if (antennas.ContainsKey(data[i][j]))
                    {
                        antennas[data[i][j]].Add((i, j));
                    }
                    else
                    {
                        antennas.Add(data[i][j], [(i, j)]);
                    }
                }

            }

            List<(int pos1, int pos2)> antinodes = [];
            foreach (var antenna in antennas)
            {
                int s = 0;
                while (s < antenna.Value.Count - 1)
                {
                    List<List<char>> specificAntennas = SetUpMap(antenna.Key);

                    //(int i, int j) = antenna.Value[s];
                    for (int k = s + 1; k < antenna.Value.Count; k++)
                    {
                        int distanceI = antenna.Value[k].i - antenna.Value[s].i;
                        int distanceJ = antenna.Value[k].j - antenna.Value[s].j;

                        int x = antenna.Value[s].i - distanceI;
                        int y = antenna.Value[s].j - distanceJ;

                        while (x < specificAntennas.Count && x >= 0 && y < specificAntennas[0].Count && y >= 0)
                        {
                            if (!antinodes.Contains((x, y)))
                            {
                                antinodes.Add((x, y));
                                if (data[x][y] != '#' && !antennas.ContainsKey(data[x][y]))
                                {
                                    data[x][y] = '#'; 
                                }
                            }

                            x -= distanceI;
                            y -= distanceJ;
                        }


                        x = antenna.Value[k].i + distanceI;
                        y = antenna.Value[k].j + distanceJ;

                        while (x < specificAntennas.Count && x >= 0 && y < specificAntennas[0].Count && y >= 0)
                        {
                            if (!antinodes.Contains((x, y)))
                            {
                                antinodes.Add((x, y));
                                if (data[x][y] != '#' && !antennas.ContainsKey(data[x][y]))
                                {
                                    data[x][y] = '#'; 
                                }
                            }

                            x += distanceI;
                            y += distanceJ;
                        }

                    }

                    s++;
                }
                
            }
            Console.SetCursorPosition(0, 0);
            int count = 0;
            foreach (var row in data)
            {
                foreach (var col in row)
                {
                    if (col == '#')
                    {
                        count++;
                    }
                    Console.Write(col);
                }
                Console.WriteLine();
            }

            foreach (var item in antennas)
            {
                count += item.Value.Count;
            }

            ;
        }

        static List<List<char>> SetUpMap(char antenna)
        {
            List<List<char>> result = [];
            for (int i = 0; i < data.Count; i++)
            {
                result.Add([]);
                for (int j = 0; j < data[i].Count; j++)
                {
                    if (!antenna.Equals(data[i][j]))
                    {
                        result[i].Add('.');
                    }
                    else
                    {
                        result[i].Add(data[i][j]);
                    }
                }
            }

            return result;
        }
    }
}
