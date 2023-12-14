using System;
using System.Collections.Generic;

public abstract class Animal
{
    public string Name { get; set; }
    public abstract void MakeSound();
}

public class Lion : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Roar!");
    }
}

public class Elephant : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Trumpet!");
    }
}

public class Penguin : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Honk!");
    }
}

public class Zoo
{
    private List<Animal> animals = new List<Animal>();

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    public void DisplayAnimalInfo()
    {
        foreach (var animal in animals)
        {
            Console.WriteLine($"Name: {animal.Name}");
            animal.MakeSound();
            Console.WriteLine();
        }
    }
}

public class Mammal : Animal
{
    public int NumberOfLegs { get; set; }

    public override void MakeSound()
    {
        throw new NotImplementedException();
    }
}

public class Giraffe : Mammal
{
    public override void MakeSound()
    {
        Console.WriteLine("Silent Giraffe...");
    }
}

public class Snake : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Hiss!");
    }
}

class Program
{
    static void Main()
    {
        Zoo myZoo = new Zoo();

        Lion lion = new Lion { Name = "Simba" };
        Elephant elephant = new Elephant { Name = "Dumbo" };
        Penguin penguin = new Penguin { Name = "Skipper" };
        Giraffe giraffe = new Giraffe { Name = "Melman", NumberOfLegs = 4 };
        Snake snake = new Snake { Name = "Kaa" };

        myZoo.AddAnimal(lion);
        myZoo.AddAnimal(elephant);
        myZoo.AddAnimal(penguin);
        myZoo.AddAnimal(giraffe);
        myZoo.AddAnimal(snake);

        myZoo.DisplayAnimalInfo();
    }
}
