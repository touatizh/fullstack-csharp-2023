// Basic arrays
int[] zeroToNine = {0,1,2,3,4,5,6,7,8,9};
string[] names = {"Tim", "Martin", "Nikki", "Sara"};
bool[] altArray = {true, false, true, false, true, false, true, false, true, false};

// List of flavors
Console.WriteLine("***** list of flavors *****");
List<string> flavors = new List<string>();
flavors.Add("Chocolate");
flavors.Add("Vanilla");
flavors.Add("Strawberry");
flavors.Add("Mint Chocolate Chip");
flavors.Add("Chocolate Chip Cookie Dough");
Console.WriteLine($"ice cream flavors count: {flavors.Count}");
Console.WriteLine($"third flavor: {flavors[2]}");
flavors.RemoveAt(2);
Console.WriteLine($"ice cream flavors count: {flavors.Count}");

Console.WriteLine("***** user info dictionary *****");
Dictionary<string,string> userInfo = new Dictionary<string,string>();
Random rand = new Random();
foreach (string name in names)
{
    userInfo.Add(name, flavors[rand.Next(flavors.Count)]);
}
foreach (KeyValuePair<string,string> entry in userInfo)
{
    Console.WriteLine($"{entry.Key}: {entry.Value}");
}
