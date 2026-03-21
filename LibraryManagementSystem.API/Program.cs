using System.Text.Json;
using LibraryManagementSystem.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

// TESTing Json Deserialization and swagger hosting
string jsonFile = File.ReadAllText("./Resources/64KB.json");
var jsonData = JsonSerializer.Deserialize<List<Person>>(jsonFile, options);

app.MapGet("/people", () => jsonData)
    .WithName("GetPeople")
    .Produces<List<Person>>(statusCode: StatusCodes.Status200OK);

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
