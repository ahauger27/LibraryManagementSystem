using System.Text.Json;

namespace LibraryManagementSystem.Common.Models;

public static class PatronActions
{    
    
    public static Patron CreateNewPatron()
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
    
    // GET ALL
    public static async Task GetPatrons(HttpClient client, JsonSerializerOptions options)
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

        if (patrons?.Count == 0)
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

    // GET BY ID
    public static async Task<Patron> GetPatronByID(HttpClient client, JsonSerializerOptions options, int id)
    {
        HttpResponseMessage singleResponse = await client.GetAsync($"/patrons/{id}");

        if (singleResponse.IsSuccessStatusCode)
        {
            string jsonResponse = await singleResponse.Content.ReadAsStringAsync();
            var patron = JsonSerializer.Deserialize<Patron>(jsonResponse, options);

            return patron;
        }
        else
        {
            Console.WriteLine($"Error: {singleResponse.StatusCode}");
            Console.WriteLine(await singleResponse.Content.ReadAsStringAsync());
            return null;
        }
    }

    // POST
    public static async Task PostNewPatron(Patron newPatron, HttpClient client)
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

    // PUT
    public static async Task UpdatePatron(int id, HttpClient client, JsonSerializerOptions options)
    {
        Patron patron = await GetPatronByID(client, options, id);

        Console.WriteLine($"Updating patron record for {patron.FullName()}");

        Console.Write("Enter new FIRSTNAME: ");
        string? UpdatedFirstName = Console.ReadLine();
        patron.FirstName = UpdatedFirstName;

        string patronJson = JsonSerializer.Serialize(patron, options);
        StringContent content = new(patronJson, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync($"/patrons/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Successfully added new patron: {patron.FullName()}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}{Environment.NewLine}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    // DELETE
    public static async Task DeletePatron(int id, HttpClient client)
    {
        try
        {
            HttpResponseMessage response = await client.DeleteAsync($"/patrons/{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully delete patron with ID: {id}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            } 
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
