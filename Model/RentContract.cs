using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRentApp;

public class RentContract
{
    [Key]
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    public int HousingId { get; set; }
    public DateTime StartDate { get; set; }   
    public DateTime EndDate { get; set; }
    
    [ForeignKey("ClientId")]
    public Client Client { get; set; }

    [ForeignKey("HousingId")]
    public Housing Housing { get; set; }
    
    public RentContract(){}
    
    public RentContract(Client client, Housing housing, DateTime startDate, DateTime endDate)
    {
        Client = client;
        Housing = housing;
        ClientId = client.Id;
        HousingId = housing.Id;
        StartDate = startDate;
        EndDate = endDate;
    }
    
    
}