namespace Common.BasicHelper.Graphics.Screen;

[TestClass]
public class Resolution_Tests
{
    [TestMethod()]
    public void Test_Suggest()
    {
        foreach (var item in Resolution.resolutions)
        {
            var tarRes = Resolution.Suggest(
                Resolution.Parse("2560x1440"),
                Resolution.Parse("1280x720"),
                item
            ).Integerization();

            Console.WriteLine(
                "" +
                $"{item} ({item.Description})\r\n" +
                $"\t{tarRes}"
            );
        }
    }
}
