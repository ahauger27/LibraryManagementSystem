using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class Circulate
{
    public static bool CheckOutItem(Patron patron, Item item)
    {
        if (item.CircStatus != CircStatus.In)
        {
           Console.WriteLine("This item is unavailable to check out at this time.");
           Console.WriteLine($"Item Status: {item.CircStatus}");
           return false;
        }
        else
        {
            AddToActiveLoans(patron, item);
            item.CircStatus = CircStatus.Out;
            item.CurrentBorrowerID = patron.PatronID;
            return true;
        }
    }

    public static void CheckInItem(Patron patron, Item itemToCheckIn)
    {
        if (itemToCheckIn.CircStatus == CircStatus.In)
        {
            Console.WriteLine("This item was already marked 'In'");
        }
        
        itemToCheckIn.CircStatus = CircStatus.In;

        // if (itemToCheckIn.CurrentBorrowerID != null)
        // {
        // }

        RemoveFromActiveLoans(patron, itemToCheckIn.ItemNumber);
        itemToCheckIn.CurrentBorrowerID = null;
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
        }
    }

    public static void RemoveFromActiveLoans(Patron patron, string itemNumber)
    {
        var itemToRemove = patron.ActiveLoans.FirstOrDefault(i => i.ItemNumber == itemNumber);
        
        if (itemToRemove != null)
        {
            patron.ActiveLoans.Remove(itemToRemove);
        }
    }

    public static bool IsItemAvailable(Item item)
    {
        if (item.CircStatus != CircStatus.In)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
  
