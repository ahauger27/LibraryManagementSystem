using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Common.Models;
public class PatronDB : DbContext
{
    public PatronDB(DbContextOptions<PatronDB> options)
        : base(options) { }
    
    public DbSet<Patron> Patrons { get; set; }
}