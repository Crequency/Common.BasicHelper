using Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

// Test serialization

var group = TestClasses.GetOneGroup();

var result = LbptSerializer.Serialize(group, out var text);

Console.WriteLine(text);

// Test deserialization

group.TestPropertyForDeserialization = "Deserialization test succeeded!";

var obj = LbptSerializer.Deserialize<TestClasses.PersonGroup>(
    LbptSerializer.Serialize(group, out _).SerializedText!
);

// Breakpoint for debug
;
