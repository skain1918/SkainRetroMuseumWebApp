
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SkainRetroMuseumWebApp.Models; 
public class ApplicationDbContext:IdentityDbContext<AppUser> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
        }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Hardware> Hardwares { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Message> Messages { get; set; }
}