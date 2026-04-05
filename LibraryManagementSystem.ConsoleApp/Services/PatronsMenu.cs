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

            PATRONS MENU
            ============

            """);

            Console.WriteLine("1. List All Patrons");
            Console.WriteLine("2. Search Patrons By ID (WIP)");
            Console.WriteLine("3. Create New Patron (WIP)");
            Console.WriteLine("4. Delete Patron (WIP)");
            Console.WriteLine("5. Update Patron Account (WIP)");
            Console.WriteLine("6. Return To Main Menu");

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

                    // Ask if user wants to select a patron from list
                    Console.WriteLine("");
                    Console.WriteLine("1. Select Patron By ID");
                    Console.WriteLine("2. Return To Patron Menu");

                    Console.WriteLine("");
                    Console.Write("Please Select An Option: ");
                    userChoice = Console.ReadLine();

                    if (!string.IsNullOrEmpty(userChoice))
                    {
                        switch (userChoice)
                        {
                            case "1":
                                //Select By ID
                                break;
                            case "2":
                                Console.WriteLine("Returning To Patron Menu...");
                                break;
                        }
                    }
                    break;

                case "2":
                    Console.Write("Enter patron's ID: ");
                    string? idToSearchString = UserActions.StringInput();

                    if (int.TryParse(idToSearchString, out int idToSearch))
                    {   
                        Console.WriteLine("Finding patron...");
                        Patron patron = await PatronHttpActions.GetPatronByID(idToSearch, client, run.JsonOptions);
                        
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
                
                case "3":
                    Console.WriteLine("MENU TO ADD A NEW PATRON");

                    Patron newPatron = PatronActions.CreateNewPatron();
                    
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
                    Console.WriteLine("MENU TO DELETE A PATRON");

                    Console.Write("Enter the ID of the patron you want to delete: ");
                    string? idToDeleteString = Console.ReadLine();
                    if (int.TryParse(idToDeleteString, out int idToDelete))
                    {
                        await PatronHttpActions.DeletePatron(idToDelete, client);
                    }
                    break;

                case "5":
                    Console.WriteLine("MENU TO UPDATE A PATRON");
                            
                    Console.Write("Enter the ID of the patron to update: ");
                    string? idToUpdateString = UserActions.StringInput();
                    
                    if (int.TryParse(idToUpdateString, out int idToUpdate))
                    {
                        Patron patronToUpdate = await PatronHttpActions.GetPatronByID(idToUpdate, client, run.JsonOptions);
                    
                        string fieldNumber = PatronActions.UpdatePatronInfo(patronToUpdate);
                        Console.Write("Enter the updated information: ");
                        string? input = UserActions.StringInput();

                        PatronActions.ApplyPatronUpdate(patronToUpdate, fieldNumber, input);
                    
                        await PatronHttpActions.PutPatron(idToUpdate, patronToUpdate, client, run.JsonOptions);
                    }
                    break;

                case "6":
                    Console.WriteLine("Returning to Main Menu...");
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