namespace HouseRentApp.Presenter;

public interface IRentsGUI_
{
    void SetEditMode(bool editMode);
    void SetHousingType(string str);
    void  SetAddress(string address);
    void SetPrice(int price);
    void SetRooms(int roomNumber);
    void SetImgURL(string imgURL);
    void SetClientName(string name);
    void SetClientCnp(string cnp);
    void SetCurrentId(int id);
    int GetPrice();
    int GetNoOfRooms();
    string GetHousingType();
    string GetAddress();
    string GetImgURL();
    string GetClientCnp();
    string GetClientName();
    int GetId();
}