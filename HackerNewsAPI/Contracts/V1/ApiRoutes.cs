namespace HackerNewsAPI.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Verion = "v1";
    public const string Base = $"{Root}/{Verion}";

    public static class BestStories
    {
        public const string Get = $"{Base}/beststories/{{limit}}";
    }
}

/*
 I used the Requests and Responses directories to separate HTTP requests models.
 I created an ApiRoutes file because this approach facilitates maintenance,
 especially when we have logic based on redirections, with changes in just one place.
 */