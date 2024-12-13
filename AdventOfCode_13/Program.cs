using AdventOfCode_Helper;

namespace AdventOfCode_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> data = Helper.ReadFileStringList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));

            List<Game> games = [];
            for (int i = 0; i < data.Count; i += 4)
            {
                Button buttonA = new()
                {
                    Name = data[i].Split(':')[0],
                    X = int.Parse(data[i].Split(':')[1].Split(',')[0].Replace(" X+", "")),
                    Y = int.Parse(data[i].Split(':')[1].Split(',')[1].Replace(" Y+", ""))
                };

                Button buttonB = new()
                {
                    Name = data[i + 1].Split(':')[0],
                    X = int.Parse(data[i + 1].Split(':')[1].Split(',')[0].Replace(" X+", "")),
                    Y = int.Parse(data[i + 1].Split(':')[1].Split(',')[1].Replace(" Y+", ""))
                };

                int x = int.Parse(data[i + 2].Split(':')[1].Split(',')[0].Replace(" X=",""));
                int y = int.Parse(data[i + 2].Split(':')[1].Split(',')[1].Replace(" Y=",""));

                games.Add(new()
                {
                    ButtonA = buttonA,
                    ButtonB = buttonB,
                    Prize = (x, y)
                });
            }
        }
    }

    public class Button
    {
        public string Name { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Game
    {
        public Button ButtonA { get; set; }
        public Button ButtonB { get; set; }
        public (int x, int y) Prize { get; set; }
    }
}
