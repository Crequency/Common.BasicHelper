using System;
using System.Collections.Generic;
using Common.BasicHelper.Math;

namespace Common.BasicHelper.Math;

public class ExpressionParser
{
    private string? _source;
    private int _current;
    private double _numberValue;
    private Token _currentToken;

    private enum Token
    {
        Number,
        Add,
        Subtract,
        Multiply,
        Divide,
        Power,
        LeftParen,
        RightParen,
        End,
    }

    public ExpressionParser() { }

    public Expression Parse(string expression)
    {
        _source = expression;
        _current = 0;
        _numberValue = 0;

        GetNextToken();
        return ParseExpression();
    }

    private void GetNextToken()
    {
        if (_source is null)
            throw new Exception("Source expression is null.");

        while (_current < _source.Length && char.IsWhiteSpace(_source[_current]))
        {
            _current++;
        }

        if (_current >= _source.Length)
        {
            _currentToken = Token.End;
            return;
        }

        char currentChar = _source[_current];

        switch (currentChar)
        {
            case '+':
                _currentToken = Token.Add;
                _current++;
                return;
            case '-':
                _currentToken = Token.Subtract;
                _current++;
                return;
            case '*':
                if (_current + 1 < _source.Length && _source[_current + 1] == '*')
                {
                    _currentToken = Token.Power;
                    _current += 2;
                }
                else
                {
                    _currentToken = Token.Multiply;
                    _current++;
                }
                return;
            case '/':
                _currentToken = Token.Divide;
                _current++;
                return;
            case '(':
                _currentToken = Token.LeftParen;
                _current++;
                return;
            case ')':
                _currentToken = Token.RightParen;
                _current++;
                return;
        }

        if (char.IsDigit(currentChar) || currentChar == '.')
        {
            ParseNumber();
            return;
        }

        throw new Exception($"Unexpected character: {currentChar}");
    }

    private void ParseNumber()
    {
        if (_source is null)
            throw new Exception("Source expression is null.");

        int start = _current;

        // Reading integer part
        while (_current < _source.Length && char.IsDigit(_source[_current]))
        {
            _current++;
        }

        // Reading fractional part
        if (_current < _source.Length && _source[_current] == '.')
        {
            _current++;
            while (_current < _source.Length && char.IsDigit(_source[_current]))
            {
                _current++;
            }
        }

        string numberStr = _source[start.._current];

        if (!double.TryParse(numberStr, out _numberValue))
        {
            throw new Exception($"Invalid number format: {numberStr}");
        }

        _currentToken = Token.Number;
    }

    // <Expression> ::= <AddSubtractExpression>
    private Expression ParseExpression()
    {
        return ParseAddSubtractExpression();
    }

    // <AddSubtractExpression> ::= <MultiplyDivideExpression> {('+' | '-') <MultiplyDivideExpression>}
    private Expression ParseAddSubtractExpression()
    {
        Expression left = ParseMultiplyDivideExpression();

        while (_currentToken == Token.Add || _currentToken == Token.Subtract)
        {
            Token op = _currentToken;
            GetNextToken();

            Expression right = ParseMultiplyDivideExpression();

            left = new Expression
            {
                Type = op == Token.Add ? CalculationType.Add : CalculationType.Subtraction,
                Left = left,
                Right = right,
            };
        }

        return left;
    }

    // <MultiplyDivideExpression> ::= <PowerExpression> {('*' | '/') <PowerExpression>}
    private Expression ParseMultiplyDivideExpression()
    {
        Expression left = ParsePowerExpression();

        while (_currentToken == Token.Multiply || _currentToken == Token.Divide)
        {
            Token op = _currentToken;
            GetNextToken();

            Expression right = ParsePowerExpression();

            left = new Expression
            {
                Type = op == Token.Multiply ? CalculationType.Multiply : CalculationType.Division,
                Left = left,
                Right = right,
            };
        }

        return left;
    }

    // <PowerExpression> ::= <PrimaryExpression> {'**' <PrimaryExpression>}
    private Expression ParsePowerExpression()
    {
        Expression left = ParsePrimaryExpression();

        while (_currentToken == Token.Power)
        {
            GetNextToken();

            Expression right = ParsePrimaryExpression();

            left = new Expression
            {
                Type = CalculationType.Power,
                Left = left,
                Right = right,
            };
        }

        return left;
    }

    // <PrimaryExpression> ::= Number | '-' Number | '(' <Expression> ')'
    private Expression ParsePrimaryExpression()
    {
        switch (_currentToken)
        {
            case Token.Number:
                Expression numberExpr = Expression.FromValue(_numberValue);
                GetNextToken();
                return numberExpr;

            case Token.Subtract:
                GetNextToken();
                if (_currentToken == Token.Number)
                {
                    Expression negativeExpr = Expression.FromValue(-_numberValue);
                    GetNextToken();
                    return negativeExpr;
                }
                else
                {
                    throw new Exception("负号后必须是数字, ToDo: Support -()");
                }

            case Token.LeftParen:
                GetNextToken();
                Expression expr = ParseExpression();

                if (_currentToken != Token.RightParen)
                {
                    throw new Exception("Missing right brace ')'");
                }
                GetNextToken();
                return expr;

            default:
                throw new Exception($"Unexpected token: {_currentToken}");
        }
    }
}
