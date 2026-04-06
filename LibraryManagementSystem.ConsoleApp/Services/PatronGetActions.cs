using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronGetActions
{
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