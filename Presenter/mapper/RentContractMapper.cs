using HouseRentApp.Presenter.dto;

namespace HouseRentApp.Presenter.mapper;

public class RentContractMapper
{
    public RentContractDto RentContractToDto(RentContract rentContract)
    {
        return new RentContractDto(
            rentContract.Id,
            rentContract.HousingId,
            rentContract.Housing.Type.ToString(),
            rentContract.Housing.Address,
            rentContract.Housing.Price,
            rentContract.Housing.RoomNumber,
            rentContract.Housing.ImageUrl,
            rentContract.ClientId,
            rentContract.Client.Name,
            rentContract.Client.Cnp,
            rentContract.StartDate.ToString("dd/MM/yyyy"),
            rentContract.EndDate.ToString("dd/MM/yyyy"));
    }

    public List<RentContractDto> RentContractsToDto(List<RentContract> rentContracts)
    {
        return rentContracts.Select(r => RentContractToDto(r)).ToList();
    }
}