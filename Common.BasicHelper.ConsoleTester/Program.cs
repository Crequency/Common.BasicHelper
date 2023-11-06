using Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

var group = TestClasses.GetOneGroup();

var result = LbptSerializer.Serialize(group, out var text);

Console.WriteLine(text);

