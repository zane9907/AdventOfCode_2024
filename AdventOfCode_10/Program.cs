using AdventOfCode_Helper;

namespace AdventOfCode_10
{
    internal class Program
    {
        static List<List<int>> map = [];

        static void Main(string[] args)
        {
            map = Helper.ReadFileIntMatrix(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));

            List<(int x, int y)> zeroPos = [];
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == 0)
                    {
                        zeroPos.Add((i, j));
                    }
                }
            }

            int sum = 0;
            int ratingSum = 0;
            foreach (var trailhead in zeroPos)
            {
                List<(int x, int y)> peaks = [];
                int rating = 0;

                Move(ref peaks, ref rating, trailhead.x, trailhead.y, 0);

                sum += peaks.Count;
                ratingSum += rating;
            }


            ;
        }

        static void Move(ref List<(int x, int y)> peaks, ref int rating, int x, int y, int height)
        {
            if (map[x][y] == 9)
            {
                if (!peaks.Contains((x, y)))
                {
                    peaks.Add((x, y));
                }
                rating++;

                return;
            }

            if (CanMoveUp(x, y, height))
            {
                Move(ref peaks, ref rating, x - 1, y, height + 1);
            }
            if (CanMoveRight(x, y, height))
            {
                Move(ref peaks, ref rating, x, y + 1, height + 1);
            }
            if (CanMoveDown(x, y, height))
            {
                Move(ref peaks, ref rating, x + 1, y, height + 1);
            }
            if (CanMoveLeft(x, y, height))
            {
                Move(ref peaks, ref rating, x, y - 1, height + 1);
            }

        }

        static bool CanMoveUp(int x, int y, int height)
        {
            if (x - 1 >= 0 && map[x - 1][y] - height == 1)
            {
                return true;
            }

            return false;
        }

        static bool CanMoveRight(int x, int y, int height)
        {
            if (y + 1 < map[0].Count && map[x][y + 1] - height == 1)
            {
                return true;
            }

            return false;
        }

        static bool CanMoveDown(int x, int y, int height)
        {
            if (x + 1 < map.Count && map[x + 1][y] - height == 1)
            {
                return true;
            }

            return false;
        }
        static bool CanMoveLeft(int x, int y, int height)
        {
            if (y - 1 >= 0 && map[x][y - 1] - height == 1)
            {
                return true;
            }

            return false;
        }
    }
}
