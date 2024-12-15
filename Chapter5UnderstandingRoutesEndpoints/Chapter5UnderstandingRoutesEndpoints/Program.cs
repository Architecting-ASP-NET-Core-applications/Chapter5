var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Benvenuto nella homepage!");
});

app.MapGet("/about", async context =>
{
    await context.Response.WriteAsync("Informazioni su di noi.");
});




app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();



app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Wekcome in the home page!");
}).WithName("aaa").WithOpenApi();

app.MapGet("/about", async context =>
{
    await context.Response.WriteAsync("About us.");
}).WithName("sss").WithOpenApi();


app.MapGet("/api/", async context =>
{
    await context.Response.WriteAsync("Hi from the homepage!");
}).WithName("ddd").WithOpenApi();

app.MapGet("/api/about", async context =>
{
    await context.Response.WriteAsync("Hi from us.");
}).WithName("fff").WithOpenApi();



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
