namespace HackerNewsAPI.Domain;

public class Story
{
    public string? By { get; set; }
    public int Descendants { get; set; }
    public int Id { get; set; }
    public IEnumerable<int>? Kids { get; set; } = null;
    public int Score { get; set; }
    public int Time { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public string? Url { get; set; }
}

/*
 Domain models reflect the models returned by the source data API of Hacker News API.
 These models, instead of being classes, could just as well be record types.
 */