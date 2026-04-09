using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class ItemRecordMenu
{
    public static void DisplayBasicItemRecordInfo(Item item)
    {
        Console.WriteLine($"Title:  {item.PrintTitle()}");
        Console.WriteLine($"Item#:  {item.ItemNumber}");
        Console.WriteLine($"Status: {item.CircStatus.ToString().ToUpper()}");
    }

    public static void DisplayFullItemRecordInfo(Item item)
    {
        Console.WriteLine($"Item#: \t\t{item.ItemNumber}");
        Console.WriteLine($"Title: \t\t{item.PrintTitle()}");
        Console.WriteLine($"Author: \t{item.PrintAuthor()}");
        Console.WriteLine($"Format: \t{item.Format.ToString().ToUpper()}");
        Console.WriteLine($"Collection: \t{item.Genre.ToString().ToUpper()}");
        Console.WriteLine($"Borrower ID: \t{item.CurrentBorrowerID}");
    }

    public static async Task MenuLoop(Item item, HttpClient client, Processes session)
    {
        bool returnToCatalog = false;


        while (!returnToCatalog)
        {
            Console.WriteLine("""

                ITEM RECORD
                ===========

                """);

            DisplayBasicItemRecordInfo(item);

            Console.WriteLine("""

            OPTIONS
            1. Display Full Item Record
            2. Check Out To Patron
            3. Return To Catalog

            """);

            Console.Write("Please select an option: ");

            string? userChoice = Console.ReadLine();
        
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("""

                    FULL ITEM INFORMATION
                    =====================
                    """);

                    DisplayFullItemRecordInfo(item);
                    UserActions.PressKeyToContinue();
                    break;

                case "2":
                    Console.WriteLine("");
                    
                    if (!Circulate.IsItemAvailable(item))
                    {
                        Console.WriteLine("This item is not available to check out at this time.");
                        
                        UserActions.PressKeyToContinue();
                        break;
                    }

                    try
                    {
                        string? patronIDToCheckOutTo = PatronGetActions.GetPatronIDFromUser();
                        
                        Console.WriteLine("Loading patron...");
                        
                        Patron? patronToCheckOutTo = await PatronHttpActions.GetPatronByID(patronIDToCheckOutTo, client, session.JsonOptions);
                    
                        if (patronToCheckOutTo == null)
                        {   
                            Console.WriteLine("This id is not tied to an existing patron.");
                            break;
                        }
                        
                        if (Circulate.CheckOutItem(patronToCheckOutTo, item))
                        {
                            Console.WriteLine($"Checked out \"{item.PrintTitle()}\" to {patronToCheckOutTo.PrintPatronName()}.");
                            
                            await PatronHttpActions.PutPatron(patronToCheckOutTo, client, session.JsonOptions);
                            await ItemHttpActions.PutItem(item, client, session.JsonOptions);
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
                    returnToCatalog = true;
                    break;
                    
            }
        }
    }
}