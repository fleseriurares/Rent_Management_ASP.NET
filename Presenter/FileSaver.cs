using System.Text;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace HouseRentApp.Presenter;

public class FileSaver
{

    public FileSaver()
    {

    }

    public void SaveAsCSVFile(List<Housing> houses)
    {
        string filePath = "houses.csv";
        CreateCsvFromList(houses, filePath);

        Console.WriteLine("CSV file created at: " + filePath);
    }
    
    public void SaveAsDocFile(List<Housing> houses)
    {
        string filePath = "houses.docx";
        CreateCsvFromList(houses, filePath);

        Console.WriteLine("CSV file created at: " + filePath);
    }
    
    static void CreateCsvFromList(List<Housing> houses, string filePath)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var house in houses)
        {
            Console.WriteLine($"Verificare house: Id = {house.Id}, Type = {house.Type}, Address = {house.Address}, Price = {house.Price}, RoomNumber = {house.RoomNumber}, ImageUrl = {house.ImageUrl}");
        }
        
        // Add header row
        sb.AppendLine("Id,Type,Address,Price,RoomNumber,ImageUrl");
        
        // Loop through the list and create CSV lines
        foreach (var house in houses)
        {
            sb.AppendLine($"{house.Id},{house.Type},{house.Address},{house.Price},{house.RoomNumber},{house.ImageUrl}");
        }

        // Write the CSV to a file
        File.WriteAllText(filePath, sb.ToString());
    }
    
    static void CreateDocxFromList(List<Housing> houses, string filePath)
    {
        // Crearea unui document Word nou
        using (DocX doc = DocX.Create(filePath))
        {
            // Adăugarea unui titlu în document
            doc.InsertParagraph("Lista de case închiriate")
                .FontSize(18)
                .Bold()
                .Alignment = Alignment.center;

            doc.InsertParagraph("\n"); // Linie goală pentru separare

            // Adăugarea unui tabel cu 6 coloane: Id, Type, Address, Price, RoomNumber, ImageUrl
            Table table = doc.AddTable(houses.Count + 1, 6); // 1 pentru header + N case
            table.Alignment = Alignment.center;

            // Adăugăm header-ul tabelului
            table.Rows[0].Cells[0].Paragraphs[0].Append("Id");
            table.Rows[0].Cells[1].Paragraphs[0].Append("Type");
            table.Rows[0].Cells[2].Paragraphs[0].Append("Address");
            table.Rows[0].Cells[3].Paragraphs[0].Append("Price");
            table.Rows[0].Cells[4].Paragraphs[0].Append("RoomNumber");
            table.Rows[0].Cells[5].Paragraphs[0].Append("ImageUrl");

            // Populăm tabelul cu datele din lista de case
            for (int i = 0; i < houses.Count; i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs[0].Append(houses[i].Id.ToString());
                table.Rows[i + 1].Cells[1].Paragraphs[0].Append(houses[i].Type.ToString());
                table.Rows[i + 1].Cells[2].Paragraphs[0].Append(houses[i].Address);
                table.Rows[i + 1].Cells[3].Paragraphs[0].Append(houses[i].Price.ToString());
                table.Rows[i + 1].Cells[4].Paragraphs[0].Append(houses[i].RoomNumber.ToString());
                table.Rows[i + 1].Cells[5].Paragraphs[0].Append(houses[i].ImageUrl);
            }

            // Salvăm documentul
            doc.Save();
        }
    }
    
}