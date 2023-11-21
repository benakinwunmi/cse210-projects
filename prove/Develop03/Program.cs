using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Example usage
        var scripture = new Scripture("John 3:16", "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have eternal life.");

        var memorizer = new ScriptureMemorizer(scripture);

        memorizer.StartMemorizing();
    }
}

class Scripture
{
    private string reference;
    private string text;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
    }

    public string Reference => reference;
    public string Text => text;

    public List<string> GetWords()
    {
        return text.Split(' ').ToList();
    }
}

class Reference
{
    private string reference;

    public Reference(string reference)
    {
        this.reference = reference;
    }

    public string GetReference()
    {
        return reference;
    }
}

class Word
{
    private string word;
    private bool hidden;
    private int difficultyLevel; // New: Difficulty level of the word

    public Word(string word, int difficultyLevel)
    {
        this.word = word;
        this.hidden = false;
        this.difficultyLevel = difficultyLevel;
    }

    public string GetWord()
    {
        return hidden ? "_ _ _" : word;
    }

    public void Hide()
    {
        hidden = true;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public int GetDifficultyLevel()
    {
        return difficultyLevel;
    }
}

class ScriptureMemorizer
{
    private Scripture scripture;
    private List<Word> words;
    private int score; // New: User's score

    public ScriptureMemorizer(Scripture scripture)
    {
        this.scripture = scripture;
        InitializeWords();
        score = 0;
    }

    private void InitializeWords()
    {
        // New: Assign difficulty levels based on word length
        words = scripture.GetWords().Select(word => new Word(word, word.Length)).ToList();
    }

    public void StartMemorizing()
    {
        while (!AllWordsHidden())
        {
            DisplayScripture();
            Console.WriteLine($"Score: {score}");
            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            HideRandomWord();
            Console.Clear();
        }

        Console.WriteLine($"Congratulations! You finished with a score of {score}");
    }

    private void DisplayScripture()
    {
        Console.WriteLine($"{scripture.Reference}:");
        foreach (var word in words)
        {
            Console.Write($"{word.GetWord()} ");
        }
        Console.WriteLine();
    }

    private bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden());
    }

    private void HideRandomWord()
    {
        var visibleWords = words.Where(word => !word.IsHidden()).ToList();
        if (visibleWords.Count > 0)
        {
            var randomWord = visibleWords[new Random().Next(visibleWords.Count)];
            randomWord.Hide();
            score += randomWord.GetDifficultyLevel(); // New: Increase score based on word difficulty
        }
    }
}
