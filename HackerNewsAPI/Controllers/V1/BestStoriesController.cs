using HackerNewsAPI.Contracts.V1;
using HackerNewsAPI.Dto;
using HackerNewsAPI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsAPI.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class BestStoriesController : Controller
{
    readonly IHackerNewsService _hackerNewsService;

    public BestStoriesController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet(ApiRoutes.BestStories.Get)]
    public async Task<ActionResult<IEnumerable<StoryDTO>>> Get([FromRoute] int limit)
    {
        var bestStories = await _hackerNewsService.GetBestStories(limit);
        if (!bestStories.Any())
            return NotFound();

        return Ok(bestStories);
    }
}

/*
 The V1 directory serves as an introduction to versioning. Additionally,
 I noticed that the source Hacker News API also has various versions. 
 I considered having one controller named Story, but I decided to narrow the context to beststories.
 */