namespace HouseRentApp.Presenter.dto;

public record HousingDto(
    int Id,
    string Type,
    string Address,
    int Price,
    int RoomNumber,
    string ImageUrl,
    bool Rented);