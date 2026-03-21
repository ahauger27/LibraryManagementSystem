namespace LibraryManagementSystem.API.Models;

public record Person
{
    public string? Name { get; set; }
    public string? Language { get; set; }
    public string? Id { get; set; }
    public string? Bio { get; set; }
    public decimal Version { get; set; }
}