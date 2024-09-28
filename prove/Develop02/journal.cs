using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    // List of entries for the journal
    public List<Entry> Entries { get; private set; } = new List<Entry>();

    // Adds an entry to the journal
    public void AddEntry(Entry entry)
    {
        Entries.Add(entry);
    }

    // Displays all the journal entries
    public void DisplayEntries()
    {
        if (Entries.Count == 0)
        {
            Console.WriteLine("No entries to display.");
        }
        else
        {
            foreach (var entry in Entries)
            {
                entry.Display();
            }
        }
    }

    // Saves the journal entries to a file
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("....");
    }

    // Loads journal entries from a file
    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            Entries.Clear();
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)

                {
                    Entry entry = new Entry(parts[1], parts[2])
                    {
                        Date = parts[0]  // Assigns the date part of the entry
                    };
                    Entries.Add(entry);                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
