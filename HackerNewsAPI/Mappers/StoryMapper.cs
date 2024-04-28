using HackerNewsAPI.Domain;
using HackerNewsAPI.Dto;

namespace HackerNewsAPI.Mappers;

public static class StoryMapper
{
    public static StoryDTO MapToDto(this Story story)
    {
        return new()
        {
            Title = story.Title,
            Uri = story.Url,
            Score = story.Score,
            PostedBy = story.By,
            CommentCount = story.Descendants,
            Time = new DateTime(story.Time)
        };
    }
}

/*
 I considered using a library like AutoMapper; however, manual mapping is faster,
 but it requires manual writing even when field names match. 
 Additionally, such mappings need to be written in two directions: .Map() and .ReverseMap().
*/