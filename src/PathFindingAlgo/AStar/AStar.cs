namespace PathFindingAlgo;

public class AStar
{
    private List<AStarNode> m_OpenList = [];
    private List<AStarNode> m_CloseList = [];

    public List<MapNode>? FindPath(MapData2D map, MapNode startPos, MapNode endPos)
    {
        var astarMap = new AStarMap(map);
        var start = astarMap.GetNode(startPos);
        if (start == null) return null;

        m_CloseList.Add(start);

        do
        {
            SearchAroundPosList(start, endPos, astarMap);
            m_OpenList.Sort(SortOpenList);

            m_CloseList.Add(m_OpenList[0]);
            start = m_OpenList[0];
            m_OpenList.RemoveAt(0);

            if (start.Pos.X == endPos.X && start.Pos.Y == endPos.Y)
            {
                var pathList = new List<MapNode>();

                while (start.Prev != null)
                {
                    pathList.Add(start.Prev.Pos);
                    start = start.Prev;
                }
                pathList.Reverse();

                return pathList;
            }
        } while (m_OpenList.Count != 0);

        return null;
    }

    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.F > b.F)
            return 1;
        else if (a.F == b.F)
            return 1;
        else
            return -1;
    }

    private void SearchAroundPosList(AStarNode startNode, MapNode endNode, AStarMap map)
    {
        // 是否是边界
        if (!map.IsLegal(startNode.Pos)) return;
        // 是否是阻挡
        if (map.IsObstacle(startNode.Pos)) return;

        // 左上
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X - 1, startNode.Pos.Y - 1)), startNode, endNode, map, 1.4f);
        // 左下
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X - 1, startNode.Pos.Y + 1)), startNode, endNode, map, 1.4f);
        // 右上
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X + 1, startNode.Pos.Y - 1)), startNode, endNode, map, 1.4f);
        // 右下
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X + 1, startNode.Pos.Y + 1)), startNode, endNode, map, 1.4f);

        // 正上
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X, startNode.Pos.Y - 1)), startNode, endNode, map, 1f);
        // 正下
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X, startNode.Pos.Y + 1)), startNode, endNode, map, 1f);
        // 正左
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X - 1, startNode.Pos.Y)), startNode, endNode, map, 1f);
        // 正右
        CheckPos(map.GetNode(MapNode.Create(startNode.Pos.X + 1, startNode.Pos.Y)), startNode, endNode, map, 1f);
    }

    private void CheckPos(AStarNode? pos, AStarNode prev, MapNode endPos, AStarMap map, float g)
    {
        // 若检测的点值为空，或者非法，或者为阻挡物，直接忽略返回
        if (pos == null || !map.IsLegal(pos.Pos) || map.IsObstacle(pos.Pos)) return;

        // 若点已经包含，直接忽略返回
        if (m_OpenList.Contains(pos) || m_CloseList.Contains(pos)) return;

        var nearlyPos = new AStarNode
        {
            Pos = pos.Pos,
            Prev = prev,
            G = prev.G + g,
            H = MathF.Abs(endPos.X - pos.Pos.X) + MathF.Abs(endPos.Y - pos.Pos.Y)
        };

        m_OpenList.Add(nearlyPos);
    }
}