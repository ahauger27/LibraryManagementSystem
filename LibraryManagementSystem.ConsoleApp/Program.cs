using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;


Processor run = new Processor();
run.Start();

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5126");
HttpResponseMessage response = await client.GetAsync("/api/Patron");
JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

var patrons = new List<Patron>();
if (response.IsSuccessStatusCode)
{
    string jsonResponse = await response.Content.ReadAsStringAsync();

    patrons = JsonSerializer.Deserialize<List<Patron>>(jsonResponse, options);
}
else
{
    Console.WriteLine($"Error: {response.StatusCode}");
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

Console.WriteLine("Library Management System");
Console.WriteLine("=========================");

while (run.RunStatus == true)
{
    Console.WriteLine($"{Environment.NewLine}What would you like to do?");
    Console.WriteLine("1. Look up patron(s)");
    Console.WriteLine("2. Search patrons by name (WIP)");
    Console.WriteLine("3. Quit program (WIP)");
    
    string? userChoice = Console.ReadLine();

    if (userChoice != null || userChoice != string.Empty)
    {
        switch (userChoice)
        {
            case "1":
                ListAllPatrons(patrons);
                break;
            case "2":
                Console.WriteLine("This feature is still in progress");
                break;
            case "3":
                run.End();
                Console.WriteLine("End program");
                break;
            default:
                break;
        }
    }
}

/*
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
*/

static void ListAllPatrons(List<Patron> patronList)
        {
            if (patronList.Count == 0)
            {
                Console.WriteLine("No patrons found.");   
            }
            else
            {
                Console.WriteLine($"{Environment.NewLine}Patrons:");
                Console.WriteLine("\tNAME");
                
                int i = 0;
                foreach (var patron in patronList)
                {
                    Console.WriteLine($"{++i:D3} \t{patron.LastName}, {patron.FirstName}");
                }
            }
        }