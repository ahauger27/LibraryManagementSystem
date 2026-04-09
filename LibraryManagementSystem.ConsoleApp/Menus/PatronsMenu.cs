using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class PatronsMenu
{
    public static async Task MenuLoop(HttpClient client, Processes session)
    {
        Console.Clear();

        bool returnToPreviousMenu = false;

        while (!returnToPreviousMenu)
        {
            Console.WriteLine("""

            PATRON VIEWER
            =============

            1. View All Patrons
            2. Search Patron By ID
            3. Create New Patron Record
            4. Return To Main Menu

            """);

            Console.Write("Please Select An Option: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1": 
                    Console.WriteLine("Getting all patrons...");

                    string patronJson = await PatronHttpActions.GetPatrons(client);
                    List<Patron> patronList = await PatronGetActions.CreatePatronsListFromApi(patronJson, session.JsonOptions);
                    PatronGetActions.DisplayAllPatrons(patronList);

                    Console.WriteLine("""
                    
                    OPTIONS
                    1. View Single Patron Record
                    2. Return To Patron Viewer

                    """);

                    Console.Write("Please select an option: ");

                    string? userChoice2 = Console.ReadLine();

                    switch (userChoice2)
                    {
                        case "1":
                            string? patronIDToSelect = PatronGetActions.GetPatronIDFromUser();

                            Patron? patronToSelect = await PatronGetActions.TryToLoadPatronAccount(patronIDToSelect, client, session);

                            if (patronToSelect == null)
                            {
                                Console.WriteLine($"The ID \"{patronIDToSelect}\" is not tied to an existing patron.");
                                Console.WriteLine("Returning to menu..."); 
                                UserActions.PressKeyToContinue();
                                Console.Clear();  
                            }
                            else
                            {
                                await PatronAccountMenu.MenuLoop(patronToSelect, client, session);
                            }
                            break;

                        default:
                            Console.Clear();
                            break;

                    }
                    break;

                case "2":
                    string? patronIDToSearch = PatronGetActions.GetPatronIDFromUser();

                    Patron? patronToSearch = await PatronGetActions.TryToLoadPatronAccount(patronIDToSearch, client, session);

                    if (patronToSearch == null)
                    {
                        Console.WriteLine($"The ID \"{patronIDToSearch}\" is not tied to an existing patron.");
                        Console.WriteLine("Returning to menu..."); 
                        UserActions.PressKeyToContinue();
                        Console.Clear();  
                    }
                    else
                    {
                        await PatronAccountMenu.MenuLoop(patronToSearch, client, session);
                    }
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("CREATING NEW PATRON RECORD");

                    Patron newPatron = PatronPostActions.CreateNewPatron();
                    
                    if (newPatron != null)
                    {
                        await PatronHttpActions.PostNewPatron(newPatron, client);
                        Console.WriteLine("Opening patron record...");
                        UserActions.PressKeyToContinue();
                        await PatronAccountMenu.MenuLoop(newPatron, client, session);
                    }
                    else
                    {
                        Console.WriteLine("Unable to create patron account. Returning to main menu...");
                    }   
                    break; 

                case "4":                   
                    returnToPreviousMenu = true;
                    Console.Clear();
                    break;

                default:
                    Console.Write("INVALID OPTION: Please enter 1, 2, 3, or 4: ");
                    continue;
                
            }
        }
    }
}