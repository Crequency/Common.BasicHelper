using Common.BasicHelper.UI.Screen;

namespace Common.BasicHelper.Test;

[TestClass]
public class UI_Test
{
    [TestMethod]
    public void ResolutionSuggestTest()
    {
        foreach (var item in Resolution.resolutions)
        {
            Resolution tarRes = Resolution.Suggest(Resolution.Parse("2560x1440"),
                Resolution.Parse("1280x720"), item).Integerization();
            Console.WriteLine($"{item}\r\n\t{tarRes}");
        }
    }
}