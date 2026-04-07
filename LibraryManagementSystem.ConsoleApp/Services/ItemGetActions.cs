using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class ItemGetActions
{
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
        Console.WriteLine($"{Environment.NewLine}ITEMS:");
        Console.WriteLine("ITEM NUMBER\tTITLE");
        Console.WriteLine("=================================");
        
        foreach (var item in items)
        {
            Console.WriteLine($"{item.ItemNumber}\t{item.Title}");
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