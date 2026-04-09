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

                    string itemJson = await ItemHttpActions.GetItems(client);
                    List<Item> itemList = await ItemGetActions.CreateItemsListFromApi(itemJson, session.JsonOptions);
                    ItemGetActions.DisplayAllItems(itemList);
                    
                    UserActions.PressKeyToContinue();
                    Console.Clear();
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