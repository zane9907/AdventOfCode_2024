using AdventOfCode_Helper;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode_13
{
    internal class Program
    {
        static void test()
        {
            long[] totals = new long[2];
            string[] lines = Helper.ReadFileString(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt")).Split(Environment.NewLine)
            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var regexButton = new Regex(@"Button (?:A|B): X\+(\d+), Y\+(\d+)");

            Vector2 Match(Regex regex, string line)
            {
                var g = regex.Match(line).Groups;
                return new(int.Parse(g[1].Value), int.Parse(g[2].Value));
            }

            for (int j = 0; j < 2; j++)
                for (int i = 0; i < lines.Length; i += 3)
                {   // Good ol' algebra
                    Vector2 a = Match(regexButton, lines[i]);
                    Vector2 b = Match(regexButton, lines[i + 1]);
                    var pr = Match(new(@"Prize: X=(\d+), Y=(\d+)"), lines[i + 2]);
                    long[] p = [(long)pr.X, (long)pr.Y];
                    p[0] = j == 1 ? /*Part 2*/ p[0] + 10000000000000 : p[0];
                    p[1] = j == 1 ? /*Part 2*/ p[1] + 10000000000000 : p[1];
                    double y = (p[1] * (double)a.X - p[0] * (double)a.Y)
                        / (b.Y * (double)a.X - b.X * (double)a.Y);
                    double x = (p[0] - y * b.X) / a.X;
                    if (x % 1 != 0 || y % 1 != 0) continue; // Not an int
                    totals[j] += (long)x * 3 + (long)y;
                }

            ;
        }


        static List<Game> games = [];
        static object LockObject = new object();

        static void Main(string[] args)
        {

            //test();
            List<string> data = Helper.ReadFileStringList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));

            games = [];
            int gameCount = 1;
            for (int i = 0; i < data.Count; i += 4)
            {
                Button buttonA = new()
                {
                    Name = data[i].Split(':')[0][^1],
                    X = int.Parse(data[i].Split(':')[1].Split(',')[0].Replace(" X+", "")),
                    Y = int.Parse(data[i].Split(':')[1].Split(',')[1].Replace(" Y+", ""))
                };

                Button buttonB = new()
                {
                    Name = data[i + 1].Split(':')[0][^1],
                    X = int.Parse(data[i + 1].Split(':')[1].Split(',')[0].Replace(" X+", "")),
                    Y = int.Parse(data[i + 1].Split(':')[1].Split(',')[1].Replace(" Y+", ""))
                };

                long x = int.Parse(data[i + 2].Split(':')[1].Split(',')[0].Replace(" X=", "")) + 10000000000000;
                long y = int.Parse(data[i + 2].Split(':')[1].Split(',')[1].Replace(" Y=", "")) + 10000000000000;

                games.Add(new()
                {
                    ID = gameCount++,
                    ButtonA = buttonA,
                    ButtonB = buttonB,
                    Buttons = [buttonA, buttonB],
                    Prize = (x, y)
                });
            }

            long tokens = 0;

            foreach (var game in games)
            {
                Console.WriteLine($"Game {game.ID} started!");
                long count = CalculateTokens(game);
                tokens += count;
                Console.WriteLine($"Game {game.ID} ENDED -------- {count}");

            }

            //Parallel.ForEach(games, game =>
            //{
            //    Console.WriteLine($"Game {game.ID} started!");
            //    int count = CalculateTokens(game);
            //    lock (LockObject)
            //    {
            //        tokens += count;
            //        Console.WriteLine($"Game {game.ID} ENDED");
            //    }
            //});


            ;


            //Console.Clear();
            //for (int i = 0; i < mapX.GetLength(0); i++)
            //{
            //    for (int j = 0; j < mapX.GetLength(1); j++)
            //    {
            //        Console.Write(mapX[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }

        static long CalculateTokens(Game game)
        {
            long[,] mapX = new long[10001, 10001];
            for (long i = 0; i < mapX.GetLength(0); i++)
            {
                for (long j = 0; j < mapX.GetLength(1); j++)
                {
                    //if (i == j)
                    //{
                    //    mapX[i, j] = 0;
                    //    continue;
                    //}

                    mapX[i, j] = game.ButtonA.X * j + game.ButtonB.X * i;
                }
            }
            
            List<(long x, long y)> Xindexes = [];
            for (long i = 0; i < mapX.GetLength(0); i++)
            {
                for (long j = 0; j < mapX.GetLength(1); j++)
                {
                    if (mapX[i, j] == game.Prize.x)
                    {
                        Xindexes.Add((i, j));
                    }

                    //if (mapX[i, j] > game.Prize.x)
                    //{
                    //    j = 0;
                    //    break;
                    //}
                }
            }


            ;

            long[,] mapY = new long[10001, 10001];
            for (long i = 0; i < mapY.GetLength(0); i++)
            {
                for (long j = 0; j < mapY.GetLength(1); j++)
                {
                    //if (i == j)
                    //{
                    //    mapY[i, j] = 0;
                    //    continue;
                    //}

                    mapY[i, j] = game.ButtonA.Y * j + game.ButtonB.Y * i;
                }
            }

            List<(long x, long y)> Yindexes = [];
            for (long i = 0; i < mapY.GetLength(0); i++)
            {
                for (long j = 0; j < mapY.GetLength(1); j++)
                {
                    if (mapY[i, j] == game.Prize.y)
                    {
                        Yindexes.Add((i, j));
                    }

                    //if (mapY[i, j] > game.Prize.y)
                    //{
                    //    j = 0;
                    //    break;
                    //}
                }
            }

            (long x, long y) result = (0, 0);
            foreach (var item in Xindexes)
            {
                if (Yindexes.Contains(item))
                {
                    result = item;
                    break;
                }
            }

            return 3 * result.y + result.x;
        }


        public class Button
        {
            public char Name { get; set; }

            public int X { get; set; }
            public int Y { get; set; }
        }

        public class Game
        {
            public int ID { get; set; }
            public Button ButtonA { get; set; }
            public Button ButtonB { get; set; }
            public List<Button> Buttons { get; set; } = [];
            public (long x, long y) Prize { get; set; }
        }
    }
}
