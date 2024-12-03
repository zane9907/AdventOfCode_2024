using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt");

        string data = File.ReadAllText(path);

        string pattern = @"mul\(\d{1,3},\d{1,3}\)";
        Regex rg = new(pattern);

        var matches = rg.Matches(data);

        int sum = 0;
        foreach (Match item in matches)
        {
            List<int> numbers = item.Value.Replace("mul(", "").Replace(")", "").Split(',').ToList().ConvertAll(int.Parse);
            sum += numbers[0] * numbers[1];
        }


        //Part 2

        Regex rgDo = new(@"do(?:n't)?\(\)");

        var doMatches = rgDo.Matches(data);

        sum = 0;
        List<int> firstnumbers = matches[0].Value.Replace("mul(", "").Replace(")", "").Split(',').ToList().ConvertAll(int.Parse);
        sum += firstnumbers[0] * firstnumbers[1];


        for (int i = 1; i < matches.Count; i++)
        {
            Match? latestDo = FindMostRecentDo(doMatches, matches[i]);

            if (latestDo != null && latestDo.Value.Equals("don't()"))
            {
                continue;
            }

            List<int> numbers = matches[i].Value.Replace("mul(", "").Replace(")", "").Split(',').ToList().ConvertAll(int.Parse);
            sum += numbers[0] * numbers[1];
        }

        ;
    }

    static Match? FindMostRecentDo(MatchCollection doMatches, Match mul)
    {
        Match? latest = null;
        foreach (Match item in doMatches)
        {
            if (item.Index > mul.Index)
            {
                return latest;
            }

            latest = item;
        }

        return latest;
    }
}