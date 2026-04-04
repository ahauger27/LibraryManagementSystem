using System.Text.Json;

namespace LibraryManagementSystem.ConsoleApp.Models;

public static class PatronActions
{    
    public static bool VerifyNewPatronInfo(Patron newPatron)
    {
        string? response;

        do
        { 
            Console.WriteLine($"{Environment.NewLine}DO YOU WANT TO CONTINUE WITH THIS INFORMATION?");
            Console.WriteLine($"FIRST NAME: {newPatron.FirstName}");
            Console.WriteLine($"MIDDLE INITIAL: {newPatron.MiddleInitial}");
            Console.WriteLine($"LAST NAME: {newPatron.LastName}");
            Console.WriteLine($"DATE OF BIRTH: {newPatron.DateOfBirth}");
            Console.WriteLine($"ADDRESS: {newPatron.Address}");
            Console.WriteLine($"EMAIL: {newPatron.Email}");
            Console.WriteLine($"PHONE NUMBER: {newPatron.PhoneNumber}");
            Console.Write("Y/N: ");

            response = UserActions.StringInput().ToUpper();
            if (response == "N")
            {
                UpdatePatronInfo(newPatron);
                return false;
            }
            else
            {
                return true;
            }

        } while (response != "Y");
    }

    public static void UpdatePatronInfo(Patron patron)
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
        string? input = UserActions.StringInput();

        switch (input)
        {
            case "1":
                patron.FirstName = InputPatronFirstName();
                break;

            case "2":
                patron.MiddleInitial = InputPatronMiddleInitial();
                break;

            case "3":
                patron.LastName = InputPatronLastName();
                break;

            case "4":
                patron.DateOfBirth = DateOfBirth.DateInput();
                break;

            case "5":
                patron.Address = InputPatronAdress();
                break;

            case "6":
                patron.Email = InputPatronEmail();
                break;

            case "7":
                patron.PhoneNumber = InputPatronPhoneNumber();
                break;
            
            case "8":
                Console.WriteLine("Done");
                break;
                 
            default:
                break;
        }        
    }

    public static Patron CreateNewPatron()
    {
        return InputNewPatronInfo();
        
        // bool patronCreationSuccess = false;
        
        // while (!patronCreationSuccess)
        // {

        //     if (!VerifyNewPatronInfo(newPatron))
        //     {
        //         continue;
        //     }
        //     else
        //     {
        //         patronCreationSuccess = true;
        //         return newPatron;
        //     }
        // }
    }

    public static Patron InputNewPatronInfo()
    {
        string firstName = InputPatronFirstName();

        string middleInitial = InputPatronMiddleInitial();
        
        string lastName = InputPatronLastName();

        DateOnly dateOfBirth = DateOfBirth.DateInput();

        string address = InputPatronAdress();

        string email = InputPatronEmail();

        string phoneNumber = InputPatronPhoneNumber();
            
        Patron newPatron = new(firstName, lastName, dateOfBirth, middleInitial, address, email, phoneNumber);
        return newPatron;
    }

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
}