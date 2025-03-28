using HouseRentApp.Presenter.dto;

namespace HouseRentApp.Presenter.mapper;

public class HousingMapper
{
    public HousingDto HousingEntityToDto(Housing housing)
    {
        return new HousingDto(housing.Id,housing.Type.ToString(), housing.Address, housing.Price, housing.RoomNumber, housing.ImageUrl, housing.Rented);
    }

    public List<HousingDto> HousingEntitiesToDto(List<Housing> housings)
    {
        return housings.Select(h => HousingEntityToDto(h)).ToList();
    }

    public Housing HousingDtoToEntity(HousingDto housingDto)
    {
        return new Housing(HousingPresenter.MatchType(housingDto.Type),
                housingDto.Address, 
                housingDto.Price,
                housingDto.RoomNumber,
                housingDto.ImageUrl, 
                housingDto.Rented);
    }

    public List<Housing> HousingDtosToEntity(List<HousingDto> housingDtos)
    {
        return housingDtos.Select(h => HousingDtoToEntity(h)).ToList();
    }
    
}