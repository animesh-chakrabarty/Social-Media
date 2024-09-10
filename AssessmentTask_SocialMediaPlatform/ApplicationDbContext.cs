using Social_Media.Models;
using Microsoft.EntityFrameworkCore;

namespace Social_Media;

public class ApplicationDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // connect to sqlite database
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }

    // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}