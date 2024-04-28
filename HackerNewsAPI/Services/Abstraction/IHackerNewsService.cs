using HackerNewsAPI.Dto;

namespace HackerNewsAPI.Services.Abstraction;

public interface IHackerNewsService
{
    public Task<IEnumerable<StoryDTO>> GetBestStories(int limit);
}
