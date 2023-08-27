namespace Common.BasicHelper.Math;

public enum CalculationType
{
    Unknown = 0,
    Add = 1,
    Substraction = 2,
    Multiply = 3,
    Division = 4,
    Power = 5,
}

public class Expression
{
    private CalculationType type = CalculationType.Unknown;

    public CalculationType Type { get => type; set => type = value; }

    public Expression? Left { get; set; }

    public Expression? Right { get; set; }

    private double? givenValue = null;

    public double Result
    {
        get => givenValue ?? Calculator.Calculate(Left, Right, type);
        set
        {
            givenValue = value;
        }
    }

    public static Expression FromValue(double value)
    {
        return new()
        {
            Result = value
        };
    }
}


