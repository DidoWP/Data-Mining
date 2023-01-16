
public class ValueNode
{
    public AttributeNode Parent { get; set; }
    public string ValueName { get; set; }
    public List<AttributeNode> Children { get; set; }
    public bool IsLeaf { get; set; } // entropy == 0
}