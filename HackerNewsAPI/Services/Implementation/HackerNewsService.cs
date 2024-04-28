using HackerNewsAPI.Domain;
using HackerNewsAPI.Dto;
using HackerNewsAPI.Mappers;
using HackerNewsAPI.Services.Abstraction;

namespace HackerNewsAPI.Services.Implementation;

public class HackerNewsService : IHackerNewsService
{
    readonly HttpClient _httpClient;

    public HackerNewsService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<IEnumerable<StoryDTO>> GetBestStories(int limit)
    {
        if(limit <= 0) 
            return [];

        IReadOnlyCollection<int>? bestStoriesIds = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<int>>("beststories.json");

        if(!bestStoriesIds.Any()) 
            return [];

        IEnumerable<Task<Story>> tasks =
        from id in bestStoriesIds?.Take(limit)
        select _httpClient.GetFromJsonAsync<Story>($"item/{id}.json");

        return await Task.WhenAll(tasks.Select(async task =>
        {
            var story = await task;
            return story.MapToDto();
        }));
    }
}

/*
 Service returning a list of top stories. First, I fetch the identifiers of the top stories. 
 Then, I create a list of tasks to retrieve details for each story.
 Subsequently, the data is fetched, mapped, and returned. 
 I considered throwing an exception if the user requests more stories than available in 
 the data source, but I decided to simply return the maximum available in such cases. 
 It's worth considering whether there should be a field indicating this fact.
 */