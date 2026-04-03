using System.Text.Json;

namespace LibraryManagementSystem.Common.Models;

public static class PatronActions
{    
    public static bool VerifyNewPatronInfo(Patron newPatron)
    {
        Console.WriteLine($"{Environment.NewLine}DO YOU WANT TO CONTINUE WITH THIS INFORMATION?");
        Console.WriteLine($"FIRST NAME: {newPatron.FirstName}");
        Console.WriteLine($"MIDDLE INITIAL: {newPatron.MiddleInitial}");
        Console.WriteLine($"LAST NAME: {newPatron.LastName}");
        Console.WriteLine($"DATE OF BIRTH: {newPatron.DateOfBirth}");
        Console.WriteLine($"ADDRESS: {newPatron.Address}");
        Console.WriteLine($"EMAIL: {newPatron.Email}");
        Console.WriteLine($"PHONE NUMBER: {newPatron.PhoneNumber}");
        Console.Write("Y/N: "); //can't say no right now

        string? input = UserActions.StringInput();

        if (!UserActions.IsInputValid(input))
        {
            Console.WriteLine("Please ");
            return false;
        }

        return true;
    }
    public static Patron CreateNewPatron()
    {
        Patron newPatron = InputNewPatronInfo();

        if (!VerifyNewPatronInfo(newPatron))
        {
            throw new Exception();
        }
        else
        {
            return newPatron;
        }
    }

    public static Patron InputNewPatronInfo()
    {

        Console.Write($"{Environment.NewLine}Enter the patron's FIRST NAME: ");
        string firstName = UserActions.StringInput();

        Console.Write($"{Environment.NewLine}Enter the patron's MIDDLE INITIAL: ");
        string middleName = UserActions.StringInput();

        Console.Write($"{Environment.NewLine}Enter the patron's LAST NAME: ");
        string lastName = UserActions.StringInput();

        DateOnly dateOfBirth = DateOfBirth.DateInput();

        Console.Write($"{Environment.NewLine}Enter the patron's ADDRESS: ");
        string address = UserActions.StringInput();

        Console.Write($"{Environment.NewLine}Enter the patron's EMAIL: ");
        string email = UserActions.StringInput();

        Console.Write($"{Environment.NewLine}Enter the patron's PHONE NUMBER: ");
        string phoneNumber = UserActions.StringInput();
            
        Patron newPatron = new(firstName, lastName, dateOfBirth, middleName, address, email, phoneNumber);
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
    public static async Task<Patron> GetPatronByID(int id,HttpClient client, JsonSerializerOptions options)
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
        Patron patron = await GetPatronByID(id,client, options);
        
        if (patron != null)
        {
            Console.WriteLine($"Updating patron record for {patron.FullName()}");

            Console.Write("Enter new FIRSTNAME: ");
            string? UpdatedFirstName = Console.ReadLine();
            patron.FirstName = UpdatedFirstName;

            string patronJson = JsonSerializer.Serialize(patron, options);
            StringContent content = new(patronJson, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"/patrons/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully update patron: {patron.FullName()}");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
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
                Console.WriteLine($"Successfully deleted patron with ID: {id}");
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
