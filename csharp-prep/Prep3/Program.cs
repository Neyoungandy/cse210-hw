using System;

class Program
{
    static void Main(string[] args)
    {
 string playAgain = "yes";

        while (playAgain == "yes")
        {
            // Generate a random magic number between 1 and 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            int guess = -1;
            int guessCount = 0; // Variable to track the number of guesses

            // Loop until the guess matches the magic number
            while (guess != 20)
            {
                // Ask the user for their guess
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++; // Increment the guess count

                // Check if the guess is higher, lower, or correct
                if (guess > 20)
                {
                    Console.WriteLine("Lower");
                }
                else if (guess < 20)
                {
                    Console.WriteLine("Higher");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {guessCount} tries!");
                }
            }

            // Ask if they want to play again
            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine().ToLower();
        }

        Console.WriteLine("Thanks for playing!");
    }
}