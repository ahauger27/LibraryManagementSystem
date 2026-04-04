using System.Text.Json;

namespace LibraryManagementSystem.ConsoleApp.Models;

public static class PatronActions
{    
    public static bool VerifyNewPatronInfo(Patron newPatron)
    {
        string? response;

        do
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

            response = UserActions.StringInput().ToUpper();
            if (response == "N")
            {
                UpdatePatronInfo(newPatron);
                return false;
            }
            else
            {
                return true;
            }

        } while (response != "Y");
    }

    public static void UpdatePatronInfo(Patron patron)
    {
        Console.WriteLine(
        $"""

        CHOOSE WHICH FIELD TO CHANGE:
        1. FIRST NAME: {patron.FirstName}
        2. MIDDLE INITIAL: {patron.MiddleInitial}
        3. LAST NAME: {patron.LastName}
        4. DATE OF BIRTH: {patron.DateOfBirth}
        5. ADDRESS: {patron.Address}
        6. EMAIL: {patron.Email}
        7. PHONE NUMBER: {patron.PhoneNumber}
        8. Quit Editing
        """
        );

        Console.Write("Which field do you want to update?: ");
        string? input = UserActions.StringInput();

        switch (input)
        {
            case "1":
                patron.FirstName = InputPatronFirstName();
                break;

            case "2":
                patron.MiddleInitial = InputPatronMiddleInitial();
                break;

            case "3":
                patron.LastName = InputPatronLastName();
                break;

            case "4":
                patron.DateOfBirth = DateOfBirth.DateInput();
                break;

            case "5":
                patron.Address = InputPatronAdress();
                break;

            case "6":
                patron.Email = InputPatronEmail();
                break;

            case "7":
                patron.PhoneNumber = InputPatronPhoneNumber();
                break;
            
            case "8":
                Console.WriteLine("Done");
                break;
                 
            default:
                break;
        }        
    }

    public static Patron CreateNewPatron()
    {
        return InputNewPatronInfo();
        
        // bool patronCreationSuccess = false;
        
        // while (!patronCreationSuccess)
        // {

        //     if (!VerifyNewPatronInfo(newPatron))
        //     {
        //         continue;
        //     }
        //     else
        //     {
        //         patronCreationSuccess = true;
        //         return newPatron;
        //     }
        // }
    }

    public static Patron InputNewPatronInfo()
    {
        string firstName = InputPatronFirstName();

        string middleInitial = InputPatronMiddleInitial();
        
        string lastName = InputPatronLastName();

        DateOnly dateOfBirth = DateOfBirth.DateInput();

        string address = InputPatronAdress();

        string email = InputPatronEmail();

        string phoneNumber = InputPatronPhoneNumber();
            
        Patron newPatron = new(firstName, lastName, dateOfBirth, middleInitial, address, email, phoneNumber);
        return newPatron;
    }

    public static string InputPatronFirstName()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's FIRST NAME: ");
        return UserActions.StringInput();
    }

    public static string InputPatronMiddleInitial()
    {
        string? middleInitial = "";
        while (middleInitial.Length != 1)
        {
            Console.Write($"{Environment.NewLine}Enter the patron's MIDDLE INITIAL: ");
            middleInitial = UserActions.StringInput();
            
        }
        return middleInitial; 
    }

    public static string InputPatronLastName()
    {

        Console.Write($"{Environment.NewLine}Enter the patron's LAST NAME: ");
        return UserActions.StringInput();
    }

    public static string InputPatronAdress()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's ADDRESS: ");
        return UserActions.StringInput();   
    }

    public static string InputPatronEmail()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's EMAIL: ");
        return UserActions.StringInput();    
    }

    public static string InputPatronPhoneNumber()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's PHONE NUMBER: ");
        return UserActions.StringInput();    
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
    public static async Task PutPatron(int id, HttpClient client, JsonSerializerOptions options)
    {
        Patron patron = await GetPatronByID(id,client, options);
        
        if (patron != null)
        {
            Console.WriteLine($"Updating patron record for {patron.FullName()}");

            UpdatePatronInfo(patron);
            string patronJson = JsonSerializer.Serialize(patron, options);
            StringContent content = new(patronJson, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"/patrons/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully updated patron: {patron.PatronID}");
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
