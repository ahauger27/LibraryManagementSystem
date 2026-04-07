using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class CatalogMenu
{
    public static async Task MenuLoop(Processes session, HttpClient client)
    {
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            Console.WriteLine("""

            CATALOG MENU
            ============

            """);

            Console.WriteLine("1. View All Items in Catalog");
            Console.WriteLine("2. Search Item By Item Number");
            Console.WriteLine("3. Return To Main Menu");

            Console.WriteLine("");
            Console.Write("Please Select An Option");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("Getting Catalog...");

                    string itemJson = await ItemHttpActions.GetItems(client);
                    List<Item> itemList = await ItemGetActions.CreateItemsListFromApi(itemJson, session.JsonOptions);
                    ItemGetActions.DisplayAllItems(itemList);
                    break;

                case "2":
                    throw new NotImplementedException();
                    break;

                case "3":
                    Console.WriteLine("Returning To Main Menu...");
                    returnToMainMenu = true;
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: ");
                    UserActions.PressKeyToContinue();
                    break;
            }
        }
    }
}