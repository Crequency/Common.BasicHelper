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
        [LbptFormat(Ignore=true)]
        public string? Tip => "This tip will be ignored.";

        [LbptComment("人数\nIndex from 0 - 1")]
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
