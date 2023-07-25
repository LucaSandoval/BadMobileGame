using System;

[Flags]
public enum EquationSymbolEnum
{
    None = 0,
    Circle = 1 << 0,
    Square = 1 << 1,
    Triangle = 1 << 2,
    Multiply = 1 << 3,
    Add = 1 << 4,
    Subtract = 1 << 5,
    Red = 1 << 6,
    Green = 1 << 7,
    Blue = 1 << 8,
    One = 1 << 9,
    Two = 1 << 10,
    Three = 1 << 11,
    Four = 1 << 12,
    Five = 1 << 13,
    Six = 1 << 14,
    Seven = 1 << 15,
    Eight = 1 << 16,
    Nine = 1 << 17
}

[System.Serializable]
public class RangeProbability
{
    public int MinInclusive;
    public int MaxInclusive;
    public int ProbabilityPct;

    public RangeProbability(int min, int max, int probabilityPct)
    {
        MinInclusive = min;
        MaxInclusive = max;
        ProbabilityPct = probabilityPct;
    }
}

