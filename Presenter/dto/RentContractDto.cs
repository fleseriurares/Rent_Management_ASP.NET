namespace HouseRentApp.Presenter.dto;

public record RentContractDto(
    int Id,
    int HousingId,
    string HousingType,
    string Address,
    int Price,
    int NoOfRooms,
    string ImageUrl,
    int ClientId,
    string ClientName,
    string ClientCnp,
    string StartDate,
    string EndDate
    );
