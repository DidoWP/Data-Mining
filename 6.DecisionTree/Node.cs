
public class Node
{
    public Node Parent { get; set; } = null;
    public string AttributeName { get; set; } = "";
    public List<Node> Children { get; set; } = new List<Node>();
    public bool IsAttribute { get; set; } = false;
    public bool IsLeaf { get; set; } = false;
    public string Result { get; set; } = "";

    public Node(Node parent, string attributeName, bool isAttribute)
    {
        Parent = parent;
        AttributeName = attributeName;
        IsAttribute = IsAttribute;
    }

}