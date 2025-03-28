using HouseRentApp;
using HouseRentApp.Components.Database;
using HouseRentApp.Model;


public class RentContractRepository : Repository<RentContract>
{
    public RentContractRepository() : base(DatabaseConnection.GetConnectionContext())
    {
    }

    public RentContractRepository(HousingContext context) : base(context)
    {
        
    }
}