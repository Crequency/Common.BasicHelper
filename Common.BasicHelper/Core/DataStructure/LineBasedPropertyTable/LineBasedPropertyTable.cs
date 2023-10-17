using System.Collections.Generic;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

public class LineBasedPropertyTable
{
    private LineBasedPropertyTableNode? rootNode = null;

    public LineBasedPropertyTableNode? RootNode { get => rootNode; set => rootNode = value; }
}

public class LineBasedPropertyTableNode
{
    private readonly List<LineBasedPropertyTableNode> subNodes = new();

    public List<LineBasedPropertyTableNode> SubNodes => subNodes;

    private LineBasedPropertyTableNode? parentNode = null;

    public LineBasedPropertyTableNode? ParentNode { get => parentNode; set => parentNode = value; }

    public string? PropertyName { get; set; }

    public string? PropertyPath { get; set; }

    public string? PropertyValue { get; set; }

    public bool IsEnumerable { get; set; } = false;

    public LineBasedPropertyTableNode SetParentNode(LineBasedPropertyTableNode node)
    {
        ParentNode = node;
        return this;
    }
}
