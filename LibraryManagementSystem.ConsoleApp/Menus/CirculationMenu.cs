using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp.Menus;

public static class CirculationMenu
{
    public static async Task MenuLoop(HttpClient client, Processes session)
    {
        bool returnToPreviousMenu = false;

        while (!returnToPreviousMenu)
        {
            Console.WriteLine("""

            CIRCULATION OPTIONS
            ===================

            """);

            Console.WriteLine("1. Check In Item");
            Console.WriteLine("2. Check Out Item");
            Console.WriteLine("3. Return To Previous Menu");

            Console.WriteLine("");
            Console.Write("Please Select An Option: ");

            string? userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("ITEM CHECK IN");
                    Console.Write("Enter Item Number: ");
                    string? itemNumberToCheckIn = Console.ReadLine();

                    if (string.IsNullOrEmpty(itemNumberToCheckIn))
                    {
                        Console.WriteLine($"Please Enter The Item Number");
                        break;
                    }

                    Item item = await ItemHttpActions.GetItemByID(itemNumberToCheckIn, client, session.JsonOptions);
                    if (item != null)
                    {
                        Circulate.CheckInItem(item);
                        
                    }
                    break;

                case "2":
                    Console.WriteLine("FEATURE IN PROGRESS");
                    break;
                
                case "3":
                    Console.WriteLine("FEATURE IN PROGRESS");
                    break;

                default:
                    Console.WriteLine("INVALID INPUT");
                    break;
            }
        }
    }

    public static async Task CheckInMenu(HttpClient client, Processes session)
    {
        bool returnToPreviousMenu = false;

        while (!returnToPreviousMenu)
        {
            Console.WriteLine("");
            Console.Write("CHECK IN: Enter Item Number: ");
            string? itemNumberToCheckIn = Console.ReadLine();

            if (string.IsNullOrEmpty(itemNumberToCheckIn))
            {
                Console.WriteLine("INVALID INPUT");
                Console.Write("Enter Item Number: ");
                continue;
            }

            Item item = await ItemHttpActions.GetItemByID(itemNumberToCheckIn!, client, session.JsonOptions);
            if (item == null)
            {   
                Console.WriteLine("LOL didn't work");
                break;
            }

            try
            {
                Circulate.CheckInItem(item);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static async Task CheckOutMenu(HttpClient client, Processes session)
    {
        bool returnToPreviousMenu = false;
        
        // while (!returnToPreviousMenu)
        // {
        //     Console.WriteLine("CHECKING OUT");

            

            

        //     try
        //     {
        //         Circulate.CheckInItem(item);
        //     }
        //     catch (NullReferenceException ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        // }
    }
}
  
