using System.Text.Json;
using LibraryManagementSystem.Console.Models;

HttpClient client = new HttpClient();

client.BaseAddress = new Uri("http://localhost:5126");
HttpResponseMessage response = await client.GetAsync("/api/people");

JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

if (response.IsSuccessStatusCode)
{
    string jsonResponse = await response.Content.ReadAsStringAsync();

    var people = JsonSerializer.Deserialize<List<Person>>(jsonResponse, options);

    foreach (var person in people)
    {
        Console.WriteLine($"{person.Name} speaks {person.Language}");
    }
}
else
{
    Console.WriteLine($"Error: {response.StatusCode}");
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}