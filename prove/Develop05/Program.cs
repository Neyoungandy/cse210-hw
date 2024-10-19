using System;
using System.Collections.Generic;
using System.Threading;

// Base class: Activity
public class Activity
{
    // Private member variables for encapsulation
    private string _name;
    private string _description;
    private int _duration;

    // Constructor for Activity class
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Get the duration of the activity from the user
    public void GetDuration()
    {
        Console.Write("Enter the duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
    }

    // Method to display the starting message
    public void StartActivity()
    {
        Console.WriteLine($"Starting {_name}: {_description}");
        GetDuration();
        Console.WriteLine("Get ready to begin...");
        ShowSpinner(3);  // Pause for 3 seconds before starting
    }

    // Method to display the ending message
    public void EndActivity()
    {
        Console.WriteLine("Well done! You have completed the activity.");
        Console.WriteLine($"You completed {_name} for {_duration} seconds.");
        ShowSpinner(3);  // Pause for 3 seconds before finishing
    }

    // Animation method (spinner)
    public void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("/\b");
            Thread.Sleep(250);
            Console.Write("-\b");
            Thread.Sleep(250);
            Console.Write("\\\b");
            Thread.Sleep(250);
            Console.Write("|\b");
            Thread.Sleep(250);
        }
        Console.WriteLine(); // Finish with a new line
    }

    // Protected getter for duration (if needed in derived classes)
    protected int GetDurationTime()
    {
        return _duration;
    }
}

// Derived Class: BreathingActivity
public class BreathingActivity : Activity
{
    // Constructor calls base constructor
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly.")
    {
    }

    // Method for running the breathing activity
    public void RunActivity()
    {
        StartActivity();

        int duration = GetDurationTime();
        int cycleTime = 5; // 5 seconds for each breath in and out
        int cycles = duration / cycleTime;

        for (int i = 0; i < cycles; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(2);  // Pauses for 2 seconds
            Console.WriteLine("Breathe out...");
            ShowSpinner(3);  // Pauses for 3 seconds
        }

        EndActivity();
    }
}

// Derived Class: ReflectingActivity
public class ReflectingActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you feel when it was complete?",
        "What did you learn about yourself?"
    };

    // Constructor
    public ReflectingActivity() : base("Reflection Activity", "This activity helps you reflect on your personal strengths.")
    {
    }

    // Method for running the reflecting activity
    public void RunActivity()
    {
        StartActivity();

        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);

        int duration = GetDurationTime();
        int questionTime = 4; // Allow 4 seconds to reflect per question
        int questionsCount = duration / questionTime;

        for (int i = 0; i < questionsCount; i++)
        {
            Console.WriteLine(questions[random.Next(questions.Length)]);
            ShowSpinner(4);  // Pauses for 4 seconds per question
        }

        EndActivity();
    }
}

// Derived Class: ListingActivity
public class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };

    // Constructor
    public ListingActivity() : base("Listing Activity", "This activity helps you list positive aspects of your life.")
    {
    }

    // Method for running the listing activity
    public void RunActivity()
    {
        StartActivity();

        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);

        List<string> userResponses = new List<string>();

        Console.WriteLine("Start listing your items:");
        DateTime endTime = DateTime.Now.AddSeconds(GetDurationTime());

        while (DateTime.Now < endTime)
        {
            Console.Write("Item: ");
            userResponses.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {userResponses.Count} items.");
        EndActivity();
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflecting Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an activity: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.RunActivity();
                    break;

                case "2":
                    ReflectingActivity reflectingActivity = new ReflectingActivity();
                    reflectingActivity.RunActivity();
                    break;

                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.RunActivity();
                    break;

                case "4":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
