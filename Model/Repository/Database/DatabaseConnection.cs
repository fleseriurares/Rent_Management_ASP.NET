using Microsoft.EntityFrameworkCore;

namespace HouseRentApp.Components.Database;

public class DatabaseConnection
{
    public static HousingContext GetConnectionContext()
    {
        DbContextOptions<HousingContext> options = new DbContextOptionsBuilder<HousingContext>()
            .UseMySql("server=localhost;database=RentApp;user=root;password=parolaproiect123",
                new MySqlServerVersion(new Version(8, 0, 29)))
            .Options;
        
        return new HousingContext(options);
    } 
}