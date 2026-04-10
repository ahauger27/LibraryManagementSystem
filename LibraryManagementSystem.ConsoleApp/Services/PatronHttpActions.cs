using System.Text.Json;
using System.Net.Sockets;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronHttpActions
{    
    // GET ALL
    public static async Task<string> GetPatrons(HttpClient client)
    {
        try
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
        catch (SocketException)
        {
            return "The server is not listening.";
        }
        catch (HttpRequestException ex)
        {
            return ex.Message;
        }
    }

    // GET BY ID
    public static async Task<Patron?> GetPatronByID(string id, HttpClient client, JsonSerializerOptions options)
    {
        try
        {
            HttpResponseMessage singleResponse = await client.GetAsync($"/patrons/{id}");

            string jsonResponse = await singleResponse.Content.ReadAsStringAsync();
            var patron = JsonSerializer.Deserialize<Patron>(jsonResponse, options);
            
            return patron;
        }
        catch (SocketException)
        {
            Console.WriteLine("The server is not listening.");
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
            return null;
        }
    }

    // POST
    public static async Task PostNewPatron(Patron newPatron, HttpClient client)
    {
        string newPatronJson = JsonSerializer.Serialize(newPatron);
        StringContent content = new(newPatronJson, System.Text.Encoding.UTF8, "application/json");

        // Need to check if this patron already exists

        try
        {
            HttpResponseMessage response = await client.PostAsync($"/patrons", content);

            Console.WriteLine($"Successfully created new patron: {newPatron.PrintPatronName()}");
        }
        catch (SocketException)
        {
            Console.WriteLine("The server is not listening.");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }

    // PUT
    public static async Task PutPatron(Patron patron, HttpClient client, JsonSerializerOptions options)
    {
        try
        {            
            if (patron != null)
            {
                string patronJson = JsonSerializer.Serialize(patron, options);
                StringContent content = new(patronJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"/patrons/{patron.PatronID}", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
            }
        }    
        catch (SocketException)
        {
            Console.WriteLine("The server is not listening.");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
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
