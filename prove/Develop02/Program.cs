using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Entry class to represent a journal entry
class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
    public List<string> Tags { get; set; }

    // Constructor for creating an entry
    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now;
        Tags = new List<string>();
    }

    // Display method to show the entry details
    public void Display()
    {
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
        Console.WriteLine($"Tags: {string.Join(", ", Tags)}\n");
    }
}

// Journal class to manage entries
class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    // Method to add a new entry to the journal
    public void AddEntry(string prompt, string response)
    {
        Entry newEntry = new Entry(prompt, response);
        entries.Add(newEntry);
    }

    // Method to display all entries in the journal
    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            entry.Display();
        }
    }

    // Method to save the journal to a file
    public void SaveToFile(string fileName)
    {
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            foreach (var entry in entries)
            {
                // Save entry details to the file
                outputFile.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response},{string.Join(",", entry.Tags)}");
            }
        }
    }

    // Method to load the journal from a file
    public void LoadFromFile(string fileName)
    {
        entries.Clear(); // Clear existing entries before loading from the file

        try
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                string[] parts = line.Split(",");
                string prompt = parts[1];
                string response = parts[2];

                // Parse the date from the file and create a new entry
                if (DateTime.TryParse(parts[0], out DateTime date))
                {
                    Entry loadedEntry = new Entry(prompt, response)
                    {
                        Date = date
                    };

                    // Load tags if available
                    if (parts.Length > 3)
                    {
                        loadedEntry.Tags.AddRange(parts[3].Split(','));
                    }

                    entries.Add(loadedEntry);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Creating a new journal.");
        }
    }

    // Method to edit an existing entry
    public void EditEntry(int entryIndex, string newResponse)
    {
        if (entryIndex >= 0 && entryIndex < entries.Count)
        {
            entries[entryIndex].Response = newResponse;
        }
    }

    // Method to delete an existing entry
    public void DeleteEntry(int entryIndex)
    {
        if (entryIndex >= 0 && entryIndex < entries.Count)
        {
            entries.RemoveAt(entryIndex);
        }
    }
}

// Program class containing the main entry point
class Program
{
    static void Main()
    {
        Journal myJournal = new Journal();

        Console.WriteLine("Welcome to the Journal App!");

        while (true)
        {
            // Display menu options including entry editing and deletion
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Edit an existing entry");
            Console.WriteLine("4. Delete an existing entry");
            Console.WriteLine("5. Save the journal to a file");
            Console.WriteLine("6. Load the journal from a file");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice (1-7): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter your response to the prompt: ");
                    string response = Console.ReadLine();
                    myJournal.AddEntry("Sample Prompt", response);
                    break;

                case "2":
                    myJournal.DisplayJournal();
                    break;

                case "3":
                    Console.Write("Enter the index of the entry to edit: ");
                    if (int.TryParse(Console.ReadLine(), out int editIndex))
                    {
                        Console.Write("Enter the new response: ");
                        string newResponse = Console.ReadLine();
                        myJournal.EditEntry(editIndex, newResponse);
                    }
                    break;

                case "4":
                    Console.Write("Enter the index of the entry to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteIndex))
                    {
                        myJournal.DeleteEntry(deleteIndex);
                    }
                    break;

                case "5":
                    Console.Write("Enter the file name to save: ");
                    string saveFileName = Console.ReadLine();
                    myJournal.SaveToFile(saveFileName);
                    break;

                case "6":
                    Console.Write("Enter the file name to load: ");
                    string loadFileName = Console.ReadLine();
                    myJournal.LoadFromFile(loadFileName);
                    break;

                case "7":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                    break;
            }
        }
    }
}
