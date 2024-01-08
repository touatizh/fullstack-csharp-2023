class Program
{
    static void Main()
    {
        Human human1 = new Human("John", 10, 10, 10, 100);
        Human human2 = new Human("Alice", 10, 10, 10, 100);

        // Testing Wizard Class
        Wizard wizard = new Wizard("Merlin", 5, 5);
        // Wizard attacks a human
        wizard.Attack(human1); // Expecting John's health to decrease and Merlin's health to increase
        Console.WriteLine($"=> John's health is now {human1.Health}");
        // Wizard heals a human
        wizard.Heal(human1); // Expecting John's health to increase
        Console.WriteLine($"=> John's health is now {human1.Health}");

        // Test Ninja Class
        Ninja ninja = new Ninja("Shadow", 5, 5, 100);
        // Ninja attacks a human
        ninja.Attack(human2); // Expecting Alice's health to decrease, potentially with additional damage
        Console.WriteLine($"=> Alice's health is now {human2.Health}");
        // Ninja steals health from a human
        ninja.Steal(human2); // Expecting Alice's health to decrease and Shadow's health to increase
        Console.WriteLine($"=> Shadow's health is now {ninja.Health}");
        Console.WriteLine($"=> Alice's health is now {human2.Health}");

        // Testing Samurai Class
        Samurai samurai = new Samurai("Gojo", 10, 10, 10);
        // Samurai attacks a human
        samurai.Attack(human2); // Expecting Alice's health to decrease, potentially to 0 if her health is 50 or less
        Console.WriteLine($"=> Alice's health is now {human2.Health}");
        // Samurai meditates
        ninja.Attack(samurai); // Expecting Gojo's health to decrease
        Console.WriteLine($"=> Gojo's health is now {samurai.Health}");
        samurai.Meditate(); // Expecting Gojo's health to reset to 200
        Console.WriteLine($"Gojo's health after meditation: {samurai.Health}");

        // Print out the health of all characters after interactions
        Console.WriteLine("*** After Interactions ***");
        Console.WriteLine($"John's Health: {human1.Health}");
        Console.WriteLine($"Alice's Health: {human2.Health}");
        Console.WriteLine($"Merlin's Health: {wizard.Health}");
        Console.WriteLine($"Shadow's Health: {ninja.Health}");
        Console.WriteLine($"Gojo's Health: {samurai.Health}");
    }
}

class Human
{
    public string Name { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Dexterity { get; set; }
    public int Health { get; set; }



    public Human(string name, int str, int intel, int dex, int hp)
    {
        Name = name;
        Strength = str;
        Intelligence = intel;
        Dexterity = dex;
        Health = hp;
    }

    // Build Attack method
    public virtual int Attack(Human target)
    {
        int dmg = Strength * 3;
        target.Health -= dmg;
        Console.WriteLine($"{Name} attacked {target.Name} for {dmg} damage!");
        return target.Health;
    }
}



// Class Wizard
class Wizard : Human
{
    public Wizard(string name, int str, int dex) : base(name, str, 25, dex, 50) { }
    public override int Attack(Human target)
    {
        int dmg = Intelligence * 3;
        target.Health -= dmg;
        Health += dmg;
        Console.WriteLine($"{Name} attacked {target.Name} for {dmg} damage!");
        return target.Health;
    }

    public int Heal(Human target)
    {
        int heal = Intelligence * 3;
        target.Health += heal;
        Console.WriteLine($"{Name} healed {target.Name} for {heal} hp!");
        return target.Health;
    }
}



// Class Ninja 
class Ninja : Human
{
    public Ninja(string name, int str, int intel, int hp) : base(name, str, intel, 75, hp) { }

    public override int Attack(Human target)
    {
        Random rand = new Random();
        int dmg = Dexterity;

        if (rand.NextDouble() >= 0.8) dmg += 10;
        target.Health -= dmg;
        Console.WriteLine($"{Name} attacked {target.Name} for {dmg} damage!");
        return target.Health;
    }

    public int Steal(Human target)
    {
        int stolenHp = 5;
        target.Health -= stolenHp;
        Health += stolenHp;
        Console.WriteLine($"{Name} stole {stolenHp} hp from {target.Name}!");
        return target.Health;
    }
}


// Class Samurai
class Samurai : Human
{
    public Samurai(string name, int str, int intel, int dex) : base(name, str, intel, dex, 200) { }

    public override int Attack(Human target)
    {
        int targetHealth = base.Attack(target);
        if (targetHealth <= 50) target.Health = 0;
        return target.Health;
    }

    public void Meditate()
    {
        Health = 200;
    }
}
