using System;
using System.Threading;

class MindfulnessActivity
{
    protected int duration;
    protected string[] motivationalMessages = {
        "You're doing great! Keep it up.",
        "Take a deep breath and embrace the present moment.",
        "Each breath brings you closer to calmness.",
    };

    public MindfulnessActivity(int duration)
    {
        this.duration = duration;
    }

    public virtual void Start()
    {
        Console.WriteLine($"Prepare for {GetType().Name.ToLower()}...");
        Thread.Sleep(2000); // Pause for 2 seconds
    }

    public virtual void End()
    {
        Console.WriteLine($"Great job! You completed {GetType().Name.ToLower()} for {duration} seconds.");
        Thread.Sleep(2000); // Pause for 2 seconds
    }

    protected void DisplayMotivationalMessage()
    {
        Random random = new Random();
        Console.WriteLine(motivationalMessages[random.Next(motivationalMessages.Length)]);
        Thread.Sleep(1000); // Pause for 1 second after displaying the message
    }

    protected void GatherUserFeedback()
    {
        Console.WriteLine("How do you feel after completing this activity? (Enter your feedback)");
        string userFeedback = Console.ReadLine();
        // Store user feedback or perform further actions with it
        Console.WriteLine("Thank you for your feedback!");
    }
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(1000); 
            Console.WriteLine("Breathe out...");
            Thread.Sleep(1000); 
        }

        base.End();
        GatherUserFeedback();
        DisplayMotivationalMessage();
    }
}

class ReflectionActivity : MindfulnessActivity
{
    public ReflectionActivity(int duration) : base(duration) { }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");

        string[] prompts = {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine(prompt);

        for (int i = 0; i < duration; i++)
        {
            string[] questions = {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
            };
            string question = questions[random.Next(questions.Length)];

            Console.WriteLine(question);
            Thread.Sleep(1000);
        }

        base.End();
        GatherUserFeedback();
        DisplayMotivationalMessage();
    }
}

class ListingActivity : MindfulnessActivity
{
    public ListingActivity(int duration) : base(duration) { }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

        string[] prompts = {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
        };
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine(prompt);

        Console.WriteLine($"You have {duration} seconds to start listing...");

        Console.WriteLine("Activity completed!");
        base.End();
        GatherUserFeedback();
        DisplayMotivationalMessage();
    }
}

class Program
{
    static void Main()
    {
        MindfulnessActivity activity = GetSelectedActivity();
        activity.Start();
    }

    static MindfulnessActivity GetSelectedActivity()
    {
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
        {
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
        }

        Console.WriteLine("Enter the duration (in seconds):");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.WriteLine("Invalid duration. Please enter a positive integer.");
        }

        switch (choice)
        {
            case 1:
                return new BreathingActivity(duration);
            case 2:
                return new ReflectionActivity(duration);
            case 3:
                return new ListingActivity(duration);
            default:
                return null;
        }
    }
}
