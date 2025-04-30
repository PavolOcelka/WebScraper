using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace WebScraperCsharp.Services
{
    public class CsvExportService
    {
        public void ExportToCsv(string filePath, List<RiskyShop> riskyShops)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteHeader<RiskyShop>();
                    csv.NextRecord();

                    foreach (var shop in riskyShops)
                    {
                        csv.WriteRecord(shop);
                        csv.NextRecord();
                    }
                }

                Console.WriteLine($"Data successfully exported to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while exporting to CSV: {ex.Message}");
            }
        }
    }
}