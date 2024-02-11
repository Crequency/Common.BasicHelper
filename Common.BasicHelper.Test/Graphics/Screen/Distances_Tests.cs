namespace Common.BasicHelper.Graphics.Screen;

[TestClass()]
public class Distances_Tests
{
    [TestMethod()]
    public void Test_Parse()
    {
        var strings = new List<string>()
        {
            "h(155.0)",
            "h(12,-24)",
            "v(27.1)",
            "v(-3.98,-2.31)",
            "d(44)",
            "d(25,25)",
            "24,25,-18,-29.4",
            "-2343.32,2343.66,23423.000,0234.00,177,242"
        };

        var anwsers = new List<Distances>()
        {
            new(horizontal: 155.0),
            new(left: 12, right: -24),
            new(vertical: 27.1),
            new(top: -3.98, bottom: -2.31),
            new(deep: 44),
            new(over: 25, under: 25),
            new(24, 25, -18, -29.4),
            new(-2343.32, 2343.66, 23423.000, 0234.00, 177, 242)
        };

        foreach (var i in Enumerable.Range(0, 8))
            Assert.AreEqual(Distances.Parse(strings[i]), anwsers[i]);
    }
}

