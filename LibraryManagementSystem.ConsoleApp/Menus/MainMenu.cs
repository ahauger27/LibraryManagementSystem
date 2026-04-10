using LibraryManagementSystem.ConsoleApp.Services;
using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class MainMenu
{
    public static async Task MenuLoop(Processes session, HttpClient client)
    {

        while (session.RunStatus == true)
        {
            Console.Clear();
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
                    try
                    {
                        string? itemNumberToCheckIn = ItemGetActions.GetItemNumberFromUser();
                        
                        Item itemToCheckIn = await ItemHttpActions.GetItemByID(itemNumberToCheckIn, client, session.JsonOptions);
                    
                        if (!ItemGetActions.DoesItemExist(itemToCheckIn))
                        {
                            Console.WriteLine($"The item # {itemNumberToCheckIn} is not tied to an existing record.");
                            UserActions.PressKeyToContinue();
                            break;
                        }
   
                        if (itemToCheckIn.CircStatus == CircStatus.In)
                        {
                            Console.WriteLine("This item is already marked as \"IN\"");
                            UserActions.PressKeyToContinue();
                            break;
                        }

                        if (itemToCheckIn.CurrentBorrowerID == null)
                        {
                            itemToCheckIn.CircStatus = CircStatus.In;
                            break;
                        }
                        
                        string? patronIDToUpdate = itemToCheckIn.CurrentBorrowerID;

                        Patron? patronAccountToUpdate = await PatronGetActions.TryToLoadPatronAccount(patronIDToUpdate, client, session);

                        if (patronAccountToUpdate != null)
                        {   
                            Circulate.CheckInItem(patronAccountToUpdate, itemToCheckIn);

                            await PatronHttpActions.PutPatron(patronAccountToUpdate, client, session.JsonOptions);
                            await ItemHttpActions.PutItem(itemToCheckIn, client, session.JsonOptions);

                            Console.WriteLine($"Item #{itemToCheckIn.ItemNumber} checked in.");

                            UserActions.PressKeyToContinue();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "4":
                    Console.WriteLine("Shutting down program...");
                    session.End();
                    break;

                default:
                    Console.Write("INVALID OPTION: Please enter 1, 2, or 3");
                    UserActions.PressKeyToContinue();
                    Console.Clear();
                    break;
            }
        } 
    }
}