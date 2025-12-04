using System;

public abstract class Goal
{
    private string _name;
    private string _description;
    protected int _pointsPerRecord;

    protected Goal(string name, string description, int pointsPerRecord)
    {
        _name = name;
        _description = description;
        _pointsPerRecord = pointsPerRecord;
    }

    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetPointsPerRecord() => _pointsPerRecord;

    public abstract int RecordEvent();

    public abstract bool IsComplete();

    public abstract string GetStatus();

    public abstract string ToDataString();

}
