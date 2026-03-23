using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

HttpClient client = new HttpClient();

client.BaseAddress = new Uri("http://localhost:5126");
HttpResponseMessage response = await client.GetAsync("/api/Patron");

JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

if (response.IsSuccessStatusCode)
{
    string jsonResponse = await response.Content.ReadAsStringAsync();

    var patrons = JsonSerializer.Deserialize<List<Patron>>(jsonResponse, options);

    foreach (var patron in patrons)
    {
        Console.WriteLine($"{patron.FirstName} {patron.LastName} is {patron.DisplayAge()} years old.");
    }
}
else
{
    Console.WriteLine($"Error: {response.StatusCode}");
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

HttpResponseMessage singleResponse = await client.GetAsync("/api/Patron/Smith");

if (singleResponse.IsSuccessStatusCode)
{
    string jsonResponse = await singleResponse.Content.ReadAsStringAsync();

    var patron = JsonSerializer.Deserialize<Patron>(jsonResponse, options);

    Console.WriteLine($"{patron.PrintPatronName()} is {patron.DisplayAge()} years old.");
}
else
{
    Console.WriteLine($"Error: {singleResponse.StatusCode}");
    Console.WriteLine(await singleResponse.Content.ReadAsStringAsync());
}