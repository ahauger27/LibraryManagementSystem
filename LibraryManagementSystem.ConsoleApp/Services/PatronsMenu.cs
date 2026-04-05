using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronsMenu
{
    public static async Task MenuLoop(HttpClient client, Processes run)
    {
        bool returnToMainMenu = false;

        while (!returnToMainMenu)
        {
            Console.WriteLine("""

            PATRON SEARCH MENU
            ==================

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
                    List<Patron> patronList = await PatronGetActions.CreatePatronsListFromApi(patronJson, run.JsonOptions);
                    PatronGetActions.DisplayAllPatrons(patronList);

                    Console.WriteLine("");
                    Console.WriteLine("1. Select Patron By ID");
                    Console.WriteLine("2. Return To Patron Menu");

                    Console.WriteLine("");
                    Console.Write("Please Select An Option: ");
                    
                    userChoice = Console.ReadLine();

                    if (string.IsNullOrEmpty(userChoice))
                    {
                        Console.WriteLine("invalid");
                        break;
                    }

                    switch (userChoice)
                    {
                        case "1":
                            Console.Write("Enter Patron's ID: ");
                            string? idToSelect = UserActions.StringInput();

                            if (int.TryParse(idToSelect, out int idSelection))
                            {   
                                Console.WriteLine("Finding Patron...");
                                Patron patron = await PatronHttpActions.GetPatronByID(idSelection, client, run.JsonOptions);
                                
                                if (patron != null)
                                {
                                    Console.WriteLine(patron.FullName());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID format.");
                            }
                            break;
            
                        case "2":
                            Console.WriteLine("Returning To Patron Menu...");
                            break;

                    }
                    break;

                case "2":
                    //
                    //
                    Console.Write("Enter Patron's ID: ");
                    string? idToSearchString = UserActions.StringInput();

                    if (int.TryParse(idToSearchString, out int idToSearch))
                    {   
                        Console.WriteLine("Loading Patron...");

                        Patron patron = await PatronHttpActions.GetPatronByID(idToSearch, client, run.JsonOptions);
                        
                        if (patron != null)
                        {
                            await PatronAccountMenu.MenuLoop(patron, client);
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
                    //
                    //
                case "3":
                    Console.WriteLine("MENU TO ADD A NEW PATRON");

                    Patron newPatron = PatronPostActions.CreateNewPatron();
                    
                    if (newPatron != null)
                    {
                        Console.WriteLine("Patron account created!");
                        await PatronHttpActions.PostNewPatron(newPatron, client);
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
                    Console.WriteLine("INVALID OPTION: Please enter 1, 2, 3, 4, 5, or 6");
                    UserActions.PressKeyToContinue();
                    break;
                
            }
        }
    }
}