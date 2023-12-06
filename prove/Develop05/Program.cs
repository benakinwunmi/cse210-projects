using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    protected List<DateTime> completionHistory = new List<DateTime>();

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsCompleted = false;
    }

    public abstract void RecordEvent();

    public virtual string GetDetailsString()
    {
        return $"Goal: {Name}, Points: {Points}, Completed: {(IsCompleted ? "Yes" : "No")}";
    }

    protected void RecordCompletionDate()
    {
        completionHistory.Add(DateTime.Now);
    }

    public void DisplayCompletionHistory()
    {
        Console.WriteLine($"Completion History for {Name}:");
        foreach (var date in completionHistory)
        {
            Console.WriteLine(date.ToString("MM/dd/yyyy HH:mm:ss"));
        }
    }

    public int GetTotalCompletions()
    {
        return completionHistory.Count;
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        IsCompleted = true;
        RecordCompletionDate();
        Console.WriteLine($"Event recorded for {Name}. You earned {Points} points!");
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        RecordCompletionDate();
        Console.WriteLine($"Event recorded for {Name}. You earned {Points} points!");
    }
}

public class ChecklistGoal : Goal
{
    private int timesCompleted;
    private int requiredTimes;

    public ChecklistGoal(string name, int points, int requiredTimes) : base(name, points)
    {
        this.requiredTimes = requiredTimes;
    }

    public override void RecordEvent()
    {
        timesCompleted++;
        RecordCompletionDate();
        Console.WriteLine($"Event recorded for {Name}. You earned {Points} points!");

        if (timesCompleted >= requiredTimes)
        {
            IsCompleted = true;
            Console.WriteLine($"Congratulations! Goal {Name} completed!");
        }
    }

    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $", Completed {timesCompleted}/{requiredTimes} times";
    }
}

public class GoalManager
{
    private List<Goal> goals;

    public GoalManager()
    {
        goals = new List<Goal>();
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void DisplayUserStatistics()
    {
        int totalPoints = 0;
        int totalCompletions = 0;

        foreach (var goal in goals)
        {
            totalPoints += goal.Points;
            totalCompletions += goal.GetTotalCompletions();
        }

        Console.WriteLine($"Total Points: {totalPoints}");
        Console.WriteLine($"Total Completions: {totalCompletions}");
    }

    public void DisplayCompletionHistoryForAllGoals()
    {
        foreach (var goal in goals)
        {
            goal.DisplayCompletionHistory();
        }
    }

    public List<Goal> GetGoals()
    {
        return goals;
    }

    public void SaveGoalsToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var goal in goals)
            {
                outputFile.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Points},{goal.IsCompleted}");
            }
        }
    }

    public void LoadGoalsFromFile(string filename)
    {
        goals.Clear();

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);

            foreach (var line in lines)
            {
                string[] parts = line.Split(",");
                string typeName = parts[0];
                string name = parts[1];
                int points = int.Parse(parts[2]);
                bool isCompleted = bool.Parse(parts[3]);

                Goal goal;

                switch (typeName)
                {
                    case nameof(SimpleGoal):
                        goal = new SimpleGoal(name, points);
                        break;
                    case nameof(EternalGoal):
                        goal = new EternalGoal(name, points);
                        break;
                    case nameof(ChecklistGoal):
                        int requiredTimes = int.Parse(parts[4]);
                        goal = new ChecklistGoal(name, points, requiredTimes);
                        break;
                    default:
                        throw new ArgumentException($"Unknown goal type: {typeName}");
                }

                goal.IsCompleted = isCompleted;
                goals.Add(goal);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        GoalManager goalManager = new GoalManager();

        Goal simpleGoal = new SimpleGoal("Run a Marathon", 1000);
        Goal eternalGoal = new EternalGoal("Read Scriptures", 100);
        Goal checklistGoal = new ChecklistGoal("Attend Temple", 50, 10);

        goalManager.AddGoal(simpleGoal);
        goalManager.AddGoal(eternalGoal);
        goalManager.AddGoal(checklistGoal);

        goalManager.DisplayGoals();

        simpleGoal.RecordEvent();
        eternalGoal.RecordEvent();
        checklistGoal.RecordEvent();
        checklistGoal.RecordEvent(); 

        goalManager.DisplayGoals();

        goalManager.DisplayCompletionHistoryForAllGoals();
        goalManager.DisplayUserStatistics();

        goalManager.SaveGoalsToFile("goals.txt");
        goalManager.LoadGoalsFromFile("goals.txt");
        goalManager.DisplayGoals();
    }
}
