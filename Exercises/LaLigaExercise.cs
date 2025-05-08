public class Match
{
    public string Team1;
    public string Team2;
    public int Score1;
    public int Score2;

    public Match(string team1, string team2, int score1, int score2)
    {
        Team1 = team1;
        Team2 = team2;
        Score1 = score1;
        Score2 = score2;
    }
}

public class LaLiga
{
    private List<Match> matches = new List<Match>();

    public void AddMatch(string team1, string team2, int score1, int score2)
    {
        matches.Add(new Match(team1, team2, score1, score2));
    }

    public void GetDataFromFile()
    {
        // Implement file reading logic here
        File.ReadAllLines("data/laliga.txt").ToList().ForEach(line =>
        {
            // First separated by space, then by - and finally by space again
            // Example: "Real Madrid 2-1 Barcelona"
            // After split: ["Real Madrid", "2", "1", "Barcelona"]
            string[] parts = line.Split('-');
            string[] team1Parts = parts[0].Trim().Split(' ');
            string[] team2Parts = parts[1].Trim().Split(' ');
            int score1 = int.Parse(team1Parts[team1Parts.Length - 1].Trim());
            int score2 = int.Parse(team2Parts[0].Trim());
            string team1 = string.Join(" ", team1Parts.Take(team1Parts.Length - 1));
            string team2 = string.Join(" ", team2Parts.Skip(1).Take(team2Parts.Length - 1));

            AddMatch(team1, team2, score1, score2);
        });

    }

    public int getTotalGoals()
    {
        Console.WriteLine($"Total matches: {matches.Count}");


        var partidosUnicos = matches
            .GroupBy(p => new
            {
                p.Team1,
                p.Team2,
                p.Score1,
                p.Score2
            })
            .Select(g => g.First())
            .ToList();
        
        Console.WriteLine($"Total unique matches: {partidosUnicos.Count}");

        int TotalPoints = 0;
        foreach (var partido in partidosUnicos)
        {
            if (partido.Score1 == partido.Score2)
            {
                TotalPoints += 2;
            }
            else if (partido.Score1 != partido.Score2)
            {
                TotalPoints += 3;
            }
        }

        return TotalPoints;
    }


}
