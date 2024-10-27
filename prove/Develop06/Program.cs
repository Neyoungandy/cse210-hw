using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;
    private int _level;
    private int _experiencePoints;

    public GoalManager()
    {
        _score = 0;
        _level = 1;
        _experiencePoints = 0;
    }

    public void Start()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine($"Level: {_level} | Experience: {_experiencePoints}");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("Choose a goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Negative Goal"); // New goal type
        Console.WriteLine("5. Large Goal"); // New goal type for larger goals
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter goal points: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter target completions: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            case "4":
                Console.Write("Enter penalty points: ");
                int penalty = int.Parse(Console.ReadLine());
                _goals.Add(new NegativeGoal(name, description, points, penalty));
                break;
            case "5":
                Console.Write("Enter target points: ");
                int targetPoints = int.Parse(Console.ReadLine());
                _goals.Add(new LargeGoal(name, description, points, targetPoints));
                break;
            default:
                Console.WriteLine("Invalid choice. Goal creation failed.");
                break;
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("Goals:");
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void SaveGoals()
    {
        Console.Write("Enter filename to save goals: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            writer.WriteLine(_level);
            writer.WriteLine(_experiencePoints);
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals successfully saved!");
    }

    public void LoadGoals()
    {
        Console.Write("Enter filename to load goals: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _goals.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                _score = int.Parse(reader.ReadLine());
                _level = int.Parse(reader.ReadLine());
                _experiencePoints = int.Parse(reader.ReadLine());

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string goalType = parts[0];
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);

                    if (goalType == "SimpleGoal")
                    {
                        bool isComplete = bool.Parse(parts[4]);
                        _goals.Add(new SimpleGoal(name, description, points, isComplete));
                    }
                    else if (goalType == "EternalGoal")
                    {
                        _goals.Add(new EternalGoal(name, description, points));
                    }
                    else if (goalType == "ChecklistGoal")
                    {
                        int amountCompleted = int.Parse(parts[4]);
                        int target = int.Parse(parts[5]);
                        int bonus = int.Parse(parts[6]);
                        _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
                    }
                    else if (goalType == "NegativeGoal")
                    {
                        int penalty = int.Parse(parts[4]);
                        _goals.Add(new NegativeGoal(name, description, points, penalty));
                    }
                    else if (goalType == "LargeGoal")
                    {
                        int targetPoints = int.Parse(parts[4]);
                        _goals.Add(new LargeGoal(name, description, points, targetPoints));
                    }
                }
            }
            Console.WriteLine("Goals successfully loaded!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Select a goal to record an event:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }

        int choice = int.Parse(Console.ReadLine());

        if (choice > 0 && choice <= _goals.Count)
        {
            Goal goal = _goals[choice - 1];
            goal.RecordEvent();
            _score += goal.Points;
            _experiencePoints += goal.Points; // Add points to experience

            // Level up if enough experience points are earned
            if (_experiencePoints >= _level * 10) // Assuming 10 XP needed per level
            {
                _level++;
                Console.WriteLine($"Congratulations! You've leveled up to Level {_level}!");
            }
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }
}

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();
    public virtual string GetDetailsString()
    {
        return $"{(IsComplete() ? "[X]" : "[ ]")} {_shortName} - {_description} : {_points} points";
    }

    public int Points => _points;
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"{_shortName} completed! +{_points} points.");
        }
        else
        {
            Console.WriteLine($"{_shortName} is already complete.");
        }
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_shortName}|{_description}|{_points}|{_isComplete}";
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Eternal goal '{_shortName}' completed! +{_points} points.");
    }

    public override bool IsComplete()
    {
        return false; // Eternal goals never complete
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_shortName}|{_description}|{_points}";
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted = 0)
        : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++;
            int totalPoints = _amountCompleted == _target ? _points + _bonus : _points;
            Console.WriteLine($"Goal '{_shortName}' updated: {_amountCompleted}/{_target}. +{totalPoints} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{_shortName}' is already completed.");
        }
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_shortName}|{_description}|{_points}|{_amountCompleted}|{_target}|{_bonus}";
    }
}

public class NegativeGoal : Goal
{
    private int _penalty;

    public NegativeGoal(string name, string description, int points, int penalty)
        : base(name, description, points)
    {
        _penalty = penalty;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Negative goal '{_shortName}' triggered! -{_penalty} points.");
    }

    public override bool IsComplete()
    {
        return false; // Negative goals don't complete
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal|{_shortName}|{_description}|{_points}|{_penalty}";
    }
}

public class LargeGoal : Goal
{
    private int _targetPoints;

    public LargeGoal(string name, string description, int points, int targetPoints)
        : base(name, description, points)
    {
        _targetPoints = targetPoints;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Large goal '{_shortName}' achieved! +{_points} points.");
    }

    public override bool IsComplete()
    {
        return false; // Large goals can be ongoing
    }

    public override string GetStringRepresentation()
    {
        return $"LargeGoal|{_shortName}|{_description}|{_points}|{_targetPoints}";
    }
}
