using System.Runtime.InteropServices;
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
        bool returnToPatronMenu = false;

        while (!returnToPatronMenu)
        {
            Console.Clear();
            Console.WriteLine($"""

            PATRON RECORD
            =============

            """);

            DisplayBasicPatronAccountInfo(patron);

            Console.WriteLine("""
            
            OPTIONS
            1. Display Full Patron Record
            2. Check Out Item
            3. View Active Loans
            4. Update Account Information
            5. Delete Patron Account
            6. Return To Patron Menu

            """);
            
            Console.Write("Please select an option: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.Clear();

                    Console.WriteLine($"""

                    ACCOUNT DETAILS FOR PATRON #{patron.PatronID}

                    """);

                    DisplayFullPatronAccountInfo(patron);
                    UserActions.PressKeyToContinue();

                    Console.Clear();
                    break;

                case "2":
                    Console.WriteLine("");
                    
                    try
                    {
                        string? itemNumberToCheckOut = ItemGetActions.GetItemNumberFromUser();

                        if (Circulate.DoesPatronAlreadyHaveItem(patron, itemNumberToCheckOut))
                        {
                            Console.WriteLine($"{patron.PrintPatronName()} already has this item on loan.");
                            UserActions.PressKeyToContinue();
                            Console.Clear();
                            break;
                        }
                        
                        Console.WriteLine("Loading item...");
                        
                        Item itemToCheckOut = await ItemHttpActions.GetItemByID(itemNumberToCheckOut, client, session.JsonOptions);
                    
                        if (itemToCheckOut == null)
                        {   
                            UserActions.PressKeyToContinue();
                            break;
                        }
                    
                        Circulate.CheckOutItem(patron, itemToCheckOut);

                        if (patron.ActiveLoans.Contains(itemToCheckOut))
                        {
                            Console.WriteLine($"Checked out \"{itemToCheckOut.Title.ToUpper()}\" to {patron.PrintPatronName()}.");
                            
                            await PatronHttpActions.PutPatron(patron, client, session.JsonOptions);
                            await ItemHttpActions.PutItem(itemToCheckOut, client, session.JsonOptions);
                        }
                        else
                        {
                            Console.WriteLine($"\"{itemToCheckOut.Title.ToUpper()}\" could not be checked out.");

                        }

                        // if (itemToCheckOut.CurrentBorrowerID == patron.PatronID)
                        // {
                                
                        // }
                        // else
                        // {
                        //     Console.WriteLine("No");
                        // }
                    
                        UserActions.PressKeyToContinue();
                        break;
                    
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "3":
                    Console.Clear();
                    
                    if (patron.ActiveLoans.Count == 0)
                    {
                        Console.WriteLine($"{patron.PrintPatronName()} has no current loans.");
                    }
                    else
                    {
                        Console.WriteLine($"""
                        
                        {patron.PrintPatronName()}'s ACTIVE LOANS
                        
                        ITEM#   TITLE
                        =========================================
                        """);
                        patron.DisplayActiveLoans();
                    }
                    
                    UserActions.PressKeyToContinue();
                    Console.Clear();
                    break;

                case "4":
                    Console.Clear();

                    Console.WriteLine("""
                    
                    UPDATING PATRON INFORMATION
                    ===========================
                    """);
                            
                    string? fieldNumber = PatronPutActions.ChoosePatronInfoToUpdate(patron);
                    
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

                    // Check if successful
                    //if (success)

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

