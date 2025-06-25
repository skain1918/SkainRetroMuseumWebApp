using Microsoft.EntityFrameworkCore;

namespace SkainRetroMuseumWebApp.Models; 
public class ApplicationDbContext:DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
        }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Hardware> Hardwares { get; set; }
    public DbSet<Branch> Branches { get; set; }
    }