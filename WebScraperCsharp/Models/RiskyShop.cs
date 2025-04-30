namespace WebScraperCsharp
{
    public class RiskyShop
    {
        public string Domain { get; set; }
        public string PdfLink { get; set; }

        public RiskyShop(string domain, string pdfLink) 
        {
            Domain = domain;
            PdfLink = pdfLink;
        }
        
    }
}