using Microsoft.EntityFrameworkCore;
namespace HouseRentApp;

public class HousingContext : DbContext
{
    public DbSet<Client> Client { get; set; }
    public DbSet<Housing> Housing { get; set; }
    public DbSet<RentContract> RentContract { get; set; }

    public HousingContext(DbContextOptions<HousingContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Housing>().HasKey(h => h.Id);
        modelBuilder.Entity<Housing>().Property(h => h.Price).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<RentContract>()
            .HasKey(rc => rc.Id);
        

        modelBuilder.Entity<RentContract>()
            .HasOne(rc => rc.Client)
            .WithMany()
            .HasForeignKey(rc => rc.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RentContract>()
            .HasOne(rc => rc.Housing)
            .WithMany()
            .HasForeignKey(rc => rc.HousingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}