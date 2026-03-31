using System.Text.Json;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Common.Services;

/*
CATEGOREIS:
CAT 4: OF HIGHEST IMPORTANCE
CAT 3: OF SECOND HIGHEST IMPORTANCE
CAT 2: OF THIRD HIGHEST IMPORTANCE
CAT 1: OF LEAST IMPORTANCE
*/

Processor run = new Processor();
run.Start();

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5126");
JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

Console.WriteLine($"{Environment.NewLine}LIBRARY MANAGEMENT SYSTEM");
Console.WriteLine("=========================");


while (run.RunStatus == true)
{
    // Make this section of Console.WriteLines more modular
    // As in, make sure that I could easily swap these out later,
    // CAT 2, would make life easier, but not crucial to program atm.
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
                case "1": // 1. Patron Search (WIP)
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
                                break;
                            case "2": // Search by patron method name // MORE READABLE
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
                case "2": // 2. Search Items (WIP)
                    Console.WriteLine("This feature is still in progress");
                    break;
                case "3": // 3. Add New Patron (WIP)
                    Console.WriteLine("MENU TO ADD A NEW PATRON");

                    Patron newPatron = CreateNewPatron();
                    await PostNewPatron(newPatron, client);
                    //
                    //
                    //

                    //
                    //
                    //
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

Patron CreateNewPatron()
{
    /*
    // TELL THE USER WHICH ARE REQUIRED?
    Console.WriteLine("Enter the patron's FIRST NAME: ");
    string? firstName = Console.ReadLine();

    Console.WriteLine("Enter the patron's LAST NAME: ");
    string? lastName = Console.ReadLine();

    Console.WriteLine("Enter the patron's DATE OF BIRTH (YYYY-MM-DD): ");
    string? middleName = Console.ReadLine();

    Console.WriteLine("Enter the patron's MIDDLE NAME: ");
    string? dateOfBirth = Console.ReadLine();

    // CHECKDATEFUNCTION(); while loop so user can't go further until correct : 
    // CAT 3
    if (DateTime.TryParse(dateOfBirth, out DateTime parsedDOB)) { }
    else
    {
        Console.WriteLine("Invalid date format, returning to main menu.");
    }

    Console.WriteLine("Enter the patron's ADDRESS: ");
    string? address = Console.ReadLine();

    Console.WriteLine("Enter the patron's EMAIL: ");
    // PROPER EMAIL CHECKER?
    string? email = Console.ReadLine();

    Console.WriteLine("Enter the patron's PHONE NUMBER: ");
    string? phoneNumber = Console.ReadLine();
    */

    Patron newPatron = new("jIM", "bO", new DateTime());
    return newPatron;
}

async Task PostNewPatron(Patron newPatron, HttpClient client)
{
    string newPatronJson = JsonSerializer.Serialize(newPatron);
    StringContent content = new(newPatronJson, System.Text.Encoding.UTF8, "application/json");

    HttpResponseMessage response = await client.PostAsync($"/patrons", content);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Successfully added new patron: {newPatron.FullName()}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}{Environment.NewLine}");
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
