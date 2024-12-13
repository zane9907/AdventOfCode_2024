using AdventOfCode_Helper;

namespace AdventOfCode_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> data = Helper.ReadFileStringList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));

            List<Game> games = [];
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

                int x = int.Parse(data[i + 2].Split(':')[1].Split(',')[0].Replace(" X=", ""));
                int y = int.Parse(data[i + 2].Split(':')[1].Split(',')[1].Replace(" Y=", ""));

                games.Add(new()
                {
                    ID = gameCount++,
                    Buttons = [buttonA, buttonB],
                    Prize = (x, y)
                });
            }

            int tokens = 0;
            //foreach (Game game in games)
            //{
            //    Console.WriteLine(gameCount);
            //    List<Button> pressedButtons = [];
            //    List<Button> optimal = [];
            //    bool found = false;
            //    Backtrack(0, ref pressedButtons, ref optimal, game, ref found);

            //    tokens += CalculateTokens(optimal);
            //    gameCount++;
            //}

            Parallel.ForEach(games, game =>
            {
                Console.WriteLine(game.ID); 
                
                List<Button> pressedButtons = [];
                List<Button> optimal = [];
                bool found = false;
                Backtrack(0, ref pressedButtons, ref optimal, game, ref found);

                lock (lockobject)
                {
                    tokens += CalculateTokens(optimal);
                    Console.WriteLine();
                    Console.WriteLine($"Game {game.ID} DONE");
                }
            });


        }

        static object lockobject = new object();

        static void Backtrack(int press, ref List<Button> pressedButtons, ref List<Button> optimal, Game game,ref bool found)
        {
            int i = 0;
            while (!found && i < 2)
            {
                if (Ft(pressedButtons))
                {
                    if (Fk(press, game, game.Buttons[i], pressedButtons))
                    {
                        if (pressedButtons.Count <= press)
                        {
                            pressedButtons.Add(game.Buttons[i]);
                        }
                        else
                        {
                            pressedButtons[press] = game.Buttons[i];
                        }

                        if (pressedButtons.Sum(button => button.X) == game.Prize.x && pressedButtons.Sum(button => button.Y) == game.Prize.y)
                        {
                            if (CalculateTokens(pressedButtons) > CalculateTokens(optimal))
                            {
                                optimal = new(pressedButtons);
                                found = true;
                            }
                        }
                        else
                        {
                            Backtrack(press + 1, ref pressedButtons, ref optimal, game,ref found);
                        }
                    } 
                }
                i++;
            }

            if (press == pressedButtons.Count - 1)
            {
                pressedButtons.RemoveAt(press);
            }
        }

        static int CalculateTokens(List<Button> pressedButtons)
        {
            int tokens = 0;
            foreach (Button button in pressedButtons)
            {
                if (button.Name.Equals('A'))
                {
                    tokens += 3;
                }
                else
                {
                    tokens++;
                }
            }

            return tokens;
        }

        static bool Ft(List<Button> pressedButtons)
        {
            return pressedButtons.Count(x => x.Name.Equals('A')) <= 100 && pressedButtons.Count(x => x.Name.Equals('B')) <= 100;
        }

        static bool Fk(int press, Game game, Button button, List<Button> pressedButtons)
        {
            int sumX = 0;
            int sumY = 0;
            for (int i = 0; i < press; i++)
            {
                sumX += pressedButtons[i].X;
                sumY += pressedButtons[i].Y;
            }

            return sumX + button.X <= game.Prize.x && sumY + button.Y <= game.Prize.y;
        }
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
        public List<Button> Buttons { get; set; }
        public (int x, int y) Prize { get; set; }
    }
}
