namespace HackerNewsAPI.Dto;

public class StoryDTO
{
    public string? Title { get; set; }
    public string? Uri { get; set; }
    public string? PostedBy { get; set; }
    public DateTime Time { get; set; }
    public int Score { get; set; }
    public int CommentCount { get; set; }
}

/*
 DTO models are models returned to the user interface/API responses.
 */