using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class ItemGetActions
{
    public static string GetItemNumberFromUser()
    {
        Console.Write("Enter item number: ");

        while (true)
        {
            string? itemNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(itemNumber))
            {
                Console.WriteLine("INVALID. Item number cannot be empty.");
                Console.Write("Please enter the item number: ");
                continue;
            }
            else
            {
                return itemNumber;
            }
        }
    }

    public static async Task<Item?> TryToLoadItemRecord(string itemIDToSearch, HttpClient client, Processes session)
    {
        try
        {
            Item? itemToSearch = await ItemHttpActions.GetItemByID(itemIDToSearch, client, session.JsonOptions);
            
            return itemToSearch;
        }
        catch (NullReferenceException)
        {
            return null;
        }
    }
    
    public static async Task<List<Item>?> CreateItemsListFromJson(string jsonContent, JsonSerializerOptions options)
    {
        var itemsList = JsonSerializer.Deserialize<List<Item>>(jsonContent, options);
        
        if (itemsList != null)
        {
            return itemsList;
        }
        else
        {
            return null;
        }
    }

    public static void DisplayAllItems(List<Item> items)
    {   
        Console.WriteLine("");
        Console.WriteLine("ITEM#\tTITLE");
        Console.WriteLine("=================================");
        
        foreach (var item in items)
        {
            Console.WriteLine($"{item.ItemNumber}\t{item.PrintTitle()}");
        }
    }
}