namespace LibraryManagementSystem.Common.Models;

public class DateOfBirth
{
    public static DateOnly DateInput()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's DATE OF BIRTH: ");

        do
        {
            string? input = UserActions.StringInput();
            if (!IsValid(input))
            {
                Console.Write("INVALID INPUT. Please input a proper date in the format MM/DD/YYYY: ");
            }
            else
            {
                DateOnly dateOfBirth = DateOnly.Parse(input);
                return dateOfBirth;
            }
        } while (true);
    }

    public static bool IsValid(string date)
    {
        DateOnly.TryParse
        (
            date, 
            out DateOnly tempObject
        );

        if (tempObject > DateOnly.FromDateTime(DateTime.Now) || tempObject == default)
        {
            return false;
        }
        else
        {
            return true; 
        }
    }
}