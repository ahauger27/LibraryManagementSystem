using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class CatalogMenu
{
    public static async Task MenuLoop(Processes session, HttpClient client)
    {
        bool returnToMainMenu = false;

        Console.WriteLine("""

        CATALOG VIEWER
        ==============

        """);

        Console.WriteLine("1. View All Items in Catalog");
        Console.WriteLine("2. Search By Item Number");
        Console.WriteLine("3. Return To Main Menu");

        while (!returnToMainMenu)
        {
            Console.WriteLine("");
            Console.Write("Please Select An Option");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("Loading Catalog...");

                    string itemJson = await ItemHttpActions.GetItems(client);
                    List<Item> itemList = await ItemGetActions.CreateItemsListFromApi(itemJson, session.JsonOptions);
                    ItemGetActions.DisplayAllItems(itemList);
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
                    Console.WriteLine("Returning To Main Menu...");
                    returnToMainMenu = true;
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: Please Enter 1, 2, or 3");
                    break;
            }
        }
    }
}