using Microsoft.EntityFrameworkCore;
using MyApp.Backend.Models;
namespace MyApp.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Players> Players { get; set; }
}