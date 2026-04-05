using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronAccountMenu
{
    public static void DisplayBasicPatronAccountInfo(Patron patron)
    {
        Console.WriteLine($"Name: {patron.PrintPatronName()} | Patron ID: {patron.PatronID}");    
    }

    public static void DisplayFullPatronAccountInfo(Patron patron)
    {
        Console.WriteLine($"FIRST NAME: {patron.FirstName}");
        Console.WriteLine($"MIDDLE INITIAL: {patron.MiddleInitial}");
        Console.WriteLine($"LAST NAME: {patron.LastName}");
        Console.WriteLine($"DATE OF BIRTH: {patron.DateOfBirth}");
        Console.WriteLine($"ADDRESS: {patron.Address}");
        Console.WriteLine($"EMAIL ADDRESS: {patron.Email}");
        Console.WriteLine($"PHONE NUMBER: {patron.PhoneNumber}");     
    }

    public static async Task MenuLoop(Patron patron, HttpClient client, Processes session)
    {
        
        bool returnToPatronMenu = false;

        while (!returnToPatronMenu)
        {
            Console.WriteLine("""

                PATRON ACCOUNT MENU
                ===================

                """);

            DisplayBasicPatronAccountInfo(patron);

            Console.WriteLine("");
            Console.WriteLine("1. Display Patron Registration Details");
            Console.WriteLine("2. View Active Loans");
            Console.WriteLine("3. Update Patron Account");
            Console.WriteLine("4. Delete Patron Account");
            Console.WriteLine("5. Return To Patron Menu");

            Console.WriteLine("");
            Console.Write("Please Make A Selection: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine($"{Environment.NewLine}ACCOUNT INFORMATION");
                    DisplayFullPatronAccountInfo(patron);
                    UserActions.PressKeyToContinue();
                    break;

                case "2":
                    Console.WriteLine($"{Environment.NewLine}ACTIVE LOANS:");
                    patron.DisplayActiveLoans();
                    UserActions.PressKeyToContinue();
                    break;

                case "3":
                    Console.WriteLine($"{Environment.NewLine}UPDATING PATRON INFO:");
                            
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

                case "4":
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
                
                case "5":
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

