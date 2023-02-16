﻿using Common.BasicHelper.Util.Extension;

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

    [TestMethod]
    public void DumpQueueTest()
    {
        var queue = new Queue<int>()
            .Push(1)
            .Push(2)
            .Pop()
            .Push(3)
            .Push(4)
            .Pop()
            .Push(5)
            ;
        Assert.AreEqual("3 4 5 ", queue.Dump());
    }
}
