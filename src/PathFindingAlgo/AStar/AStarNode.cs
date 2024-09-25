namespace PathFindingAlgo;

public class AStarNode
{
    public MapNode Pos;
    public float Value;
    public float G;
    public float H;
    public float F => G + H;
    public AStarNode? Prev;

    public AStarNode()
    {
        Value = 0;
        G = 0;
        H = 0;
    }

    public AStarNode(MapNode pos, float value)
    {
        Pos = pos;
        Value = value;
    }

    public AStarNode(MapNode pos, int value, int g, int h, AStarNode? prev)
    {
        Value = value;
        G = g;
        H = h;
        Prev = prev;
    }
}