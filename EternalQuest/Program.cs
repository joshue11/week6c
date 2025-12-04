using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        string saveFile = "goals_save.txt";

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("=== Eternal Quest ===");
            Console.WriteLine($"Score: {manager.GetScore()}");
            Console.WriteLine("1) Create a goal");
            Console.WriteLine("2) List goals");
            Console.WriteLine("3) Record an event (complete a goal)");
            Console.WriteLine("4) Save progress");
            Console.WriteLine("5) Load progress");
            Console.WriteLine("6) Exit");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoalMenu(manager);
                    break;
                case "2":
                    Console.WriteLine();
                    manager.DisplayGoals();
                    break;
                case "3":
                    RecordEventMenu(manager);
                    break;
                case "4":
                    manager.SaveToFile(saveFile);
                    Console.WriteLine($"Saved to '{saveFile}'.");
                    break;
                case "5":
                    manager.LoadFromFile(saveFile);
                    Console.WriteLine($"Loaded from '{saveFile}'.");
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void CreateGoalMenu(GoalManager manager)
    {
        Console.WriteLine();
        Console.WriteLine("Choose goal type:");
        Console.WriteLine("1) Simple Goal (complete once)");
        Console.WriteLine("2) Eternal Goal (repeatable forever)");
        Console.WriteLine("3) Checklist Goal (complete N times + bonus)");
        Console.Write("Choice: ");
        string? ch = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string? name = Console.ReadLine() ?? "";
        Console.Write("Enter description: ");
        string? desc = Console.ReadLine() ?? "";

        Console.Write("Points awarded per record: ");
        int points = ReadIntFromConsole();

        if (ch == "1")
        {
            var g = new SimpleGoal(name, desc, points);
            manager.AddGoal(g);
            Console.WriteLine("Simple goal added.");
        }
        else if (ch == "2")
        {
            var g = new EternalGoal(name, desc, points);
            manager.AddGoal(g);
            Console.WriteLine("Eternal goal added.");
        }
        else if (ch == "3")
        {
            Console.Write("How many times required to complete?: ");
            int required = ReadIntFromConsole();
            Console.Write("Bonus points upon completion: ");
            int bonus = ReadIntFromConsole();
            var g = new ChecklistGoal(name, desc, points, required, bonus);
            manager.AddGoal(g);
            Console.WriteLine("Checklist goal added.");
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }

    static void RecordEventMenu(GoalManager manager)
    {
        Console.WriteLine();
        manager.DisplayGoals();
        Console.Write("Enter goal number to record event for: ");
        int idx = ReadIntFromConsole() - 1;
        manager.RecordEventForGoal(idx);
    }

    static int ReadIntFromConsole()
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (int.TryParse(s, out int v))
                return v;
            Console.Write("Please enter a valid integer: ");
        }
    }
}
