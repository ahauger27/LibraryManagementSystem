namespace LibraryManagementSystem.Common.Services;

public class Processor
{
    public bool RunStatus { get; set; }

    public void Start()
    {
        RunStatus = true;
    }

    public void End()
    {
        RunStatus = false;
    }
}