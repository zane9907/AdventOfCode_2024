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
            List<int> rows = region.Select(pos => pos.x).Distinct().ToList();
            for (int r = 0; r < rows.Count - 1; r++)
            {
                int item = rows[r];
                List<(int x, int y)> row = region.Where(pos => pos.x == item)
                                                 .OrderBy(pos => pos.y)
                                                 .ToList();

                bool isConsistent1 = true;
                for (int i = 0; i < row.Count - 1; i++)
                {
                    if (row[i + 1].y - row[i].y != 1)
                    {
                        isConsistent1 = false;
                        break;
                    }
                }

                //Delete consistent
                if (isConsistent1)
                {
                    var first = row[0];
                    var last = row[^1];
                    //UP first
                    if (first.x - 1 < 0 || map[first.x - 1][first.y] != id)
                    {
                        sides++;
                    }
                    //UP Last
                    else if (last.x - 1 < 0 || map[last.x - 1][last.y] != id)
                    {
                        sides++;
                    }

                    //DOWN first
                    if (first.x + 1 >= map.Count || map[first.x + 1][first.y] != id)
                    {
                        sides++;
                    }
                    //LEFT first
                    if (r == 0 || r - 1 >= 0 && region.Where(pos => pos.x == rows[r - 1]).OrderBy(pos => pos.y).First().y != first.y)
                    {
                        if (first.y - 1 < 0 || map[first.x][first.y - 1] != id)
                        {
                            sides++;
                        }
                    }
                    //RIGHT last
                    if (r == 0 || r - 1 >= 0 && region.Where(pos => pos.x == rows[r - 1]).OrderBy(pos => pos.y).Last().y != last.y)
                    {
                        if (last.y + 1 >= map[0].Count || map[last.x][last.y + 1] != id)
                        {
                            sides++;
                        }
                    }
                    //DOWN last
                    if (last.x + 1 >= map.Count || map[last.x + 1][last.y] != id)
                    {
                        sides++;
                    }
                }
                else
                {
                    var first = row[0];
                    var last = row[^1];
                    //DOWN first
                    if (first.x + 1 >= map.Count || map[first.x + 1][first.y] != id)
                    {
                        sides++;
                    }

                    //DOWN last
                    if (last.x + 1 >= map.Count || map[last.x + 1][last.y] != id)
                    {
                        sides++;
                    }

                    for (int o = 0; o < row.Count; o++)
                    {
                        (int x, int y) pos = row[o];
                        //UP
                        if (pos.x - 1 < 0 || map[pos.x - 1][pos.y] != id)
                        {
                            sides++;
                        }
                        //LEFT
                        if (r == 0 || r - 1 >= 0 && region.Where(pos => pos.x == rows[r - 1]).OrderBy(pos => pos.y).First().y != pos.y)
                        {
                            if (pos.y - 1 < 0 || map[pos.x][pos.y - 1] != id)
                            {
                                sides++;
                            }
                        }
                        //RIGHT
                        if (r == 0 || r - 1 >= 0 && region.Where(pos => pos.x == rows[r - 1]).OrderBy(pos => pos.y).Last().y != pos.y)
                        {
                            if (pos.y + 1 >= map[0].Count || map[pos.x][pos.y + 1] != id)
                            {
                                sides++;
                            }
                        }
                    }
                }
            }

            List<(int x, int y)> lastRow = region.Where(pos => pos.x == rows[^1]).ToList();
            bool isConsistent = true;
            for (int i = 0; i < lastRow.Count - 1; i++)
            {
                if (lastRow[i + 1].y - lastRow[i].y != 1)
                {
                    isConsistent = false;
                    break;
                }
            }

            if (isConsistent)
            {
                var first = lastRow[0];
                var last = lastRow[^1];
                //UP first
                if (first.x - 1 < 0 || map[first.x - 1][first.y] != id)
                {
                    sides++;
                }
                //DOWN first
                if (first.x + 1 >= map.Count || map[first.x + 1][first.y] != id)
                {
                    sides++;
                }
                //LEFT first
                if (rows.Count == 1 || region.Where(pos => pos.x == rows[rows.Count - 2]).OrderBy(pos => pos.y).First().y != first.y)
                {
                    if (first.y - 1 < 0 || map[first.x][first.y - 1] != id)
                    {
                        sides++;
                    }
                }
                //RIGHT last
                if (rows.Count == 1 || region.Where(pos => pos.x == rows[rows.Count - 2]).OrderBy(pos => pos.y).Last().y != last.y)
                {
                    if (last.y + 1 >= map[0].Count || map[last.x][last.y + 1] != id)
                    {
                        sides++;
                    }
                }
            }
            else
            {
                foreach (var item in lastRow)
                {
                    //UP
                    if (item.x - 1 < 0 || map[item.x - 1][item.y] != id)
                    {
                        sides++;
                    }
                    //DOWN
                    if (item.x + 1 >= map.Count || map[item.x + 1][item.y] != id)
                    {
                        sides++;
                    }
                    //LEFT
                    if (rows.Count == 1 || region.Where(pos => pos.x == rows[rows.Count - 2]).OrderBy(pos => pos.y).First().y != item.y)
                    {
                        if (item.y - 1 < 0 || map[item.x][item.y - 1] != id)
                        {
                            sides++;
                        }
                    }
                    //RIGHT
                    if (rows.Count == 1 || region.Where(pos => pos.x == rows[rows.Count - 2]).OrderBy(pos => pos.y).Last().y != item.y)
                    {
                        if (item.y + 1 >= map[0].Count || map[item.x][item.y + 1] != id)
                        {
                            sides++;
                        }
                    }
                }
            }



            return sides;
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
