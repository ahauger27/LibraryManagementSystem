using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Menus;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronGetActions
{
    public static string GetPatronIDFromUser()
    {
        Console.Write("Enter patron ID: ");

        while (true)
        {
            string? patronIDToSearch = Console.ReadLine();
            if (string.IsNullOrEmpty(patronIDToSearch))
            {
                Console.WriteLine("INVALID. Patron ID cannot be empty.");
                Console.Write("Please enter the patron's ID: ");
                continue;
            }
            else
            {
                return patronIDToSearch;
            }
        }
    }
    
    public static async Task<Patron?> TryToLoadPatronAccount(string patronIDToSearch, HttpClient client, Processes session)
    {
        try
        {
            Patron? patronToSearch = await PatronHttpActions.GetPatronByID(patronIDToSearch, client, session.JsonOptions);
            
            return patronToSearch;
        }
        catch (NullReferenceException)
        {
            return null;
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
        Console.WriteLine("");
        Console.WriteLine("ID\tNAME");
        Console.WriteLine("=================================");
        
        foreach (var patron in patrons)
        {
            Console.WriteLine($"{patron.PatronID}\t{patron.LastName}, {patron.FirstName}");
        }
    }
}