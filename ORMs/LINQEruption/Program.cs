List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46,"Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};
// Example Query - Prints all Stratovolcano eruptions
IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(c => c.Type == "Stratovolcano");
PrintEach(stratovolcanoEruptions, "Stratovolcano eruptions.");


// First Chile Eruption
IEnumerable<Eruption> firstChileEruption = eruptions.Where(e => e.Location == "Chile").OrderBy(e => e.Year).Take(1);
PrintEach(firstChileEruption, "First eruption in Chile.");

// First Hawaiian Is Eruption
IEnumerable<Eruption> firstHawaiianEruption = eruptions.Where(e => e.Location == "Hawaiian Is").OrderBy(e => e.Year).Take(1);
if (firstHawaiianEruption.Count() == 0)
    Console.WriteLine("No Hawaiian Is eruptions found.");
else
    PrintEach(firstHawaiianEruption, "First eruption in the Hawaiian Is.");

// First Eruption After 1900 AND in "New Zealand"
IEnumerable<Eruption> firstNZEruption = eruptions.Where(e => e.Location == "New Zealand" && e.Year > 1900).OrderBy(e => e.Year).Take(1);
PrintEach(firstNZEruption, "First eruption in New Zealand after 1900.");

// Eruptions Where Volcano's Elevation is greater than 2000
IEnumerable<Eruption> highElevationEruptions = eruptions.Where(e => e.ElevationInMeters > 2000).OrderBy(e => e.ElevationInMeters);
PrintEach(highElevationEruptions, "Eruptions with elevation greater than 2000 meters");

// Eruptions Where Volcano's Name starts with "L"
IEnumerable<Eruption> startsWithLEruptions = eruptions.Where(e => e.Volcano.StartsWith("L"));
PrintEach(startsWithLEruptions, $"Eruptions with names starting with L ({startsWithLEruptions.Count()} found).");

// Highest Elevation
int highestElevation = eruptions.Max(e => e.ElevationInMeters);
Console.WriteLine($"Highest Elevation: {highestElevation} meters");

// Volcano with the highest Elevation
string volcanoWithHighestElevation = eruptions.Where(e => e.ElevationInMeters == highestElevation).Select(e => e.Volcano).First();
Console.WriteLine($"Volcano with the highest elevation: {volcanoWithHighestElevation}");

// All Volcano Names
IEnumerable<string> volcanoNames = eruptions.Select(e => e.Volcano).OrderBy(n => n);
PrintEach(volcanoNames, "Volcano Names");

// Eruptions That Happened Before 1000CE
IEnumerable<Eruption> before1000CE = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano);
PrintEach(before1000CE, "Eruptions that happened before 1000CE.");

// Bonus: Eruptions That Happened Before 1000CE (Volcano Names Only)
IEnumerable<string> volcanoNamesBefore1000CE = before1000CE.Select(e => e.Volcano).OrderBy(n => n);
PrintEach(volcanoNamesBefore1000CE, "Volcano Names for eruptions that happened before 1000CE.");

// Helper method to print each item in a List or IEnumerable.This should remain at the bottom of your class!
static void PrintEach(IEnumerable<dynamic> items, string msg = "")
{
    Console.WriteLine("\n" + msg);
    foreach (var item in items)
    {
        Console.WriteLine(item.ToString());
    }
}
