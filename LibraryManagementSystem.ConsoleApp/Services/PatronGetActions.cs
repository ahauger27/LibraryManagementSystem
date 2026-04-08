using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronGetActions
{
    public static string GetPatronIDFromUser()
    {
        Console.Write("Enter patron ID to check out to: ");

        string? patronIDToCheckOut = Console.ReadLine();

        while (true)
        {
            if (string.IsNullOrEmpty(patronIDToCheckOut))
            {
                Console.WriteLine("INVALID. Patron ID cannot be empty.");
                Console.Write("Please enter the patron's ID: ");
                continue;
            }
            else
            {
                return patronIDToCheckOut;
            }
        }
    }
    
    public static async Task<List<Patron>> CreatePatronsListFromApi(string jsonContent, JsonSerializerOptions options)
    {
        // var patrons = new List<Patron>();
        
        var patrons = JsonSerializer.Deserialize<List<Patron>>(jsonContent, options);
        
        if (patrons.Count == 0)
        {
            Console.WriteLine("No patrons found.");   
        }
        return patrons;
    }

    public static void DisplayAllPatrons(List<Patron> patrons)
    {   
        Console.WriteLine($"{Environment.NewLine}PATRONS:");
        Console.WriteLine("ID\tNAME");
        Console.WriteLine("=================================");
        
        foreach (var patron in patrons)
        {
            Console.WriteLine($"{patron.PatronID}\t{patron.LastName}, {patron.FirstName}");
        }
    }
}