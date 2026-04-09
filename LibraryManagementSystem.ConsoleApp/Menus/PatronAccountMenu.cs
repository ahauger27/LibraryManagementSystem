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
        Console.WriteLine($"First Name: \t{patron.FirstName.ToUpper()}");
        Console.WriteLine($"Middle Initial: {patron.MiddleInitial.ToUpper()}");
        Console.WriteLine($"Last Name: \t{patron.LastName.ToUpper()}");
        Console.WriteLine($"Date of Birth: \t{patron.DateOfBirth}");
        Console.WriteLine($"Address: \t{patron.Address.ToUpper()}");
        Console.WriteLine($"Email Address: \t{patron.Email.ToLower()}");
        Console.WriteLine($"Phone Number: \t{patron.PhoneNumber}");     
    }

    public static async Task MenuLoop(Patron patron, HttpClient client, Processes session)
    {
        Console.Clear();
        
        bool returnToPatronMenu = false;

        while (!returnToPatronMenu)
        {
            Console.WriteLine("""

            PATRON RECORD
            =============

            """);

            DisplayBasicPatronAccountInfo(patron);

            Console.WriteLine("OPTIONS");
            Console.WriteLine("1. Display Full Patron Record");
            Console.WriteLine("2. Check Out Item");
            Console.WriteLine("3. View Active Loans");
            Console.WriteLine("4. Update Account Information");
            Console.WriteLine("5. Delete Patron Account");
            Console.WriteLine("6. Return To Patron Menu");

            Console.WriteLine("");
            Console.Write("Please select an option: ");

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
                    Console.WriteLine("");
                    
                    try
                    {
                        string? itemNumberToCheckOut = ItemGetActions.GetItemNumberFromUser();
                        
                        Console.WriteLine("Loading item...");
                        
                        Item itemToCheckOut = await ItemHttpActions.GetItemByID(itemNumberToCheckOut, client, session.JsonOptions);
                    
                        if (itemToCheckOut == null)
                        {   
                            Console.WriteLine("This item number is not tied to an existing item.");
                            break;
                        }
                        
                        if (Circulate.CheckOutItem(patron, itemToCheckOut))
                        {
                            Console.WriteLine($"Checked out \"{itemToCheckOut.Title.ToUpper()}\" to {patron.PrintPatronName()}.");
                            
                            await PatronHttpActions.PutPatron(patron, client, session.JsonOptions);
                            await ItemHttpActions.PutItem(itemToCheckOut, client, session.JsonOptions);
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong, try again.");
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "3":
                    Console.WriteLine($"""
                    
                    {patron.PrintPatronName()}'s ACTIVE LOANS
                    
                    ITEM#   TITLE
                    =========================================
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
                    Console.Clear();
                    returnToPatronMenu = true;
                    break;

                default:
                    Console.WriteLine("INVALID");
                    break;
            }
        }
    }
}

