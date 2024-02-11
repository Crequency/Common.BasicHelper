namespace Common.BasicHelper.Graphics.Screen;

[TestClass()]
public class Location_Tests
{
    [TestMethod()]
    public void Test_Parse()
    {
        var strings = new List<string>()
        {
            "155,33",
            "-232,355.22",
            "244,328,324",
            "092.23,00.00,234",
            "234,2.1,-234.34"
        };

        var anwsers = new List<Location>()
        {
            new(155, 33),
            new(-232, 355.22),
            new(244, 328, 324),
            new(092.23, 00.00, 234),
            new(234, 2.1, -234.34)
        };

        foreach (var i in Enumerable.Range(0, 5))
            Assert.AreEqual(Location.Parse(strings[i]), anwsers[i]);
    }
}
