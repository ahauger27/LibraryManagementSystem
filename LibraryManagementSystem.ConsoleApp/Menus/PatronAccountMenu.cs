using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class PatronAccountMenu
{
    public static void DisplayBasicPatronAccountInfo(Patron patron)
    {
        Console.WriteLine($"Name: {patron.PrintPatronName()} | Patron ID: {patron.PatronID}");    
    }

    public static void DisplayFullPatronAccountInfo(Patron patron)
    {
        Console.WriteLine($"First Name: \t{patron.FirstName}");
        Console.WriteLine($"Middle Initial: {patron.MiddleInitial}");
        Console.WriteLine($"Last Name: \t{patron.LastName}");
        Console.WriteLine($"Date of Birth: \t{patron.DateOfBirth}");
        Console.WriteLine($"Address: \t{patron.Address}");
        Console.WriteLine($"Email Address: \t{patron.Email}");
        Console.WriteLine($"Phone Number: \t{patron.PhoneNumber}");     
    }

    public static async Task MenuLoop(Patron patron, HttpClient client, Processes session)
    {
        
        bool returnToPatronMenu = false;

        Console.WriteLine("""

            PATRON ACCOUNT MENU
            ===================

            """);

        while (!returnToPatronMenu)
        {
            Console.WriteLine("");
            DisplayBasicPatronAccountInfo(patron);

            Console.WriteLine("");
            Console.WriteLine("1. Display Patron Registration Details");
            Console.WriteLine("2. Check Out Item");
            Console.WriteLine("3. View Active Loans");
            Console.WriteLine("4. Update Account Information");
            Console.WriteLine("5. Delete Patron Account");
            Console.WriteLine("6. Return To Patron Menu");

            Console.WriteLine("");
            Console.Write("Please Make A Selection: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("""

                    ACCOUNT DETAILS
                    ===============
                    """);

                    DisplayFullPatronAccountInfo(patron);
                    UserActions.PressKeyToContinue();
                    break;

                case "2":
                    Console.WriteLine("""
                    CHECK OUT
                    =========
                    """);

                    // throw new NotImplementedException();
                    Console.Write("Enter the Item Number of the Item you wish to check out (XXXXX): ");
                    string? itemNumberToSearch = Console.ReadLine();

                    if (string.IsNullOrEmpty(itemNumberToSearch))
                    {
                        Console.WriteLine("Invalid Item Number.");
                        break;
                    }   

                    Console.WriteLine("Loading Item...");

                    Item item = await ItemHttpActions.GetItemByID(itemNumberToSearch, client, session.JsonOptions);

                    if (item != null)
                    {
                        Circulate.CheckOutItem(patron, item);
                        Console.WriteLine($"SUCCESS: {item.Title} has been checked out to Patron with ID: {patron.PatronID}");
                    }
                    else
                    {
                        Console.WriteLine($"Could Not Find Item Number {itemNumberToSearch}");
                    }
                    break;

                case "3":
                    Console.WriteLine("""
                    ACTIVE LOANS
                    ============
                    """);

                    patron.DisplayActiveLoans();
                    UserActions.PressKeyToContinue();
                    break;

                case "4":
                    Console.WriteLine("""
                    UPDATING PATRON INFORMATION
                    ===========================
                    """);
                            
                    string fieldNumber = PatronPutActions.ChoosePatronInfoToUpdate(patron);
                    
                    if (fieldNumber == null)
                    {
                        break;
                    }

                    Console.Write("Enter the Updated Information: ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }

                    PatronPutActions.ApplyPatronUpdateToAccount(patron, fieldNumber, input);
                    
                    await PatronHttpActions.PutPatron(patron, client, session.JsonOptions);

                    break;

                case "5":
                    Console.WriteLine($"{Environment.NewLine}Delete This Patron Account? ({patron.LastName}, {patron.FirstName})");
                    Console.Write("CONFIRM 'Y/N': ");

                    string? confirmation = Console.ReadLine();

                    if (string.IsNullOrEmpty(confirmation))
                    {
                        Console.WriteLine("Invalid Input: Returning To Menu...");
                    }

                    if (confirmation!.ToUpper() == "Y")
                    {
                        await PatronHttpActions.DeletePatron(patron, client); 
                        Console.WriteLine("Returning To Patron Search Menu");
                        returnToPatronMenu = true;

                        UserActions.PressKeyToContinue();
                    }
                    else
                    {
                        Console.WriteLine("Returning to Patron Search Menu...");
                    }
                    break;
                
                case "6":
                    Console.WriteLine("Returning to Patron Search Menu...");
                    returnToPatronMenu = true;
                    break;

                default:
                    Console.WriteLine("INVALID");
                    break;
            }
        }
    }
}

