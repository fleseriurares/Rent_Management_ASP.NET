using HouseRentApp;
using HouseRentApp.Components.Database;
using HouseRentApp.Model;

public class ClientRepository : Repository<Client>
{
    public ClientRepository() : base(DatabaseConnection.GetConnectionContext())
    {
    }

    public ClientRepository(HousingContext context) : base(context)
    {
        
    }
}