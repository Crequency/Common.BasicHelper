using System.Text;

namespace Common.BasicHelper.Utils.Extensions;

[TestClass]
public class QueueHelper_Tests
{
    [TestMethod]
    public void Test_QueueExtensions()
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
    public void Test_DumpQueue()
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

    [TestMethod()]
    public void Test_ForEach()
    {
        var sb = new StringBuilder();

        var queue = new Queue<int>()
            .Push(1)
            .Push(3)
            .Push(5)
            .ForEach(x => sb.AppendLine(x.ToString()), reappend: true)
            ;

        Assert.AreEqual
        (
            """
            1
            3
            5

            """,
            sb.ToString().Print()
        );
    }

    [TestMethod()]
    public async Task Test_ForEachAsync()
    {
        var sb = new StringBuilder();

        var queue = await new Queue<int>()
            .Push(1)
            .Push(3)
            .Push(5)
            .ForEachAsync(x => sb.AppendLine(x.ToString()), reappend: true)
            ;

        Assert.AreEqual
        (
            """
            1
            3
            5

            """,
            sb.ToString().Print()
        );
    }

    [TestMethod()]
    public void Test_IsEmpty()
    {
        Assert.AreEqual(true, new Queue<int>().IsEmpty());

        Assert.AreEqual
        (
            true,
            new Queue<int>()
                .Push(3)
                .Push(5)
                .Pop()
                .Pop()
                .IsEmpty()
        );
    }
}
