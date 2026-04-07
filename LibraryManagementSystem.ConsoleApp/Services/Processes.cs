using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.ConsoleApp.Services;

public class Processes
{
    public bool RunStatus { get; set; }
    public JsonSerializerOptions JsonOptions { get; private set; } = new();

    public void Start()
    {
        RunStatus = true;
        InitializeJsonSerializerOptions();
    }

    public void InitializeJsonSerializerOptions()
    {
        JsonOptions.PropertyNameCaseInsensitive = true;
        // JsonOptions.ReferenceHandler = ReferenceHandler.Preserve;
    }

    public void End()
    {
        RunStatus = false;
    }
}