namespace LibraryManagementSystem.ConsoleApp.Services;

public class UserActions
{
    public static bool IsInputValid(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            Console.Write("INVALID INPUT. Please Try Again: ");
            return false;
        }
        else
        {
            return true;
        }
    }

    public static string StringInput()
    {
        string? input;
        do
        {
            input = Console.ReadLine();

            if (!IsInputValid(input))
            {
                continue;
            }
            else
            {
                return input!;
            }
        } 
        while (true);
    }

    public static void PressKeyToContinue()
    {
        Console.WriteLine("");
        Console.Write("Press any key to continue... ");
        Console.ReadLine();
    }
}