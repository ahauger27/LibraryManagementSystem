using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class ItemRecordMenu
{
    public static void DisplayBasicItemRecordInfo(Item item)
    {
        Console.WriteLine($"Title: {item.PrintTitle()} | Author: {item.PrintAuthor()}");
        Console.WriteLine($"Item #: {item.ItemNumber} | Current Status: {item.CircStatus}");
    }

    public static void DisplayFullItemRecordInfo(Item item)
    {
        Console.WriteLine($"Title: {item.PrintTitle()}");
        Console.WriteLine($"Author: {item.PrintTitle()}");
        Console.WriteLine($"Title: {item.PrintTitle()}");
        Console.WriteLine($"Title: {item.PrintTitle()}");
    }

    public static async Task MenuLoop(Item item, HttpClient client, Processes session)
    {
        bool returnToCatalog = false;

        Console.WriteLine("""

            ITEM RECORD
            ===========

            """);

        while (!returnToCatalog)
        {
            Console.WriteLine("");
            DisplayBasicItemRecordInfo(item);

            Console.WriteLine("");
            Console.WriteLine("1. Display Item Record Details");
            Console.WriteLine("2. Check Out To Patron");
            Console.WriteLine("3. Return To Catalog");

            Console.WriteLine("");
            Console.Write("Please Make A Selection: ");

            string? userChoice = Console.ReadLine();
        
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("""
                    FULL ITEM INFORMATION
                    =====================
                    """);

                    DisplayFullItemRecordInfo(item);
                    break;

                case "2":
                    Console.WriteLine("""
                    CHECK OUT
                    =========
                    """);
                    
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