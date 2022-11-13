using BasicHelper.UI.Screen;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class BasicHelper_UI_Screen
    {
        [TestMethod]
        public void ResolutionSuggestTest()
        {
            foreach (var item in Resolution.resolutions)
            {
                Console.WriteLine(Resolution.Suggest(Resolution.Parse("2560x1440"),
                    Resolution.Parse("1280x720"), item).Integerization());
            }
        }
    }
}