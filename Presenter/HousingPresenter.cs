using HouseRentApp.Presenter.dto;
using HouseRentApp.Presenter.mapper;
using Microsoft.EntityFrameworkCore;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace HouseRentApp.Presenter;

public class HousingPresenter
{
    public IHousesManagementGUI _view { get; set; }
    public List<Housing> Houses { get; set; }
    public List<Housing> FilteredHouses { get; set; }
    public List<HousingDto> AvailableHouses { get; set; }
    
    private HousingMapper housingMapper;

    private readonly HousingRepository _housingRepository;
    private readonly ClientRepository _clientRepository;
    private readonly RentContractRepository _rentContractRepository;
    

    public HousingPresenter(IHousesManagementGUI view)
    {
        _view = view;
        housingMapper = new HousingMapper();
        _housingRepository = new HousingRepository();
        _clientRepository = new ClientRepository();
        _rentContractRepository = new RentContractRepository();
        AvailableHouses = new List<HousingDto>();
        
    }
    
    public static HousingType MatchType(string type)
    {
        if (type.Equals("House"))
        {
            return HousingType.House;
        }
        if (type.Equals("Apartment"))
        {
            return HousingType.Apartment;
        }

        return HousingType.Studio;
    }
    
    public async Task AddHousingAsync(string type, string address, int price, int rooms, string imageUrl, Boolean rented)
    {   
        HousingType housingType = MatchType(type);
        var newHousing = new Housing
        {
            Type = housingType,
            Address = address,
            Price = price,
            RoomNumber = rooms,
            ImageUrl = imageUrl,
            Rented = rented
        };
        _view.SetEditMode(false);
        await _housingRepository.AddAsync(newHousing); 
    }

    public async Task AddHousingAsync()
    {   
        HousingType housingType = MatchType(_view.GetHousingType());
        var newHousing = new Housing
        {
            Type = housingType,
            Address = _view.GetAddress(),
            Price = _view.GetPrice(),
            RoomNumber = _view.GetNoOfRooms(),
            ImageUrl = _view.GetImgURL(),
            Rented = _view.GetIsRented()
        };
        await _housingRepository.AddAsync(newHousing); 
    }
    
    public async Task AddClientAsync(Client newClient)
    {
            await _clientRepository.AddAsync(newClient);
    }

    public void EditInPending(int id)
    {
        Housing housing = _housingRepository.GetByIdAsync(id).Result;
        _view.SetCurrentId(id);
        _view.SetHousingType(housing.Type.ToString());
        _view.SetAddress(housing.Address);
        _view.SetPrice(housing.Price);
        _view.SetRooms(housing.RoomNumber);
        _view.SetImgURL(housing.ImageUrl);
        _view.SetEditMode(true);
    }
    
    public async Task AddRentContractAsync(int housingId)
    {
        Client newClient = new Client(_view.GetClientName(), _view.GetClientCNP());
        Console.WriteLine(newClient.Id);

        Housing housing = _housingRepository.GetByIdAsync(housingId).Result;
        housing.Rented = true;
        RentContract rC =new RentContract(newClient, housing.Clone(),_view.GetStartDate(), _view.GetEndDate());
        
        await _rentContractRepository.AddAsync(rC);
        RentHousing();
        Console.WriteLine("Okk");
    }

    public void RentHousing()
    {
        _housingRepository.DeleteAsync(_view.GetCurrentId());
        AddHousingAsync(_view.GetType().ToString(),_view.GetAddress(),_view.GetPrice(),_view.GetNoOfRooms(),_view.GetImgURL(), true);
    }
    
    public async Task DeleteHousingAsync(int housingId)
    {   
        _view.SetEditMode(false);
        await _housingRepository.DeleteAsync(housingId);
    }

    public async Task<List<HousingDto>> GetRentedHousingAsync()
    {
       return housingMapper
           .HousingEntitiesToDto((await _housingRepository.GetAllAsync()).ToList())
           .Where(h => !h.Rented)
           .ToList();
    }
    
    public async Task<List<HousingDto>> GetAvailableHousingAsync()
    {
        return AvailableHouses = housingMapper
            .HousingEntitiesToDto((await _housingRepository.GetAllAsync()).ToList())
            .Where(h => !h.Rented)
            .ToList();
    }
    
    public async Task<List<Housing>> GetAllHousesAsync()
    {
        FilteredHouses = (await _housingRepository.GetAllAsync()).ToList();
        SaveHousesInFile();
        RemoveRented(FilteredHouses);
        Houses = FilteredHouses.ToList();
       
        return FilteredHouses;
    }
    
    
    public async Task UpdateHousing()
    {   
        await _housingRepository.DeleteAsync(_view.GetCurrentId());
        await AddHousingAsync(_view.GetType().ToString(),_view.GetAddress(),_view.GetPrice(),_view.GetNoOfRooms(),_view.GetImgURL(), _view.GetIsRented());
        _view.SetEditMode(false);
        _view.ShowHouses();
    }
    
    public void SortByPrice()
    {
        FilteredHouses.Sort((x,y) => x.Price.CompareTo(y.Price));
        _view.ShowHouses();
    }
    
    public void SortByLocation()
    {
        FilteredHouses.Sort((x,y) => string.Compare(x.Address, y.Address, StringComparison.InvariantCultureIgnoreCase));
    }

    public void SaveHousesInFile()
    {
        FileSaver fileSaver = new FileSaver();
        fileSaver.SaveAsCSVFile(GetRentedHouses());
        fileSaver.SaveAsDocFile(GetRentedHouses());
    }

    public List<Housing> GetRentedHouses()
    {
        List<Housing> rentedHouses = new List<Housing>();
        foreach (Housing housing in FilteredHouses)
        {
            if (housing.Rented)
            {
                rentedHouses.Add(housing);
            }
        }
        return rentedHouses;
    }
    
    public void ApplyFilters()
    {
        int minPrice = _view.GetMinPrice();
        int maxPrice = _view.GetMaxPrice();
        string location = _view.GetSelectedAddress();
        int noOfRooms = _view.GetSelectedNoOfRooms();
        string housingType = _view.GetSelectedType();
        List<Housing> result = new List<Housing>();

        foreach (Housing house in Houses)
        {
            if ((house.Price >= minPrice &&  house.Price <= maxPrice) || maxPrice == 0)
            {
                if((location.Equals("") || house.Address.Equals(location)) && (housingType.Equals(house.Type.ToString()) || housingType.Equals("All types")) && (house.RoomNumber == noOfRooms || noOfRooms == 0))
                    result.Add(house);
            }
        }
        FilteredHouses = result.ToList();
        AvailableHouses = housingMapper.HousingEntitiesToDto(FilteredHouses).ToList();
    }

    public void ResetFilters()
    {
        _view.SetMinPrice(0);
        _view.SetMaxPrice(2000);
        _view.SetSelectedType("All types");
        _view.SetSelectedAddress("");
        
        ApplyFilters();
        SortByLocation();
        SortByPrice();
        
    }

    public void RemoveRented(List<Housing> houses)
    {
       houses.RemoveAll(house => house.Rented);
    }
    
    public void ResetFilter()
    {
        FilteredHouses = Houses.ToList();
    }
    
}