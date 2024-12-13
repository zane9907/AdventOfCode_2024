using AdventOfCode_Helper;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode12
{
    internal class Program
    {
        static List<List<char>> map = [];
        static void Main(string[] args)
        {
            map = Helper.ReadFileCharMatrixList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));


            List<Plot> garden = [];

            int i = 0;
            int j = 0;
            while (i < map.Count)
            {
                while (j < map[i].Count)
                {
                    if (!garden.Any(x => x.Region.Contains((i, j))))
                    {
                        List<(int x, int y)> region = [];
                        AddRegion(region, i, j, map[i][j]);

                        garden.Add(new Plot()
                        {
                            ID = map[i][j],
                            Region = region,
                            Area = region.Count,
                            Perimeter = CalculatePerimeter(region, map[i][j]),
                            Sides = CalculateSides(region, map[i][j])
                        });
                    }

                    j++;
                }

                j = 0;
                i++;
            }

            int price = 0;
            foreach (Plot plot in garden)
            {
                price += plot.Area * plot.Perimeter;
            }

            int price2 = 0;
            foreach (Plot plot in garden)
            {
                price2 += plot.Area * plot.Sides;
            }

        }

        static int CalculateSides(List<(int x, int y)> region, char id)
        {
            int sides = 0;


            Dictionary<string, List<(int x, int y)>> directionSide = new()
            {
                {"UP", [] },
                {"RIGHT", [] },
                {"DOWN", [] },
                {"LEFT", [] }
            };

            List<int> rows = region.Select(pos => pos.x).Distinct().OrderBy(x => x).ToList();
             
            foreach (var rowNumber in rows)
            {
                List<(int x, int y)> row = region.Where(pos => pos.x == rowNumber)
                                                 .OrderBy(pos => pos.y)
                                                 .ToList();

                for (int i = 0; i < row.Count; i++)
                {
                    //UP
                    if (row[i].x - 1 < 0 || map[row[i].x - 1][row[i].y] != id)
                    {
                        var positions = directionSide["UP"];
                        List<(int x, int y)> lastSide = positions.Where(pos => pos.x == row[i].x - 1).ToList();
                        if (lastSide.Count == 0)
                        {
                            lastSide.Add((-99999, -99999));
                        }

                        if (positions.Count == 0
                            || row[i].y - lastSide.Last().y > 1)
                        {
                            positions.Add((row[i].x - 1, row[i].y));
                        }
                        else
                        {
                            var index = positions.FindIndex(x => x == lastSide[^1]);
                            positions[index] = (row[i].x - 1, row[i].y);
                        }
                    }
                    //RIGHT
                    if (row[i].y + 1 >= map[0].Count || map[row[i].x][row[i].y + 1] != id)
                    {
                        var positions = directionSide["RIGHT"];
                        List<(int x, int y)> lastSide = positions.Where(pos => pos.y == row[i].y + 1).ToList();
                        if (lastSide.Count == 0)
                        {
                            lastSide.Add((-99999, -99999));
                        }

                        if (positions.Count == 0
                            || row[i].x - lastSide.Last().x > 1)
                        {
                            positions.Add((row[i].x, row[i].y + 1));
                        }
                        else
                        {
                            var index = positions.FindIndex(x => x == lastSide[^1]);
                            positions[index] = (row[i].x, row[i].y + 1);
                        }

                    }
                    //DOWN
                    if (row[i].x + 1 >= map[0].Count || map[row[i].x + 1][row[i].y] != id)
                    {
                        var positions = directionSide["DOWN"];
                        List<(int x, int y)> lastSide = positions.Where(pos => pos.x == row[i].x + 1).ToList();
                        if (lastSide.Count == 0)
                        {
                            lastSide.Add((-99999, -99999));
                        }

                        if (positions.Count == 0 
                            || row[i].y - lastSide.Last().y > 1)
                        {
                            positions.Add((row[i].x + 1, row[i].y));
                        }
                        else
                        {
                            var index = positions.FindIndex(x => x == lastSide[^1]);
                            positions[index] = (row[i].x + 1, row[i].y);
                        }
                    }
                    //LEFT
                    if (row[i].y - 1 < 0 || map[row[i].x][row[i].y - 1] != id)
                    {
                        var positions = directionSide["LEFT"];
                        List<(int x, int y)> lastSide = positions.Where(pos => pos.y == row[i].y - 1).ToList();
                        if (lastSide.Count == 0)
                        {
                            lastSide.Add((-99999,-99999));
                        }

                        if (positions.Count == 0
                            || row[i].x - lastSide[^1].x > 1)
                        {
                            positions.Add((row[i].x, row[i].y - 1));
                        }
                        else
                        {
                            var index = positions.FindIndex(x => x == lastSide[^1]);
                            positions[index] = (row[i].x, row[i].y - 1);
                        }
                    }


                }
            }

            foreach (var item in directionSide)
            {
                sides += item.Value.Count;
            }


            return directionSide.Sum(x => x.Value.Count);
        }

        static int CalculatePerimeter(List<(int x, int y)> region, char id)
        {
            int result = 0;
            foreach (var pos in region)
            {
                //UP
                if (pos.x - 1 < 0 || map[pos.x - 1][pos.y] != id)
                {
                    result++;
                }

                //DOWN
                if (pos.x + 1 >= map.Count || map[pos.x + 1][pos.y] != id)
                {
                    result++;
                }

                //LEFT
                if (pos.y - 1 < 0 || map[pos.x][pos.y - 1] != id)
                {
                    result++;
                }

                //RIGHT
                if (pos.y + 1 >= map.Count || map[pos.x][pos.y + 1] != id)
                {
                    result++;
                }
            }

            return result;
        }



        static void AddRegion(List<(int x, int y)> result, int i, int j, char id)
        {
            if (!result.Contains((i, j)) && map[i][j] == id)
            {
                result.Add((i, j));
            }
            else
            {
                return;
            }

            //UP
            if (i - 1 >= 0 && map[i - 1][j] == id)
            {
                AddRegion(result, i - 1, j, id);
            }

            //DOWN
            if (i + 1 < map.Count && map[i + 1][j] == id)
            {
                AddRegion(result, i + 1, j, id);
            }

            //LEFT
            if (j - 1 >= 0 && map[i][j - 1] == id)
            {
                AddRegion(result, i, j - 1, id);
            }

            //RIGHT
            if (j + 1 < map.Count && map[i][j + 1] == id)
            {
                AddRegion(result, i, j + 1, id);
            }
        }
    }


    public class Plot
    {
        public char ID { get; set; }
        public List<(int x, int y)> Region { get; set; } = [];
        public int Area { get; set; }
        public int Perimeter { get; set; }
        public int Sides { get; set; }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
