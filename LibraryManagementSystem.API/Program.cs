using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

string itemJsonFile = File.ReadAllText("./Resources/items.json");
string patronJsonFile = File.ReadAllText("./Resources/patrons.json");

var items = JsonSerializer.Deserialize<List<Item>>(itemJsonFile, options);
var patrons = JsonSerializer.Deserialize<List<Patron>>(patronJsonFile, options);

app.MapGet("/items", async () => items)
    .WithName("GetItems")
    .Produces<List<Item>>(statusCode: StatusCodes.Status200OK);

app.MapGet("/items/{id}", (string id) =>
{
    var item = items?.FirstOrDefault(i => i.ItemNumber == id);
    if (item != null)
    {
        return Results.Ok(item);
    }
    else
    {
        return Results.NotFound($"Item with Item Number: {id} not found.");
    }
});

app.MapPut("/items/{id}", ([FromRoute]string id, [FromBody] Item inputItem) =>
{
    var existingItem = items?.FirstOrDefault(i => i.ItemNumber == id);

    if (existingItem == null)
    {
        return Results.NotFound($"Item with Item Number: {id} not found.");
    }

    existingItem.CircStatus = inputItem.CircStatus;
    existingItem.CurrentBorrowerID = inputItem.CurrentBorrowerID;

    return Results.Ok($"Item updated successfully");
});

app.MapGet("/patrons", async () => patrons)
    .WithName("GetPatrons")
    .Produces<List<Patron>>(statusCode: StatusCodes.Status200OK);

app.MapGet("/patrons/{id}", (string id) =>
{
    var patron = patrons?.FirstOrDefault(p => p.PatronID == id);
    if (patron != null)
    {
        return Results.Ok(patron);
    }
    else
    {
        return Results.NotFound($"Patron with ID: {id} not found.");
    }
});

// app.MapGet("/patrons/LastName{LastName}", (string LastName) =>
// {
//     var patron = jsonData?.FirstOrDefault(p => p.LastName == LastName);
//     if (patron != null)
//     {
//         return Results.Ok(patron);
//     }
//     else
//     {
//         return Results.NotFound($"Patron with Last Name: {LastName} not found.");
//     }
// });

app.MapPost("/patrons", (Patron newPatron) =>
{
    patrons?.Add(newPatron);
    return Results.Created($"/patrons/{newPatron.PatronID}", newPatron);
});

app.MapPut("/patrons/{id}", ([FromRoute]string id,[FromBody] Patron inputPatron) =>
{
    var existingPatron = patrons?.FirstOrDefault(p => p.PatronID == id);

    if (existingPatron == null)
    {
        return Results.NotFound($"Patron with ID: {id}, not found.");
    }
    
    existingPatron.FirstName = inputPatron.FirstName;
    existingPatron.LastName = inputPatron.LastName;
    existingPatron.MiddleInitial = inputPatron.MiddleInitial;
    existingPatron.DateOfBirth = inputPatron.DateOfBirth;
    existingPatron.Address = inputPatron.Address;
    existingPatron.Email = inputPatron.Email;
    existingPatron.PhoneNumber = inputPatron.PhoneNumber;  
    existingPatron.ActiveLoans = inputPatron.ActiveLoans;

    return Results.Ok($"Patron with ID: {id}, updated successfully.");
});

app.MapDelete("/patrons/{id}", (string id) =>
{
    var patronToDelete = patrons?.FirstOrDefault(p => p.PatronID == id);

    if (patronToDelete != null)
    {
        patrons?.Remove(patronToDelete);
        return Results.Ok($"Patron with ID: {id}, deleted successfully.");
    }
    else
    {
        return Results.NotFound($"Patron with ID: {id}, not found.");
    }
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.Run();
