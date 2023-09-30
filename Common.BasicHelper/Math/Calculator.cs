using System;

namespace Common.BasicHelper.Math;

public static class Calculator
{
    public static double Calculate(Expression? a, Expression? b, CalculationType type)
    {
        a = a ?? throw new ArgumentNullException(nameof(a));
        b = b ?? throw new ArgumentNullException(nameof(b));

        return type switch
        {
            CalculationType.Unknown => throw new NotImplementedException(
                $"Unknown type can't calculate for `{a?.Result} {type} {b?.Result}`."
            ),
            CalculationType.Add => a!.Result + b!.Result,
            CalculationType.Substraction => a!.Result - b!.Result,
            CalculationType.Multiply => a!.Result * b!.Result,
            CalculationType.Division => a!.Result / b!.Result,// Here couldn't throw divide by zero exception.
            CalculationType.Power => System.Math.Pow(a!.Result, b!.Result),
            _ => throw new NotImplementedException(
                $"Please provide argument {nameof(type)} ({nameof(CalculationType)})"
            ),
        };
    }
}
