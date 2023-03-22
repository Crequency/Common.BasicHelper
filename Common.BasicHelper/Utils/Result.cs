using System;

namespace Common.BasicHelper.Util;

public class Result<T> : Exception
{

    private bool hasProblem = false;

    /// <summary>
    /// 是否存在问题
    /// </summary>
    public bool HasProblem
    {
        get { return hasProblem; }
        set { hasProblem = value; }
    }

    private object returnResult = string.Empty;

    /// <summary>
    /// 返回结果
    /// </summary>
    public object ReturnResult
    {
        get { return returnResult; }
        set
        {
            returnResult = value;
            returnType = value.GetType();
        }
    }

    private Type returnType = typeof(string);

    /// <summary>
    /// 返回值类型
    /// </summary>
    public Type ReturnType
    {
        get { return returnType; }
    }

    /// <summary>
    /// 异常状态下的构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="hasProblem">是否存在问题, 默认存在</param>
    public Result(string message, bool hasProblem = true) : base(message) => HasProblem = hasProblem;

    /// <summary>
    /// 正常状态下的构造函数
    /// </summary>
    /// <param name="result">返回结果</param>
    public Result(T result)
    {
        if (result != null)
        {
            ReturnResult = result;
        }
    }
}
