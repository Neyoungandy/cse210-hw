using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json; // For JSON serialization
using System.Text;

namespace JournalApp
{
    // Represents a single journal entry
    class Entry
    {
        // Member variables using _underscoreCamelCase
        public string Date { get; }
        public string Prompt { get; }
        public string Response { get; }
        public string Emotion { get; } // Additional information

        // Constructor
        public Entry(string prompt, string response, string emotion)
        {
            Date = DateTime.Now.ToShortDateString();
            Prompt = prompt;
            Response = response;
            Emotion = emotion;
        }

        // Override ToString() for display purposes
        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\nEmotion: {Emotion}\n";
        }
    }

    // Manages the collection of journal entries
    class Journal
    {
        // Member variables
        private List<Entry> _entries = new List<Entry>();
        private static Random _random = new Random();

        // List of prompts
        private List<string> _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What did I learn today?",
            "What am I grateful for today?"
        };

        // List of emotions for tracking
        private List<string> _emotions = new List<string>
        {
            "Happy",
            "Sad",
            "Excited",
            "Anxious",
            "Content",
            "Frustrated",
            "Inspired"
        };

        // Adds a new entry to the journal
        public void AddEntry()
        {
            string prompt = GetRandomPrompt();
            Console.WriteLine($"\nPrompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            // Emotion tracking
            Console.WriteLine("\nSelect your current emotion from the list below:");
            for (int i = 0; i < _emotions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_emotions[i]}");
            }
            Console.Write("Enter the number corresponding to your emotion: ");
            string emotionChoice = Console.ReadLine();
            string selectedEmotion = "Neutral"; // Default emotion

            if (int.TryParse(emotionChoice, out int emotionIndex) && emotionIndex >= 1 && emotionIndex <= _emotions.Count)
            {
                selectedEmotion = _emotions[emotionIndex - 1];
            }
            else
            {
                Console.WriteLine("Invalid choice. Emotion set to 'Neutral'.");
            }

            _entries.Add(new Entry(prompt, response, selectedEmotion));
            Console.WriteLine("Entry added successfully!\n");
        }

        // Displays all entries in the journal
        public void DisplayEntries()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("\nNo entries to display.\n");
                return;
            }

            Console.WriteLine("\n--- Journal Entries ---\n");
            foreach (Entry entry in _entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }

        // Saves the journal entries to a file in CSV format
        public void SaveToFileCSV(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    // Write header
                    writer.WriteLine("Date,Prompt,Response,Emotion");
                    foreach (Entry entry in _entries)
                    {
                        // Escape quotes by doubling them
                        string prompt = entry.Prompt.Replace("\"", "\"\"");
                        string response = entry.Response.Replace("\"", "\"\"");
                        string emotion = entry.Emotion.Replace("\"", "\"\"");

                        writer.WriteLine($"\"{entry.Date}\",\"{prompt}\",\"{response}\",\"{emotion}\"");
                    }
                }
                Console.WriteLine("Journal saved successfully in CSV format.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving journal: {ex.Message}\n");
            }
        }

        // Loads journal entries from a CSV file
        public void LoadFromFileCSV(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.\n");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filename);
                _entries.Clear();

                // Skip header
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    // Split by comma not inside quotes
                    var parts = SplitCsvLine(line);
                    if (parts.Length == 4)
                    {
                        string date = parts[0];
                        string prompt = parts[1];
                        string response = parts[2];
                        string emotion = parts[3];
                        _entries.Add(new Entry(prompt, response, emotion) { /* Set date manually */ });
                    }
                }
                Console.WriteLine("Journal loaded successfully from CSV.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading journal: {ex.Message}\n");
            }
        }

        // Splits a CSV line taking into account quoted commas
        private string[] SplitCsvLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            StringBuilder field = new StringBuilder();

            foreach (char c in line)
            {
                if (c == '"' )
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }
            fields.Add(field.ToString());
            return fields.ToArray();
        }

        // Saves the journal entries to a file in JSON format
        public void SaveToFileJSON(string filename)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, jsonString);
                Console.WriteLine("Journal saved successfully in JSON format.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving journal: {ex.Message}\n");
            }
        }

        // Loads journal entries from a JSON file
        public void LoadFromFileJSON(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.\n");
                return;
            }

            try
            {
                string jsonString = File.ReadAllText(filename);
                _entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);
                Console.WriteLine("Journal loaded successfully from JSON.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading journal: {ex.Message}\n");
            }
        }

        // Displays the menu options
        public void DisplayMenu()
        {
            Console.WriteLine("=== Journal Menu ===");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a CSV file");
            Console.WriteLine("4. Load the journal from a CSV file");
            Console.WriteLine("5. Save the journal to a JSON file");
            Console.WriteLine("6. Load the journal from a JSON file");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
        }

        // Selects a random prompt from the list
        private string GetRandomPrompt()
        {
            int index = _random.Next(_prompts.Count);
            return _prompts[index];
        }
    }

    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            Console.WriteLine("Welcome to the Journal Program!");

            while (running)
            {
                journal.DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter filename to save as CSV (e.g., journal.csv): ");
                        string saveCsvFilename = Console.ReadLine();
                        journal.SaveToFileCSV(saveCsvFilename);
                        break;
                    case "4":
                        Console.Write("Enter filename to load from CSV (e.g., journal.csv): ");
                        string loadCsvFilename = Console.ReadLine();
                        journal.LoadFromFileCSV(loadCsvFilename);
                        break;
                    case "5":
                        Console.Write("Enter filename to save as JSON (e.g., journal.json): ");
                        string saveJsonFilename = Console.ReadLine();
                        journal.SaveToFileJSON(saveJsonFilename);
                        break;
                    case "6":
                        Console.Write("Enter filename to load from JSON (e.g., journal.json): ");
                        string loadJsonFilename = Console.ReadLine();
                        journal.LoadFromFileJSON(loadJsonFilename);
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("Exiting the Journal Program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }
    }
}
