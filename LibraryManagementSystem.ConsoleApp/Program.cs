using LibraryManagementSystem.ConsoleApp.Services;
using LibraryManagementSystem.ConsoleApp.Menus;

namespace LibraryManagementSystem.ConsoleApp;

public class Program
{
    public static async Task Main()
    {
        Processes session = new Processes();
        session.Start();

        Console.WriteLine("""

        
            =========================
            LIBRARY MANAGEMENT SYSTEM
            =========================
            
            """);

        HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost:5126")
        };

        await MainMenu.MenuLoop(session, client);                          
    }
}
