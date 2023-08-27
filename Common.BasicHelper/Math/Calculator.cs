using System;

namespace Common.BasicHelper.Math;

public static class Calculator
{
    public static double Calculate(Expression? a, Expression? b, CalculationType type)
    {
        a = a ?? throw new ArgumentNullException(nameof(a));
        b = b ?? throw new ArgumentNullException(nameof(b));

        switch (type)
        {
            case CalculationType.Unknown:
                throw new NotImplementedException(
                    $"Unknown type can't calculate for `{a?.Result} {type} {b?.Result}`."
                );

            case CalculationType.Add:
                return a!.Result + b!.Result;

            case CalculationType.Substraction:
                return a!.Result - b!.Result;

            case CalculationType.Multiply:
                return a!.Result * b!.Result;

            case CalculationType.Division:
                return a!.Result / b!.Result; // Here couldn't throw divide by zero exception.

            case CalculationType.Power:
                return System.Math.Pow(a!.Result, b!.Result);

            default:
                throw new NotImplementedException(
                    $"Please provide argument {nameof(type)} ({nameof(CalculationType)})"
                );
        }
    }
}