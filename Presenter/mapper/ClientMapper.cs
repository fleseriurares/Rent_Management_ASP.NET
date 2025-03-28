using HouseRentApp.Presenter.dto;
using Microsoft.AspNetCore.SignalR;

namespace HouseRentApp.Presenter.mapper;

public class ClientMapper
{
    public ClientDto ClientEntityToDto(Client client)
    {
        return new ClientDto(client.Id, client.Name, client.Cnp);
    }

    public List<ClientDto> ClientEntityToDtos(List<Client> clients)
    {
        return clients.Select(h => ClientEntityToDto(h)).ToList();
    }

    public Client ClientDtoToEntity(ClientDto clientDto)
    {
        return new Client(clientDto.Name, clientDto.Cnp);
    }

    public List<Client> ClientsDtoToEntities(List<ClientDto> clientDtos)
    {
        return clientDtos.Select(h => ClientDtoToEntity(h)).ToList();
    }
    
}