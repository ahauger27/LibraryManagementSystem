using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronPostActions
{    
    public static string InputPatronFirstName()
    {
        Console.Write($"{Environment.NewLine}Enter the patron's FIRST NAME: ");
        return UserActions.StringInput();
    }

    public static string InputPatronMiddleInitial()
    {
        string? middleInitial = "";
        while (string.IsNullOrEmpty(middleInitial))
        {
            Console.Write($"{Environment.NewLine}Enter the patron's MIDDLE INITIAL: ");
            middleInitial = Console.ReadLine();
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
}