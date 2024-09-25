
namespace PathFindingAlgo;

public class AStarMap
{
    private AStarNode[,] m_Map;

    public int Width;
    public int Height;

    public AStarMap(MapData2D mapData2D)
    {
        m_Map = new AStarNode[mapData2D.Height, mapData2D.Width];
        Width = mapData2D.Width;
        Height = mapData2D.Height;

        for (int h = 0; h < mapData2D.Height; h++)
        {
            for (int w = 0; w < mapData2D.Width; w++)
            {
                var pos = new MapNode(w, h);
                m_Map[h, w] = new AStarNode(pos: pos, value: mapData2D.Get(pos));
            }
        }
    }

    public AStarNode? GetNode(MapNode pos)
    {
        if (!IsLegal(pos)) return null;

        return m_Map[pos.Y, pos.X];
    }


    public bool IsObstacle(MapNode pos)
    {
        if (!IsLegal(pos)) return false;

        var node = GetNode(pos);
        if (node == null) return false;

        return node.Value == -1;
    }

    public bool IsLegal(MapNode pos)
    {
        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height) return false;

        return true;
    }
}