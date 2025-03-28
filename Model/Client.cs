
using System.ComponentModel.DataAnnotations;

namespace HouseRentApp;

public class Client
{
    [Key]
    public int Id{ get; set; }
    public string Name{ get; set; }
    public string Cnp { get; set; }

    public Client()
    {
        
    }
    
    public Client(string name, string cnp)
    {
        Name = name;
        Cnp = cnp;
    }
    
}