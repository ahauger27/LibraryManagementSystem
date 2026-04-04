using System.Text.Json;
using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.ConsoleApp;
public class Program
{
    public static async Task Main(string[] args)
    {
        Processor run = new Processor();
        run.Start();

        HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost:5126")
        };

        JsonSerializerOptions options = new() 
        { 
            PropertyNameCaseInsensitive = true 
        };

        //MainMenu();
        Console.WriteLine("=========================");
        Console.WriteLine("LIBRARY MANAGEMENT SYSTEM");
        Console.WriteLine("=========================");

        while (run.RunStatus == true)
        {
            // Make this section of Console.WriteLines more modular
            // As in, make sure that I could easily swap these out later,
            // CAT 2, would make life easier, but not crucial to program atm.
            Console.WriteLine($"{Environment.NewLine}MAIN MENU");
            Console.WriteLine($"========={Environment.NewLine}");
            Console.WriteLine("1. Patron Search (WIP)");
            Console.WriteLine("2. View Catalog (WIP)");
            Console.WriteLine("3. Create New Patron (WIP)");
            Console.WriteLine("D. Delete Patron (WIP)");
            Console.WriteLine("U. Update Patron Account (WIP)");
            Console.WriteLine("4. Quit program (WIP)");

            Console.WriteLine("");
            Console.Write("Please select an option: ");
            string? userChoice = UserActions.StringInput();

            try
            {
                if (userChoice != null || userChoice != string.Empty)
                {
                    switch (userChoice)
                    {
                        case "1": // 1. Patron Search (WIP)
                            Console.WriteLine($"{Environment.NewLine}PATRON SEARCH{Environment.NewLine}=============");
                            Console.WriteLine("1. List All Patrons");
                            Console.WriteLine("2. Search Patrons By ID (WIP)");

                            Console.WriteLine("");
                            Console.Write("Please select an option: ");

                            string? userChoice2 = Console.ReadLine();
                            if (userChoice2 != null || userChoice2 != string.Empty)
                            {
                                switch (userChoice2)
                                {
                                    case "1": 
                                        await PatronHttpActions.GetPatrons(client, options);
                                        break;

                                    case "2": // Search by patron method name // MORE READABLE
                                        // Move this into a method

                                        Console.Write("Enter patron's ID: ");
                                        string? idToSearchString = Console.ReadLine();
                                        if (idToSearchString != null || idToSearchString != string.Empty)
                                        {
                                            if (int.TryParse(idToSearchString, out int idToSearch))
                                            {   
                                                Console.WriteLine("Finding patron...");
                                                Patron patron = await PatronHttpActions.GetPatronByID(idToSearch, client, options);
                                                
                                                if (patron != null)
                                                {
                                                    Console.WriteLine(patron.FullName());
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid ID format.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input, returning to main menu.");
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Invalid option, returning to main menu.");
                                        break;
                                }
                            }
                            break;

                        case "2": // 2. Search Items (WIP)
                            Console.WriteLine("This feature is still in progress");
                            break;

                        case "3": // 3. Add New Patron (WIP)
                            Console.WriteLine("MENU TO ADD A NEW PATRON");

                            Patron newPatron = PatronActions.CreateNewPatron();
                            
                            if (newPatron != null)
                            {
                                Console.WriteLine("Patron account created!");
                                await PatronHttpActions.PostNewPatron(newPatron, client);
                            }
                            else
                            {
                                Console.WriteLine("Unable to create patron account. Returning to main menu...");
                            }
                            break;

                        case "D":
                            Console.WriteLine("MENU TO DELETE A PATRON");

                            Console.Write("Enter the ID of the patron you want to delete: ");
                            string? idToDeleteString = Console.ReadLine();
                            if (int.TryParse(idToDeleteString, out int idToDelete))
                            {
                                await PatronHttpActions.DeletePatron(idToDelete, client);
                            }
                            break;

                        case "U":
                            Console.WriteLine("MENU TO UPDATE A PATRON");
                            
                            Console.Write("Enter the ID of the patron to update: ");
                            string? idToUpdateString = Console.ReadLine();
                            if (int.TryParse(idToUpdateString, out int idToUpdate))
                            {
                                await PatronHttpActions.PutPatron(idToUpdate, client, options);
                            }
                            break;

                        case "4":
                            run.End();
                            Console.WriteLine("Shutting down program...");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please choose a valid option.");
                            break;
                    }
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
        }
    }
}
