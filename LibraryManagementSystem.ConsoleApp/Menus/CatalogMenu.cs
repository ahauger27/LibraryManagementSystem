using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class CatalogMenu
{
    public static async Task MenuLoop(Processes session, HttpClient client)
    {
        Console.Clear();
        
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            Console.WriteLine("""

            CATALOG VIEWER
            ==============

            OPTIONS
            1. View All Items in Catalog
            2. Search By Item Number
            3. Return To Main Menu

            """);
        
            Console.Write("Please select An option: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("Loading Catalog...");

                    string? itemsJson = await ItemHttpActions.GetItems(client);
                    
                    if (itemsJson == null)
                    {
                        Console.WriteLine("Catalog unavailable");
                        break;
                    }

                    List<Item> itemsList = await ItemGetActions.CreateItemsListFromJson(itemsJson, session.JsonOptions);

                    if (itemsList == null)
                    {
                        Console.WriteLine("There are no items in the catalog");
                    }
                    else
                    {
                        ItemGetActions.DisplayAllItems(itemsList);
                    }
                    
                    Console.WriteLine("""
                    
                    OPTIONS
                    1. View Single Item Record
                    2. Return To Catalog Viewer

                    """);
                    
                    Console.Write("Please select an option: ");

                    string? userChoice2 = Console.ReadLine();

                    switch (userChoice2)
                    {
                        case "1":
                            string? itemIDToSelect = ItemGetActions.GetItemNumberFromUser();

                            Item? itemToSelect = await ItemGetActions.TryToLoadItemRecord(itemIDToSelect, client, session);

                            if (itemToSelect == null)
                            {
                                Console.WriteLine($"The ID \"{itemIDToSelect}\" is not tied to an existing record.");
                                Console.WriteLine("Returning to menu..."); 
                                UserActions.PressKeyToContinue();
                                Console.Clear();  
                            }
                            else
                            {
                                await ItemRecordMenu.MenuLoop(itemToSelect, client, session);
                            }
                            break;

                        default:
                            Console.Clear();
                            break;
                            
                    }
                    break;

                case "2":
                    string? itemNumberToSearch = ItemGetActions.GetItemNumberFromUser();

                    try
                    {
                        Console.WriteLine("Loading item...");

                        Item item = await ItemHttpActions.GetItemByID(itemNumberToSearch, client, session.JsonOptions);

                        if (item == null)
                        {
                            Console.WriteLine("This item number is not tied to an existing item.");
                            break;
                        }

                        await ItemRecordMenu.MenuLoop(item, client, session);
                        break;

                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "3":
                    Console.Clear();
                    
                    returnToMainMenu = true;
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: Please Enter 1, 2, or 3");
                    break;
            }
        }
    }
}