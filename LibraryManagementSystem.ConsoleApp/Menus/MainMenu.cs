using LibraryManagementSystem.ConsoleApp.Services;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class MainMenu
{
    public static async Task MenuLoop(Processes session, HttpClient client)
    {

        while (session.RunStatus == true)
        {
            Console.WriteLine("""

            MAIN MENU
            =========

            OPTIONS
            1. Patron Viewer
            2. Catalog Viewer
            3. Check In Item
            4. Quit program
            
            """);
            
            Console.Write("Please select an option: ");
            
            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    await PatronsMenu.MenuLoop(client, session);
                    break;

                case "2":
                    await CatalogMenu.MenuLoop(session, client);
                    break;
                
                case "3":
                    Console.Write("Enter item number: ");
                    string? itemNumberToCheckIn = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(itemNumberToCheckIn))
                    {
                        Console.WriteLine("INVALID INPUT");
                        Console.Write("Enter 5 digit item number (xxxxx): ");
                        continue;
                    }
                    
                    Item itemToCheckIn = await ItemHttpActions.GetItemByID(itemNumberToCheckIn, client, session.JsonOptions);
                    
                    if (itemToCheckIn == null)
                    {   
                        Console.WriteLine("Item doesn't exist");
                        break;
                    }

                    //Check if item is already marked in

                    string? patronIDToUpdate = itemToCheckIn.CurrentBorrowerID;

                    Patron? patronAccountToUpdate = await PatronHttpActions.GetPatronByID(patronIDToUpdate, client, session.JsonOptions);

                    if (patronAccountToUpdate == null)
                    {   
                        Console.WriteLine("Patron doesn't exist");
                        break;
                    }

                    Circulate.CheckInItem(patronAccountToUpdate, itemToCheckIn);

                    await PatronHttpActions.PutPatron(patronAccountToUpdate, client, session.JsonOptions);
                    await ItemHttpActions.PutItem(itemToCheckIn, client, session.JsonOptions);
                    break;

                case "4":
                    Console.WriteLine("Shutting down program...");
                    session.End();
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: Please enter 1, 2, or 3.");
                    UserActions.PressKeyToContinue();
                    Console.Clear();
                    break;
            }
        } 
    }
}