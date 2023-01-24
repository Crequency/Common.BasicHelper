using Common.BasicHelper.Util.Extension;

namespace Common.BasicHelper.Test;

[TestClass]
public class Util_Extension_Test
{

    [TestMethod]
    public void QueueExtensionTest()
    {
        var queue = new Queue<int>()
            .Push(1)
            .Push(2)
            .Push(3)
            .Push(4)
            .Push(5)
            .Push(6)
            .Pop()
            ;
        while (queue.IsNotEmpty())
        {
            queue = queue.ForEach(x => ++x);
            queue = queue.Pop();
        }
        Assert.AreEqual(0, queue.Count);
    }
}
