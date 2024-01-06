// Random Array
Console.WriteLine("***** Random Array *****");
static int[] RandomArray()
{
    // Initialization
    int[] ints = new int[10];
    Random rand = new Random();

    // Populate the array with random integers
    for (int i = 0; i < ints.Length; i++)
    {
        ints[i] = rand.Next(5, 26);
    }

    // Generate a random array and initialize the min, max, and sum
    int min = ints[0], max = ints[0];
    int sum = 0;

    // Iterate through the array to find min, max, and calculate the sum
    foreach (int e in ints)
    {
        // Update max if the current element is greater
        if (e > max)
        {
            max = e;
        }
        // Update min if the current element is smaller
        else if (e < min)
        {
            min = e;
        }

        // Accumulate the sum
        sum += e;
    }

    // Display the final min, max, and sum values
    Console.WriteLine($"Final min: {min}, max: {max}");
    Console.WriteLine($"Sum: {sum}");

    return ints;
}

RandomArray();

// Coin Flip
Console.WriteLine("***** Coin Flip *****");
static string TossCoin()
{
    // Initialization
    string[] sides = { "Heads", "Tails" };
    Random rand = new Random();

    // Randomly select either Heads or Tails
    int toss = rand.Next(sides.Length);
    string result = sides[toss];

    // Display the result of the coin toss
    Console.WriteLine(result);

    return result;
}

static double TossMultipleCoins(int nbrOfFlips)
{
    // Initialization
    List<string> results = new List<string>();

    // Toss the coin nbrOfFlips times
    for (int i = 0; i < nbrOfFlips+1; i++)
    {
        results.Add(TossCoin());
    }

    // Calculate the ratio of head toss to total tosses
    int headsCount = results.Count(x => x == "Heads");
    double ratio = (double)headsCount / nbrOfFlips;
    return ratio;
}

// Display the average Heads toss for 20 flips
Console.WriteLine($"Average Heads Toss for 20 flips: {TossMultipleCoins(20)}");


// Names
Console.WriteLine("***** Names *****");
static List<string> Names()
{
    // Initialization
    List<string> namesList = new List<string> {"Todd", "Tiffany", "Charlie", "Geneva", "Sydney"};
    List<string> newList = new List<string>();

    // Only keep names that are longer than 5 characters
    foreach (string name in namesList)
    {
        if (name.Length > 5 )
        {
            newList.Add(name);
        }
    }

    return newList;
}

// Display the new list
foreach (string name in Names())
{
    Console.Write($"{name} ");
}
