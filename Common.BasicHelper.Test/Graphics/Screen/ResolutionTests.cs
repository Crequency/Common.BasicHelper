namespace Common.BasicHelper.Graphics.Screen;

[TestClass]
public class ResolutionTests
{
    [TestMethod]
    public void Test_SuggestResolution()
    {
        foreach (var item in Resolution.resolutions)
        {
            Resolution tarRes = Resolution.Suggest(Resolution.Parse("2560x1440"),
                Resolution.Parse("1280x720"), item).Integerization();
            Console.WriteLine($"{item}\r\n\t{tarRes}");
        }
    }
}
