using System.Collections.Generic;

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

    public Expression? Parent { get; set; }

    private Expression? left;

    public Expression? Left
    {
        get => left;
        set
        {
            left = value;
            if (left is not null)
                left!.Parent = this;
        }
    }

    private Expression? right;

    public Expression? Right
    {
        get => right;
        set
        {
            right = value;
            if (right is not null)
                right!.Parent = this;
        }
    }

    private double? givenValue = null;

    public double Result
    {
        get => givenValue ?? Calculator.Calculate(Left, Right, type);
        set
        {
            givenValue = value;
        }
    }

    public static Dictionary<CalculationType, string> CalculationTypeStrings = new()
    {
        { CalculationType.Unknown, "?" },
        { CalculationType.Add, "+" },
        { CalculationType.Substraction, "-" },
        { CalculationType.Multiply, "*" },
        { CalculationType.Division, "/" },
        { CalculationType.Power, "**" },
    };

    public static List<CalculationType> CalculationTypeOrder = new()
    {
        CalculationType.Power,
        CalculationType.Multiply,
        CalculationType.Division,
        CalculationType.Add,
        CalculationType.Substraction,
        CalculationType.Unknown,
    };

    public static Expression FromValue(double value)
    {
        return new()
        {
            Result = value
        };
    }

    public override string ToString()
    {
        if (type == CalculationType.Unknown && givenValue is not null)
        {
            return givenValue.ToString();
        }

        var shouldAddBrackets = true
            && Parent is not null
            && (CalculationTypeOrder.IndexOf(type) < CalculationTypeOrder.IndexOf(Parent.type));

        var baseStr = $"{Left?.ToString()} {CalculationTypeStrings[type]} {Right?.ToString()}";

        return $"{(shouldAddBrackets ? "(" : "")}{baseStr}{(shouldAddBrackets ? ")" : "")}";
    }
}


