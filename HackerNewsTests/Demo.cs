using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using HackerNewsAPI.Domain;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace HackerNewsTests;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class Demo
{
    //[Benchmark]
    //public async Task<List<int>> GetAsString()
    //{
    //    using HttpClient client = new() { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") };
    //    var reposne = await client.GetStringAsync("beststories.json");
    //    List<int>? ids = JsonSerializer.Deserialize(reposne, typeof(List<int>)) as List<int>;
    //    return ids.Take(5).ToList();
    //}

    //[Benchmark]
    //public async Task<List<int>> GetFromJson()
    //{
    //    using HttpClient client = new() { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") };
    //    List<int>? bestStoriesIds = await client.GetFromJsonAsync<List<int>>("beststories.json");

    //    return bestStoriesIds.Take(5).ToList();
    //}

    //[Benchmark]
    //public async Task<IReadOnlyCollection<int>> ReadAsStream()
    //{
    //    var cancelationToken = new CancellationToken();
    //    var request = CreateRequest();
    //    using HttpClient client = new() { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") };
    //    using var result = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancelationToken).ConfigureAwait(false);
    //    using var contentStream = await result.Content.ReadAsStreamAsync();
    //    var data = await JsonSerializer.DeserializeAsync<List<int>>(contentStream);

    //    return data.Take(5).ToList();
    //}

    //private static HttpRequestMessage CreateRequest()
    //{
    //    return new HttpRequestMessage(HttpMethod.Get, "beststories.json");
    //}

    [Benchmark]
    public async Task<Story[]> GetBestStories()
    {
        var limit = 200;
        using HttpClient client = new() { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") };
        var result2 = new List<Story>();
        IReadOnlyCollection<int>? bestStoriesIds = await client.GetFromJsonAsync<IReadOnlyCollection<int>>("beststories.json");

        List<Task<HttpResponseMessage>> tasks = new();
        bestStoriesIds?.Take(limit)
                       .ToList()
                       .ForEach((id) => 
                       {
                           var request = CreateRequest(id.ToString());
                           tasks.Add(client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)); 
                       });


        var stories = await Task.WhenAll(tasks.Select(async task =>
        {
            var message = await task;
            using var contentStream = await message.Content.ReadAsStreamAsync();
            var story = await JsonSerializer.DeserializeAsync<StoryDTO>(contentStream);
            return new Story
            {
                Title = story.Title,
                Uri = story.Url,
                Score = story.Score,
                PostedBy = story.By,
                CommentCount = story.Kids?.Count() ?? default,
                Time = new DateTime(story.Time)
            };
        }));

        return stories;
    }


    //public async Task<int> GetRepositories(CancellationToken cancellationToken)
    //{
    //    using HttpClient client = new() { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") };
    //    var request = CreateRequest("5");
    //    using (var result = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
    //    {
    //        using (var contentStream = await result.Content.ReadAsStreamAsync())
    //        {
    //            return await JsonSerializer.DeserializeAsync<List<GitHubRepositoryDto>>(contentStream, DefaultJsonSerializerOptions.Options, cancellationToken);
    //        }
    //    }
    //}

    private static HttpRequestMessage CreateRequest(string? id)
    {
        return new HttpRequestMessage(HttpMethod.Get, $"item/{id}.json");
    }
}
