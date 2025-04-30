using WebScraperCsharp.Services;

namespace WebScraperCsharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scraperService = new WebScraperService();
            var csvExportService = new CsvExportService();

            Console.WriteLine("Enter the file name to save the CSV file (e.g., RiskyShops.csv), or type 'default' to save it in the Downloads folder:");
            var inputPath = Console.ReadLine();

            string filePath;
            if (string.IsNullOrWhiteSpace(inputPath) || inputPath.ToLower() == "default")
            {
                var downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                downloadsFolder = Path.Combine(downloadsFolder, "Downloads");

                filePath = Path.Combine(downloadsFolder, "RiskyShops.csv");
            }
            else
            {
                filePath = inputPath;
            }

            Console.WriteLine($"The CSV file will be saved to: {filePath}");

            var url = "https://www.soi.sk/sk/informacie-pre-verejnost/internetove-obchody/rizikove-internetove-obchody.soi";

            Console.WriteLine("Fetching Risky Shops...");
            var riskyShops = await scraperService.GetRiskyShopsAsync(url);

            Console.WriteLine("Risky Shops Found:");
            foreach (var shop in riskyShops)
            {
                Console.WriteLine($"Domain: {shop.Domain}, PDF Link: {shop.PdfLink}");
            }

            csvExportService.ExportToCsv(filePath, riskyShops);
        }
    }
}