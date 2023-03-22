using Common.BasicHelper.Graphics.Screen;

namespace Common.BasicHelper.Test.Graphics.Screen;

[TestClass]
public class Test_Resolution
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
