using CsvHelper.Configuration;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace WebScraperCsharp.Services
{
    public class WebScraperService
    {
        private readonly HttpClient _httpClient;

        public WebScraperService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<RiskyShop>> GetRiskyShopsAsync(string url)
{
    var riskyShops = new List<RiskyShop>();

    try
    {
        var response = await _httpClient.GetStringAsync(url);

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        var wrapDiv = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='wrap']");

        if (wrapDiv != null)
        {
            var pdfNodes = wrapDiv.SelectNodes(".//a[contains(@class, 'pdf')]");

            if (pdfNodes != null)
            {
                foreach (var node in pdfNodes)
                {
                    var link = node.GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(link))
                    {
                        link = Uri.EscapeUriString(link);

                        var domains = ExtractDomains(node, link);

                        foreach (var domain in domains)
                        {
                            riskyShops.Add(new RiskyShop(domain, link));
                        }
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while scraping: {ex.Message}");
    }

    return riskyShops;
}

        private List<string> ExtractDomains(HtmlNode node, string link)
        {
            var domains = new List<string>();

            var domainText = node.InnerText.Trim();
            if (!string.IsNullOrEmpty(domainText))
            {
                domains.AddRange(SplitAndCleanDomains(domainText));
                return domains;
            }

            var strongNode = node.SelectSingleNode(".//strong");
            if (strongNode != null)
            {
                domainText = strongNode.InnerText.Trim();
                if (!string.IsNullOrEmpty(domainText))
                {
                    domains.AddRange(SplitAndCleanDomains(domainText));
                    return domains;
                }
            }

            var spanNode = node.SelectSingleNode(".//span");
            if (spanNode != null)
            {
                domainText = spanNode.InnerText.Trim();
                if (!string.IsNullOrEmpty(domainText))
                {
                    domains.AddRange(SplitAndCleanDomains(domainText));
                    return domains;
                }
            }

            var match = Regex.Match(link, @"internetove%20obchody/([^/]+)");
            if (match.Success)
            {
                domainText = match.Groups[1].Value.Replace(".docx.pdf", "").Replace(".pdf", "");
                domains.AddRange(SplitAndCleanDomains(domainText));
            }

            if (domains.Count == 0)
            {
                domains.Add(link);
            }

            return domains;
        }

        private List<string> SplitAndCleanDomains(string domainText)
{
    var domains = new List<string>();

    Console.WriteLine($"Original domain text: {domainText}");

    domainText = domainText.Trim().Trim('-').Trim('–').Trim();

    if (domainText.Count(c => c == '.') > 2)
    {
        var splitDomains = Regex.Matches(domainText, @"[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}")
                                .Cast<Match>()
                                .Select(m => m.Value)
                                .ToList();

        foreach (var domain in splitDomains)
        {
            var cleanedDomain = domain.TrimEnd('.');
            if (Regex.IsMatch(cleanedDomain, @"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                domains.Add(cleanedDomain);
            }
        }
    }
    else
    {
        domainText = Regex.Replace(domainText, @"\s+", "");

        Console.WriteLine($"Domain text after removing spaces: {domainText}");

        domainText = Regex.Replace(domainText, @"\bwww\.", "", RegexOptions.IgnoreCase);

        Console.WriteLine($"Domain text after removing 'www.': {domainText}");

        if (Regex.IsMatch(domainText, @"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            domains.Add(domainText);
        }
        else
        {
            Console.WriteLine($"Invalid domain skipped: {domainText}");
        }
    }

    domains = domains.Select(d => Regex.Replace(d, @"\bwww\.", "", RegexOptions.IgnoreCase)).ToList();

    return domains;
}
    }
}