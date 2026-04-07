using LibraryManagementSystem.ConsoleApp.Services;

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
            
            Console.WriteLine("1. Open Patrons Menu");
            Console.WriteLine("2. Open Catalog (WIP)");
            Console.WriteLine("3. Quit program (WIP)");

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
                    Console.WriteLine("Shutting Down Program...");
                    session.End();
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: Please enter 1, 2, or 3.");
                    UserActions.PressKeyToContinue();
                    break;
            }
        } 
    }
}