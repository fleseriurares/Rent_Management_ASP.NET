using HouseRentApp;
using HouseRentApp.Components.Database;
using HouseRentApp.Model;


public class HousingRepository : Repository<Housing>
{
    public HousingRepository() : base(DatabaseConnection.GetConnectionContext())
    {
    }

    public HousingRepository(HousingContext context) : base(context)
    {
        
    }
}