using System.Text.Json;

namespace LibraryManagementSystem.ConsoleApp.Models;

public static class PatronActions
{    
    public static string InputPatronFirstName()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's FIRST NAME: ");
        return UserActions.StringInput();
    }

    public static string InputPatronMiddleInitial()
    {
        string? middleInitial = "";
        while (middleInitial.Length != 1)
        {
            Console.Write($"{Environment.NewLine}Enter the patron's MIDDLE INITIAL: ");
            middleInitial = UserActions.StringInput();
            
        }
        return middleInitial; 
    }

    public static string InputPatronLastName()
    {

        Console.Write($"{Environment.NewLine}Enter the patron's LAST NAME: ");
        return UserActions.StringInput();
    }

    public static DateOnly InputPatronDOB()
    {
        while (true)
        {
            Console.Write($"{Environment.NewLine}Enter the patron's DATE OF BIRTH: ");
            string? date = UserActions.StringInput();

            if (IsValidDOB(date))
            {
                return DateOnly.Parse(date);
            }
            else
            {
                Console.WriteLine("Please input a date");
            }
        }
    }

    public static bool IsValidDOB(string date)
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

    public static string InputPatronAdress()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's ADDRESS: ");
        return UserActions.StringInput();   
    }

    public static string InputPatronEmail()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's EMAIL: ");
        return UserActions.StringInput();    
    }

    public static string InputPatronPhoneNumber()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's PHONE NUMBER: ");
        return UserActions.StringInput();    
    }

    public static Patron CreateNewPatron()
    {
        string firstName = InputPatronFirstName();

        string middleInitial = InputPatronMiddleInitial();
        
        string lastName = InputPatronLastName();

        DateOnly dateOfBirth = InputPatronDOB();

        string address = InputPatronAdress();

        string email = InputPatronEmail();

        string phoneNumber = InputPatronPhoneNumber();
            
        Patron newPatron = new(firstName, lastName, dateOfBirth, middleInitial, address, email, phoneNumber);
        return newPatron;
    }

    public static void ApplyPatronUpdate(Patron patron, string fieldNumber, string input)
    {
        switch (fieldNumber)
        {
            case "1":
                patron.FirstName = input;
                break;

            case "2":
                patron.MiddleInitial = input;
                break;

            case "3":
                patron.LastName = input;
                break;

            case "4":
                if (!IsValidDOB(input))
                {
                    DateOnly date = DateOnly.Parse(input);
                    patron.DateOfBirth = date;
                }
                break;

            case "5":
                patron.Address = input;
                break;

            case "6":
                patron.Email = input;
                break;

            case "7":
                patron.PhoneNumber = input;
                break;
            
            case "8":
                Console.WriteLine("Returning to Menu...");
                break;
                 
            default:
                break;
        }        
    }

    public static string UpdatePatronInfo(Patron patron)
    {
        Console.WriteLine(
        $"""

        CHOOSE WHICH FIELD TO CHANGE:
        1. FIRST NAME: {patron.FirstName}
        2. MIDDLE INITIAL: {patron.MiddleInitial}
        3. LAST NAME: {patron.LastName}
        4. DATE OF BIRTH: {patron.DateOfBirth}
        5. ADDRESS: {patron.Address}
        6. EMAIL: {patron.Email}
        7. PHONE NUMBER: {patron.PhoneNumber}
        8. Quit Editing
        """
        );

        Console.Write("Which field do you want to update?: ");
        return UserActions.StringInput();
    }
}