using System;

public class ChecklistGoal : Goal
{
    private int _requiredCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int pointsPerRecord, int requiredCount, int bonusPoints)
        : base(name, description, pointsPerRecord)
    {
        _requiredCount = requiredCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
            return 0;

        _currentCount++;
        int awarded = _pointsPerRecord;

        if (_currentCount >= _requiredCount)
        {
            awarded += _bonusPoints;
        }

        return awarded;
    }

    public override bool IsComplete() => _currentCount >= _requiredCount;

    public override string GetStatus()
    {
        return $"[ {(_currentCount >= _requiredCount ? "X" : " ")} ] {GetName()} ({GetDescription()}) - Completed {_currentCount}/{_requiredCount}";
    }

    public override string ToDataString()
    {
        return $"Checklist|{Escape(GetName())}|{Escape(GetDescription())}|{_pointsPerRecord}|{_currentCount}|{_requiredCount}|{_bonusPoints}";
    }

    private string Escape(string s) => s.Replace("|", "¦");
}

