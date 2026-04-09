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

    public static bool DoesItemExist(Item item)
    {
        if (item == null)
        {   
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public static async Task<List<Item>> CreateItemsListFromApi(string jsonContent, JsonSerializerOptions options)
    {
        var items = JsonSerializer.Deserialize<List<Item>>(jsonContent, options);
        
        if (items.Count == 0)
        {
            Console.WriteLine("No items found.");   
        }
        return items;
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

    // public static Item GetItem()
    // {
    //      Console.Write("Enter Patron ID: ");
    //                 string? idToSearch = Console.ReadLine();

    //                 if (string.IsNullOrEmpty(idToSearch))
    //                 {
    //                     Console.WriteLine("Invalid ID Format.");
    //                     break;
    //                 }   
                    
    //                 Console.WriteLine("Loading Patron...");

    //                 Patron patron = await PatronHttpActions.GetPatronByID(idToSearch, client, session.JsonOptions);
                    
    //                 if (patron != null)
    //                 {
    //                     await PatronAccountMenu.MenuLoop(patron, client, session);
    //                 }
    //                 else
    //                 {
    //                     Console.WriteLine("Patron Does Not Exist...");
    //                     break;
    //                 }
    //                 break;
    // }
}