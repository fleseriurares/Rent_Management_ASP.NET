namespace HouseRentApp.Presenter;

public interface IHousesManagementGUI
{
    void ShowHouses();
    int GetMinPrice();
    int GetMaxPrice();
    int GetSelectedNoOfRooms();
    string GetSelectedType();
    string GetSelectedAddress();
    string GetClientName();
    string GetClientCNP();
    DateTime GetStartDate();
    DateTime GetEndDate();
    int GetHousingId();
    int GetPrice();
    int GetNoOfRooms();
    string GetHousingType();
    string GetAddress();
    string GetImgURL();
    bool GetIsRented();
    int GetCurrentId();
    void SetMinPrice(int minPrice);
    void SetMaxPrice(int maxPrice);
    void SetSelectedType(string selectedType);
    void SetSelectedAddress(string selectedAddress);
    void SetRooms(int rooms);
    void SetEditMode(bool editMode);
    void SetAddress(string address);
    void SetImgURL(string imgUrl);
    void SetPrice(int price);
    void SetHousingType(string housingType);
    void SetCurrentId(int id);
}