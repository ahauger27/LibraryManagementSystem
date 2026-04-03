namespace LibraryManagementSystem.Common.Models;

public class DateOfBirth
{
    public static DateTime DateInput()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's DATE OF BIRTH (MM/DD/YYYY): ");

        do
        {
            string? input = UserActions.StringInput();
            if (!IsValid(input))
            {
                Console.Write("INVALID INPUT. Please input a proper date in the format MM/DD/YYYY: ");
            }
            else
            {
                DateTime dateOfBirth = DateTime.Parse(input);
                return dateOfBirth;
            }
        } while (true);
    }

    public static bool IsValid(string date)
    {
        DateTime.TryParse(date, out DateTime tempObject);
        if (tempObject > DateTime.Now || tempObject == default)
        {
            return false;
        }
        else
        {
            return true; 
        }
    }
}