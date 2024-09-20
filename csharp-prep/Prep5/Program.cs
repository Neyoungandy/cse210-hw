using System;

class Program
{
     // Function 1: Display a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function 2: Prompt the user for their name and return it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // Function 3: Prompt the user for their favorite number and return it
    static int PromptUserNumber()
    {
        int number;
        bool isValid = false;

        // Loop until a valid number is entered
        do
        {
            Console.Write("Please enter your favorite number: ");
            string input = Console.ReadLine();

            // Try parsing the input to an integer
            if (int.TryParse(input, out number))
            {
                isValid = true; // Input is valid, exit the loop
            }
            else
            {
                // Display an error message and prompt again
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        } while (!isValid); // Keep looping until valid input is entered

        return number;
    }

    // Function 4: Accept an integer, square it, and return the squared value
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function 5: Display the result by accepting the user's name and the squared number
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }

    static void Main(string[] args)
    {
        // Call the functions step by step

        // 1. Display a welcome message
        DisplayWelcome();

        // 2. Get the user's name
        string userName = PromptUserName();

        // 3. Get the user's favorite number with validation
        int favoriteNumber = PromptUserNumber();

        // 4. Square the user's favorite number
        int squaredNumber = SquareNumber(favoriteNumber);

        // 5. Display the result
        DisplayResult(userName, squaredNumber);
    }
}
