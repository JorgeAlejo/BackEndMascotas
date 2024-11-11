using Microsoft.EntityFrameworkCore;

public class VetDbContext: DbContext{
    public VetDbContext(DbContextOptions<VetDbContext> options):base(options){}
    public DbSet<User> Users { get; set; }
}