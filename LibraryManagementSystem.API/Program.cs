using System.Text.Json;
using LibraryManagementSystem.Common.Services;
using LibraryManagementSystem.Common.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
// builder.Services.AddControllers();

var app = builder.Build();

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

string jsonFile = File.ReadAllText("./Resources/patrons.json");
var patrons = JsonSerializer.Deserialize<List<Patron>>(jsonFile, options);

app.MapGet("/patrons", () => patrons)
    .WithName("GetPatrons")
    .Produces<List<Patron>>(statusCode: StatusCodes.Status200OK);

app.MapGet("/patrons/{id}", (int id) =>
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
    return Results.Created($"/patrons/{newPatron.LastName}", newPatron);
});

/*app.MapPut("/patrons/{PatronID}", (int PatronID, Patron updatedPatron) =>{
    var patronToUpdate = patrons?.FirstOrDefault(p => p.PatronID == PatronID);

    if (patronToUpdate != null)
    {
        patronToUpdate.FirstName = updatedPatron.FirstName;
        patronToUpdate.LastName = updatedPatron.LastName;
        patronToUpdate.Email = updatedPatron.Email;
        patronToUpdate.PhoneNumber = updatedPatron.PhoneNumber;

        return Results.Ok($"Patron with ID: {PatronID}, updated successfully.");
    }
    else
    {
        return Results.NotFound($"Patron with ID: {PatronID}, not found.");
    }
});
*/
app.MapDelete("/patrons/{PatronID}", (int PatronID) =>
{
    var patronToDelete = patrons?.FirstOrDefault(p => p.PatronID == PatronID);

    if (patronToDelete != null)
    {
        patrons?.Remove(patronToDelete);
        return Results.Ok($"Patron with ID: {PatronID}, deleted successfully.");
    }
    else
    {
        return Results.NotFound($"Patron with ID: {PatronID}, not found.");
    }
});

// app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.Run();
