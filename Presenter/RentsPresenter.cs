using HouseRentApp.Presenter.dto;
using HouseRentApp.Presenter.mapper;

namespace HouseRentApp.Presenter;

public class RentsPresenter
{
    private IRentsGUI_ _view;
    private RentContractMapper rcMapper;

    public List<RentContract> RentContracts { get; set; }
    public List<RentContractDto> RentContractsDto { get; set; }
    public List<Client> Clients { get; set; } = new List<Client>();
    public List<Housing> Housings { get; set; } = new List<Housing>();

    private readonly HousingRepository _housingRepository;
    private readonly ClientRepository _clientRepository;
    private readonly RentContractRepository _hRentContractRepository;
    
    
    public Dictionary<int, Housing> HousesDict { get; private set; }
    public Dictionary<int, Client> ClientsDict { get; private set; }
    
    public RentsPresenter(IRentsGUI_ view)
    {
        _view = view;
        _housingRepository = new HousingRepository();
     //   RentContracts = new List<RentContract>();
        _clientRepository = new ClientRepository();
     //   Clients = new List<Client>();
        //Clients = _clientRepository.GetAllAsync().Result.ToList();
        _hRentContractRepository = new RentContractRepository();
        RentContractsDto = new List<RentContractDto>();
        rcMapper = new RentContractMapper();
    }

    public async Task<List<Client>> GetAllClientsAsync()
    {
        Clients = (await _clientRepository.GetAllAsync()).ToList();
        return Clients;
    }
    
    public async Task<List<Housing>> GetAllHousesAsync()
    {
        Housings = (await _housingRepository.GetAllAsync()).ToList();
        return Housings;
    }
    
    public Housing GetHouseById(int houseId)
    {
        return  _housingRepository.GetByIdAsync(houseId).Result;
    }
    public Client GetClientById(int clientId)
    {
        return  _clientRepository.GetByIdAsync(clientId).Result;
    }

    public async Task<List<Client>> GetClientsList()
    {
        return  Clients = _clientRepository.GetAllAsync().Result.ToList();
    }
    
    
    public async Task AddRentContractAsync(DateTime StartDate, DateTime EndDate)
    {
        Client newClient = new Client(_view.GetClientName(), _view.GetClientCnp());
        Housing housing = new Housing(MatchType(_view.GetHousingType()), _view.GetAddress(), _view.GetPrice(), _view.GetNoOfRooms(), _view.GetImgURL(),true);
      
        RentContract rC =new RentContract(newClient, housing.Clone(), StartDate, EndDate); //de modificat data
  
        await _hRentContractRepository.AddAsync(rC);


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
    
    public async Task UpdateContract()
    {
        await AddRentContractAsync(_hRentContractRepository.GetByIdAsync(_view.GetId()).Result.StartDate, _hRentContractRepository.GetByIdAsync(_view.GetId()).Result.EndDate);
        await _housingRepository.DeleteAsync(_hRentContractRepository.GetByIdAsync(_view.GetId()).Result.ClientId);
        await _clientRepository.DeleteAsync(_hRentContractRepository.GetByIdAsync(_view.GetId()).Result.ClientId);
        await _hRentContractRepository.DeleteAsync(_view.GetId());
        Console.WriteLine("Deleted");
        
        Console.WriteLine("Inserted");
        //_view.ShowHouses();
    }
    
    public void EditInPending(int id)
    {
        RentContract rc = _hRentContractRepository.GetByIdAsync(id).Result;
        Console.WriteLine(rc.Housing.Type);
        _view.SetHousingType(rc.Housing.Type.ToString());
        _view.SetAddress(rc.Housing.Address);
        _view.SetPrice(rc.Housing.Price);
        _view.SetRooms(rc.Housing.RoomNumber);
        _view.SetImgURL(rc.Housing.ImageUrl);
        _view.SetClientName(rc.Client.Name);
        _view.SetClientCnp(rc.Client.Cnp);
        _view.SetEditMode(true);
        _view.SetCurrentId(id);
    }

    public async Task<List<Housing>> GetAllRentsAsync()
    {
        RentContracts = (await _hRentContractRepository.GetAllAsync()).ToList();
        foreach (var contract in RentContracts)
        {
            var house = await _housingRepository.GetByIdAsync(contract.HousingId);
            contract.Housing = house;
            var client = await _clientRepository.GetByIdAsync(contract.ClientId);
            contract.Client = client;
        }
        RentContractsDto = rcMapper.RentContractsToDto(RentContracts);
        return null;
    }
    
    
    
    public async Task DeleteContractAsync(int id)
    {
        await _hRentContractRepository.DeleteAsync(id);
    }
    
    
}