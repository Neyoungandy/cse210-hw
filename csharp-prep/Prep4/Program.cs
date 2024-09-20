using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int number = -1;

        // Ask the user to enter a list of numbers, stop when they enter 0
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (number != 0)
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine());

            // Do not add the 0 to the list
            if (number != 0)
            {
                numbers.Add(number);
            }
        }

        // Core Requirement 1: Compute the sum of the numbers
        int sum = numbers.Sum();
        Console.WriteLine($"The sum is: {sum}");

        // Core Requirement 2: Compute the average of the numbers
        if (numbers.Count > 0)
        {
            double average = numbers.Average();
            Console.WriteLine($"The average is: {average}");
        }

        // Core Requirement 3: Find the maximum number
        int max = numbers.Max();
        Console.WriteLine($"The largest number is: {max}");
    }
}