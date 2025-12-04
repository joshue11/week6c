using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _pointsPerRecord;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStatus()
    {
        return $"[ {(_isComplete ? "X" : " ")} ] {GetName()} ({GetDescription()}) - {(_isComplete ? "Completed" : "Not completed")}";
    }

    public override string ToDataString()
    {
        return $"Simple|{Escape(GetName())}|{Escape(GetDescription())}|{_pointsPerRecord}|{_isComplete}";
    }

    private string Escape(string s) => s.Replace("|", "¦");
}
