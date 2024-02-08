namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

[TestClass]
public class LbptSerializer_Tests
{
    public class TestClasses
    {
        public class Person
        {
            public int Id { get; set; }

            public string? Name { get; set; }
        }

        public class PersonGroup
        {
            public int? PersonCount => Persons?.Count;

            public List<Person>? Persons { get; set; } = new();
        }

        internal static PersonGroup GetOneGroup()
        {
            var group = new PersonGroup();
            group.Persons?.Add(new()
            {
                Id = 0,
                Name = "张三"
            });
            group.Persons?.Add(new()
            {
                Id = 1,
                Name = "李四"
            });
            return group;
        }
    }

    [TestMethod]
    public void Test_Serialize()
    {
        var group = TestClasses.GetOneGroup();

        var result = LbptSerializer.Serialize(group, out var text);

        Console.WriteLine(text);

        Assert.AreEqual(result.SerializedText, text);
    }
}
