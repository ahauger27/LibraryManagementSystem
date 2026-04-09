using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronHttpActions
{    
    // GET ALL
    public static async Task<string> GetPatrons(HttpClient client)
    {
        HttpResponseMessage response = await client.GetAsync("/patrons");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();   
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            return await response.Content.ReadAsStringAsync();
        }
    }

    // GET BY ID
    public static async Task<Patron?> GetPatronByID(string id, HttpClient client, JsonSerializerOptions options)
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
            // Console.WriteLine($"Error: {singleResponse.StatusCode}");
            // Console.WriteLine(await singleResponse.Content.ReadAsStringAsync());
            return null;
        }
    }

    // POST
    public static async Task PostNewPatron(Patron newPatron, HttpClient client)
    {
        string newPatronJson = JsonSerializer.Serialize(newPatron);
        StringContent content = new(newPatronJson, System.Text.Encoding.UTF8, "application/json");

        // Need to check if this patron already exists

        HttpResponseMessage response = await client.PostAsync($"/patrons", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Successfully created new patron: {newPatron.PrintPatronName()}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    // PUT
    public static async Task PutPatron(Patron patron, HttpClient client, JsonSerializerOptions options)
    {
        // Patron patronToUpdate = await GetPatronByID(id, client, options);
        
        if (patron != null)
        {
            Console.WriteLine($"Updating patron record for {patron.FullName()}");

            string patronJson = JsonSerializer.Serialize(patron, options);
            StringContent content = new(patronJson, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"/patrons/{patron.PatronID}", content);

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
    public static async Task DeletePatron(Patron patron, HttpClient client)
    {
        try
        {
            HttpResponseMessage response = await client.DeleteAsync($"/patrons/{patron.PatronID}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Successfully deleted patron with ID: {patron.PatronID}");
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
