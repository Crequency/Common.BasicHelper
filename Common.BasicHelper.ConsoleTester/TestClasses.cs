using Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

public class TestClasses
{
    public class Person
    {
        [LbptComment("ID")]
        public int Id { get; set; }

        [LbptComment("姓名")]
        public string? Name { get; set; }
    }

    public class PersonGroup
    {
        [LbptFormat(Ignore = true)]
        public string? Tip => "This tip will be ignored.";

        [LbptFormat(SerializeAsFinalMultilineProperty = true)]
        [LbptComment("这是一个文末多行属性")]
        public string? End => "You reached the end.";

        [LbptComment("人数\n这是一个多行属性")]
        [LbptFormat(SerializeInMultiLineFormat = true)]
        public int? PersonCount => Persons?.Count;

        public List<Person>? Persons { get; set; } = new();

        public string? TestPropertyForDeserialization { get; set; }
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
        group.Persons?.Add(new()
        {
            Id = 2,
            Name = "王五"
        });
        return group;
    }
}
