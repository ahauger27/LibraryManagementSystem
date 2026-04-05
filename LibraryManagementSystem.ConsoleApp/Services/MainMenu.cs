using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class MainMenu
{
    public static async Task MenuLoop(Processes run, HttpClient client)
    {


        while (run.RunStatus == true)
        {
            Console.WriteLine("""

            MAIN MENU
            =========

            """);
            
            Console.WriteLine("1. Open Patrons Menu (WIP)");
            Console.WriteLine("2. Open Catalog (WIP)");
            Console.WriteLine("3. Quit program (WIP)");

            Console.WriteLine("");
            Console.Write("Please select an option: ");
            string? userChoice = Console.ReadLine();

            try
            {
                switch (userChoice)
                {
                    case "1": // 1. Open Patrons Menu (WIP)
                        await PatronsMenu.MenuLoop(client, run);
                        break;

                    case "2": // 2. Open Catalog (WIP)
                        Console.WriteLine("CATALOG");
                        Console.WriteLine("This feature is still in progress");
                        Console.WriteLine("Returning to Main Menu...");
                        break;
                   
                    case "3":
                        Console.WriteLine("Shutting down program...");
                        run.End();
                        break;

                    default:
                        Console.WriteLine("INVALID OPTION: Please enter 1, 2, or 3.");
                        UserActions.PressKeyToContinue();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine(ex.Message);
            }
        } 

    }
}