using System.Text.Json;

namespace LibraryManagementSystem.ConsoleApp.Models;

public static class PatronHttpActions
{    
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
    public static async Task PutPatron(int id, Patron patron, HttpClient client, JsonSerializerOptions options)
    {
        Patron patronToUpdate = await GetPatronByID(id,client, options);
        
        if (patronToUpdate != null)
        {
            Console.WriteLine($"Updating patron record for {patronToUpdate.FullName()}");

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
