using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class Circulate
{
    public static void CheckOutItem(Patron patron, Item item)
    {
        //if (GetItemCircStatus == "In")
        //{
        //    AddToActiveLoans(patron, item);
        //}
        //else
        //{
        //    Console.WriteLine("This item is unavailable to check out at this time");
        //}
        
        //code here
    }

    public static void CheckInItem(Patron patron, Item item)
    {    


    }

    public static void AddToActiveLoans(Patron patron, Item item)
    {
        //code 
    }

    public static void RemoveFromActiveLoans(Patron patron, Item item)
    {
        //code
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
  
