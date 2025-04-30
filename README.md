# WebScraperCsharp

Tento projekt je jednoduchý web scraper, ktorý extrahuje rizikové internetové obchody zo stránky SOI a exportuje ich do CSV súboru.

## Požiadavky
- **.NET SDK 6.0 alebo novší**:
  - Stiahnite si a nainštalujte .NET SDK z [dotnet.microsoft.com](https://dotnet.microsoft.com/download).

## Ako Spustiť
1. **Rozbaľte ZIP súbor**:
   - Rozbaľte súbor `WebScraperCsharp.zip` do priečinka na vašom počítači.

2. **Otvorte Terminál alebo Príkazový Riadok**:
   - Na Macu: Otvorte **Terminal**.
   - Na Windows: Otvorte **Command Prompt** alebo **PowerShell**.

3. **Prejdite do Priečinka Projektu**:
   - Použite príkaz `cd`, aby ste sa dostali do priečinka projektu:
     ```bash
     cd cesta/k/priečinku/WebScraperCsharp
     ```

4. **Obnovte Závislosti**:
   - Spustite príkaz:
     ```bash
     dotnet restore
     ```
   - Tento príkaz stiahne všetky potrebné NuGet balíčky (napr. `HtmlAgilityPack`, `CsvHelper`).

5. **Spustite Program**:
   - Spustite program príkazom:
     ```bash
     dotnet run
     ```

6. **Zadajte Cestu pre CSV Súbor**:
   - Program vás vyzve, aby ste zadali cestu pre uloženie CSV súboru, alebo napíšte `default`, aby sa súbor uložil do priečinka **Downloads**.

## Príklad CSV Výstupu
Po spustení program vytvorí CSV súbor s dvoma stĺpcami: `Domain` a `PdfLink`.

```csv
Domain,PdfLink
kutilsro.sk,https://www.soi.sk/files/documents/info-verejnost/internetove%20obchody/tatiana%20kameníková_upozornenie%20pre%20spotrebiteľov.pdf
led-zrkadla.sk,https://www.soi.sk/files/documents/info-verejnost/internetove%20obchody/tatiana%20kameníková_upozornenie%20pre%20spotrebiteľov.pdf
...
