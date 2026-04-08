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

            """);
            
            Console.WriteLine("1. View Patrons");
            Console.WriteLine("2. View Catalog (WIP)");
            Console.WriteLine("3. Check Out (WIP)" );
            Console.WriteLine("4. Check In Item");
            Console.WriteLine("5. Quit program (WIP)");
            
            Console.WriteLine("");
            Console.Write("Please select an option: ");
            
            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    await PatronsMenu.MenuLoop(client, session);
                    break;

                case "2": // 2. Open Catalog (WIP)
                    Console.WriteLine("CATALOG");
                    Console.WriteLine("This feature is still in progress");
                    
                    await CatalogMenu.MenuLoop(session, client);
                    Console.WriteLine("Returning to Main Menu...");
                    UserActions.PressKeyToContinue();
                    break;
                
                case "3":
                    Console.WriteLine("CHECK OUT");

                    
                    break;

                case "4":
                    Console.WriteLine("CHECKING IN");

                    //CirculationMenu.CheckInMenu();

                    Console.Write("Enter Item Number: ");
                    string? itemNumberToCheckIn = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(itemNumberToCheckIn))
                    {
                        Console.WriteLine("INVALID INPUT");
                        Console.Write("Enter Item Number: ");
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

                case "5":
                    Console.WriteLine("Shutting Down Program...");
                    session.End();
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: Please enter 1, 2, or 3.");
                    break;
            }
        } 
    }
}