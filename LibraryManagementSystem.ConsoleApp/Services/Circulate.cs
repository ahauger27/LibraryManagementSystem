using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class Circulate
{
    public static void CheckOutItem(Item item)
    {
        if (item.CircStatus != CircStatus.In)
        {
           Console.WriteLine("This item is unavailable to check out at this time.");
           Console.WriteLine($"Item Status: {item.CircStatus}");
        }
    }

    public static void CheckInItem(Item item)
    {    
        item.CircStatus = CircStatus.In;
    }

    public static void AddToActiveLoans(Patron patron, Item item)
    {
        if (item.CircStatus != CircStatus.In)
        {
            Console.WriteLine("This item is not available.");
            return;
        }
        else
        {
            patron.ActiveLoans.Add(item);
            Console.WriteLine("Added");
        }
    }

    public static void RemoveFromActiveLoans(Patron patron, Item item)
    {
        if (patron.ActiveLoans.Contains(item))
        {
            patron.ActiveLoans.Remove(item);
        }
        else
        {
            Console.WriteLine("This item is not in the patron's active loans");
        }
    }

    public static bool IsItemAvailable(Item item)
    {
        if (item.CircStatus == CircStatus.In)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
  
