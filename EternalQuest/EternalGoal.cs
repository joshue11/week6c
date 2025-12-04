using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _pointsPerRecord;
    }

    public override bool IsComplete() => false;

    public override string GetStatus()
    {
        return $"[ ∞ ] {GetName()} ({GetDescription()}) - Eternal (not completed)";
    }

    public override string ToDataString()
    {
        return $"Eternal|{Escape(GetName())}|{Escape(GetDescription())}|{_pointsPerRecord}";
    }

    private string Escape(string s) => s.Replace("|", "¦");
}
