using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep3 World!");

    Random random = new Random();
        bool playAgain = true;

        do
        {
            int magicNumber = random.Next(1, 101);

            PlayGame(magicNumber);

            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainResponse = Console.ReadLine().ToLower();

            playAgain = (playAgainResponse == "yes");
        } while (playAgain);

        Console.WriteLine("Thanks for playing! Goodbye.");
    }

    static void PlayGame(int magicNumber)
    {
        int guess;
        int attempts = 0;

        Console.WriteLine("Welcome to the Guess My Number game!");

        do
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            attempts++;

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

        } while (guess != magicNumber);

        // Display the number of attempts
        Console.WriteLine($"It took you {attempts} attempts to guess the magic number.");    
    }
}