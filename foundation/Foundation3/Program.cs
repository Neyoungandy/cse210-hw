using System;
using System.Collections.Generic;

// Base Class
public abstract class Activity
{
    // Member Variables
    private DateTime _date;
    private int _minutes;

    // Constructor
    protected Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Properties
    public DateTime Date => _date;
    public int Minutes => _minutes;

    // Virtual Methods for Distance, Speed, and Pace
    public virtual double GetDistance()
    {
        return 0; // Base implementation (to be overridden)
    }

    public virtual double GetSpeed()
    {
        return 0; // Base implementation (to be overridden)
    }

    public virtual double GetPace()
    {
        return 0; // Base implementation (to be overridden)
    }

    // GetSummary Method
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min) - Distance {GetDistance()} " +
               $"{(GetType().Name == "Swimming" ? "laps" : "miles")}, Speed: {GetSpeed()}, Pace: {GetPace()}";
    }
}

// Derived Class: Running
public class Running : Activity
{
    private double _distance; // Distance in miles

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance; // Distance is already provided
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60; // Speed in mph
    }

    public override double GetPace()
    {
        return Minutes / GetDistance(); // Pace in min per mile
    }
}

// Derived Class: Cycling
public class Cycling : Activity
{
    private double _speed; // Speed in mph

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed / 60) * Minutes; // Distance in miles
    }

    public override double GetSpeed()
    {
        return _speed; // Speed is already provided
    }

    public override double GetPace()
    {
        return 60 / _speed; // Pace in min per mile
    }
}

// Derived Class: Swimming
public class Swimming : Activity
{
    private int _laps; // Number of laps

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return _laps * 50 / 1000.0; // Distance in km (1 lap = 50m)
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60; // Speed in kph
    }

    public override double GetPace()
    {
        return Minutes / GetDistance(); // Pace in min per km
    }

    public override string GetSummary()
    {
        return $"{Date:dd MMM yyyy} Swimming ({Minutes} min) - Distance {GetDistance()} km, " +
               $"Speed: {GetSpeed()} kph, Pace: {GetPace()} min per km";
    }
}

// Program Entry Point
public class Program
{
    public static void Main(string[] args)
    {
        // Create a list of activities
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 12.0),
            new Swimming(new DateTime(2022, 11, 5), 30, 20)
        };

        // Iterate through the list and display summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
