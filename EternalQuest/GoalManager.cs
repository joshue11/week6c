using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    public GoalManager()
    {
        _score = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public IReadOnlyList<Goal> GetGoals() => _goals.AsReadOnly();

    public int GetScore() => _score;

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");
        }
    }

    public void RecordEventForGoal(int index)
    {
        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid goal index.");
            return;
        }

        Goal g = _goals[index];
        int points = g.RecordEvent();
        _score += points;

        Console.WriteLine($"Recorded event for '{g.GetName()}'. Points awarded: {points}. Total score: {_score}.");
    }

    public void SaveToFile(string path)
    {
        using StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(_score);
        foreach (var g in _goals)
        {
            sw.WriteLine(g.ToDataString());
        }
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Save file not found.");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        _goals.Clear();
        _score = 0;

        if (lines.Length == 0)
            return;

        if (int.TryParse(lines[0], out int loadedScore))
            _score = loadedScore;

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split('|');
            string type = parts[0];

            try
            {
                if (type == "Simple" && parts.Length >= 5)
                {
                    string name = parts[1].Replace("¦", "|");
                    string desc = parts[2].Replace("¦", "|");
                    int points = int.Parse(parts[3]);
                    bool isComplete = bool.Parse(parts[4]);
                    var sg = new SimpleGoal(name, desc, points);
                    if (isComplete)
                    {
                  
                        var field = typeof(SimpleGoal).GetField("_isComplete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        field?.SetValue(sg, true);
                    }
                    _goals.Add(sg);
                }
                else if (type == "Eternal" && parts.Length >= 4)
                {
                    string name = parts[1].Replace("¦", "|");
                    string desc = parts[2].Replace("¦", "|");
                    int points = int.Parse(parts[3]);
                    var eg = new EternalGoal(name, desc, points);
                    _goals.Add(eg);
                }
                else if (type == "Checklist" && parts.Length >= 7)
                {
                    string name = parts[1].Replace("¦", "|");
                    string desc = parts[2].Replace("¦", "|");
                    int points = int.Parse(parts[3]);
                    int current = int.Parse(parts[4]);
                    int required = int.Parse(parts[5]);
                    int bonus = int.Parse(parts[6]);
                    var cg = new ChecklistGoal(name, desc, points, required, bonus);
                    var field = typeof(ChecklistGoal).GetField("_currentCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    field?.SetValue(cg, current);
                    _goals.Add(cg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading line {i + 1}: {ex.Message}");
            }
        }
    }
}
