using LibraryManagementSystem.ConsoleApp.Models;
using System.Net.Sockets;
using System.Text.Json;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class ItemHttpActions
{
    public static async Task<string?> GetItems(HttpClient client)
    {
        try
        {  
            HttpResponseMessage response = await client.GetAsync("/items");
            
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
            Console.WriteLine("The server is not listening.");
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
            return null;
        }
    }

    public static async Task<Item?> GetItemByID(string id, HttpClient client, JsonSerializerOptions options)
    {
        try
        {
            HttpResponseMessage singleResponse = await client.GetAsync($"/items/{id}");

            if (singleResponse.IsSuccessStatusCode)
            {
                string jsonResponse = await singleResponse.Content.ReadAsStringAsync();
                var item = JsonSerializer.Deserialize<Item>(jsonResponse, options);
                
                return item;
            }
            else
            {
                Console.WriteLine($"Error: {singleResponse.StatusCode}");
                Console.WriteLine(await singleResponse.Content.ReadAsStringAsync());
                return null;
            }
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

    //PUT
    public static async Task PutItem(Item item, HttpClient client, JsonSerializerOptions options)
    {
        if (item != null)
        {
            string itemJson = JsonSerializer.Serialize(item, options);
            StringContent content = new(itemJson, System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PutAsync($"/items/{item.ItemNumber}", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
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
    }
}