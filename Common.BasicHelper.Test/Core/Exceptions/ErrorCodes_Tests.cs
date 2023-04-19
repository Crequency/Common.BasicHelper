using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Core.Exceptions;

[TestClass()]
public class ErrorCodes_Tests
{
    [TestMethod()]
    public void Test_GetExceptionMessage()
    {
        foreach (var item in Enum.GetValues(typeof(ErrorCodes)))
        {
            var ec = (ErrorCodes)item;
            $"{ec}\t{ec.BuildMessage()}".Print();
        }
    }
}
