namespace LibraryManagementSystem.Common.Models;

public class DateOfBirth
{
    public static bool IsValid(string date)
    {
        DateTime.TryParse(date, out DateTime tempObject);
        if (tempObject < DateTime.Now)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
}