using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp;
public class Program
{
    public static async Task Main()
    {
        Processes run = new Processes();
        run.Start();

        Console.WriteLine("""

        
            =========================
            LIBRARY MANAGEMENT SYSTEM
            =========================
            
            """);

        HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost:5126")
        };

        JsonSerializerOptions options = new() 
        { 
            PropertyNameCaseInsensitive = true 
        };

        await MainMenu.MenuLoop(run, client);                          
    }
}
