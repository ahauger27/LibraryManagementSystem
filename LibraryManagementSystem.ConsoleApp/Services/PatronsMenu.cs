using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronsMenu
{
    public static async Task MenuLoop(HttpClient client, Processes session)
    {
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            Console.WriteLine("""

            PATRON MENU
            ===========

            """);

            Console.WriteLine("1. View All Patrons");
            Console.WriteLine("2. Search Patron By ID (WIP)");
            Console.WriteLine("3. Create New Patron (WIP)");
            Console.WriteLine("4. Return To Main Menu");

            Console.WriteLine("");
            Console.Write("Please Select An Option: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1": 
                    Console.WriteLine("Getting All Patrons...");
                    UserActions.PressKeyToContinue();

                    string patronJson = await PatronHttpActions.GetPatrons(client);
                    List<Patron> patronList = await PatronGetActions.CreatePatronsListFromApi(patronJson, session.JsonOptions);
                    PatronGetActions.DisplayAllPatrons(patronList);
                    break;

                case "2":
                    Console.Write("Enter Patron ID: ");
                    string? idToSearchString = UserActions.StringInput();

                    if (int.TryParse(idToSearchString, out int idToSearch))
                    {   
                        Console.WriteLine("Loading Patron...");

                        Patron patron = await PatronHttpActions.GetPatronByID(idToSearch, client, session.JsonOptions);
                        
                        if (patron != null)
                        {
                            await PatronAccountMenu.MenuLoop(patron, client, session);
                        }
                        else
                        {
                            Console.WriteLine("Patron Does Not Exist...");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format.");
                    }
                    break;

                case "3":
                    Console.WriteLine("MENU TO ADD A NEW PATRON");

                    Patron newPatron = PatronPostActions.CreateNewPatron();
                    
                    if (newPatron != null)
                    {
                        await PatronHttpActions.PostNewPatron(newPatron, client);
                        Console.WriteLine("Patron Account Created!");
                        
                        await PatronAccountMenu.MenuLoop(newPatron, client, session);
                    }
                    else
                    {
                        Console.WriteLine("Unable to create patron account. Returning to main menu...");
                    }   
                    break; 

                case "4":
                    Console.WriteLine("Returning To Main Menu...");
                    returnToMainMenu = true;
                    break;

                default:
                    Console.WriteLine("INVALID OPTION: Please enter 1, 2, 3, or 4");
                    UserActions.PressKeyToContinue();
                    break;
                
            }
        }
    }
}