using System.ComponentModel.DataAnnotations;

namespace HouseRentApp;

public class Housing
{
    [Key]
    public int Id { get; set; }
    public HousingType Type { get; set; }
    
    public string Address { get; set; }
    public int Price { get; set; }
    public int RoomNumber { get; set; }
    public string ImageUrl { get; set; }
    public Boolean Rented { get; set; }
    public Housing() { }
    
    public Housing(HousingType type, string address, int price, int roomNumber, string imageUrl, Boolean rented)
    {
        Type = type;
        Address = address;
        Price = price;
        RoomNumber = roomNumber;
        ImageUrl = imageUrl;
        Rented = rented;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Type: {Type}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Price: {Price}");
    }
    
    public Housing Clone()
    {
        return new Housing(this.Type, this.Address, this.Price, this.RoomNumber, this.ImageUrl, this.Rented);
    }
    
}