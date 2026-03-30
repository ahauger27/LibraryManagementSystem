using System.Text.Json;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Common.Services;


Processor run = new Processor();
run.Start();

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5126");
JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

Console.WriteLine($"{Environment.NewLine}LIBRARY MANAGEMENT SYSTEM");
Console.WriteLine("=========================");


while (run.RunStatus == true)
{
    Console.WriteLine($"{Environment.NewLine}OPTIONS MENU");
    Console.WriteLine($"============{Environment.NewLine}");
    Console.WriteLine("1. Patron Search (WIP)");
    Console.WriteLine("2. Item Search (WIP)");
    Console.WriteLine("3. Add New Patron (WIP)");
    Console.WriteLine("4. Quit program (WIP)");
    
    string? userChoice = Console.ReadLine();

    try
    {
        if (userChoice != null || userChoice != string.Empty)
        {
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine($"{Environment.NewLine}PATRON OPTIONS{Environment.NewLine}==============");
                    Console.WriteLine("1. List All Patrons");
                    Console.WriteLine("2. Search Patrons By ID (WIP)");

                    string? userChoice2 = Console.ReadLine();
                    if (userChoice2 != null || userChoice2 != string.Empty)
                    {
                        switch (userChoice2)
                        {
                            case "1":
                                await ListAllPatrons();
                                // Console.WriteLine("This feature is still in progress");
                                break;
                            case "2":
                                Console.WriteLine("Enter patron's ID: ");
                                string? idInput = Console.ReadLine();
                                if (idInput != null || idInput != string.Empty)
                                {
                                    if (int.TryParse(idInput, out int id))
                                    {
                                        await SearchPatronsByID(id);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid ID format.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input, returning to main menu.");
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid option, returning to main menu.");
                                break;
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine("This feature is still in progress");
                    break;
                case "3":
                    Console.WriteLine("This feature is still in progress");
                    // await AddNewPatron(new Patron(""));

                    break;
                case "4":
                    run.End();
                    Console.WriteLine("Shutting down program...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine(ex.Message);
    }
    Console.WriteLine("Press \"Enter\" to continue...");
    Console.ReadLine();
}


async Task ListAllPatrons()
{
    HttpResponseMessage response = await client.GetAsync("/patrons");

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

    if (patrons.Count == 0)
    {
        Console.WriteLine("No patrons found.");   
    }
    else
    {
        Console.WriteLine($"{Environment.NewLine}Patrons:");
        Console.WriteLine("ID\t\tNAME");
        Console.WriteLine("=================================");
        
        foreach (var patron in patrons)
        {
            Console.WriteLine($"{patron.PatronID}\t{patron.LastName}, {patron.FirstName}");
        }
    }
}


async Task SearchPatronsByID(int id)
{
    HttpResponseMessage singleResponse = await client.GetAsync($"/patrons/{id}");

    if (singleResponse.IsSuccessStatusCode)
    {
        string jsonResponse = await singleResponse.Content.ReadAsStringAsync();
        var patron = JsonSerializer.Deserialize<Patron>(jsonResponse, options);

        Console.WriteLine($"{Environment.NewLine}Search results for ID: {id}");
        Console.WriteLine($"{patron.FullName()}");
    }
    else
    {
        Console.WriteLine($"Error: {singleResponse.StatusCode}");
        Console.WriteLine(await singleResponse.Content.ReadAsStringAsync());
    }
}


async Task AddNewPatron(Patron patron)
{
    Patron newPatron;

    Console.WriteLine("Enter new patron's first name: ");
    string? firstName = Console.ReadLine();
    Console.WriteLine("Enter new patron's last name: ");
    string? lastName = Console.ReadLine();
    Console.WriteLine("Enter new patron's date of birth (YYYY, MM, DD)");
    string? dateOfBirth = Console.ReadLine();

    if (firstName != null && lastName != null && dateOfBirth != null)
    {
        if (DateTime.TryParse(dateOfBirth, out DateTime parsedDate))
        {
            newPatron = new Patron(firstName, lastName, parsedDate, "");
        }
        else
        {
            Console.WriteLine("Invalid date format, returning to main menu.");
            return;
        }
        string newPatronJson = JsonSerializer.Serialize(newPatron);
        var content = new StringContent(newPatronJson, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"/patrons/{patron.PatronID}", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Successfully added new patron: {newPatron.FullName()}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
    else
    {
        Console.WriteLine("Invalid input, returning to main menu.");
    }
}
