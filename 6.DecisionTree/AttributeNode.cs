
public class AttributeNode
{
    public ValueNode Parent { get; set; }
    public string AttributeName { get; set; }
    public List<ValueNode> Children { get; set; }
}