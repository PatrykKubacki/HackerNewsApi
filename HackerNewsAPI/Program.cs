
using HackerNewsAPI.Extensions;
using HackerNewsAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();

/*
 There's still a lot to do in this project.
 There's no model validation in requests, which could be done using, for example,
 FluentValidation. The logging logic defaults to the console instead of to a file using
 a library like SeriLog. There's no authorization. Consideration should be given in 
 applications of this type to caching results, for example using Redis. 
 There's a lack of features related to monitoring the API's status, such as health checks. 
 The API is built as a single project; consider introducing, for example, Clean Architecture. 
 Of course, there's a lack of an entire testing strategy, including unit tests, integration tests,
 and performance tests.
 */