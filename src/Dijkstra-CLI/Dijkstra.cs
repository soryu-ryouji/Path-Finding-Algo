namespace PathFindingAlgo;

public class Dijkstra
{
    private class DMapPos(MapPos mapPos, int g, int h, DMapPos? father)
    {
        public MapPos MapPos = mapPos;
        public float G = g;
        public float H = h;
        public float F => G + H;
        public DMapPos? Father = father;

        public static DMapPos Create(MapPos pos)
        {
            return new DMapPos(pos, 0, 0, null);
        }

        public static DMapPos Create(int x, int y)
        {
            return new DMapPos(new MapPos(x, y), 0, 0, null);
        }
    }

    private List<DMapPos> m_OpenList = [];
    private List<DMapPos> m_CloseList = [];

    public List<MapPos>? FindPath(MapData2D map, MapPos startPos, MapPos endPos)
    {
        // 检测开始坐标和结束坐标是否合法 (这里由于是测试，暂时不写那么详细)

        // 将起点放入开始
        var start = new DMapPos(startPos, 0, 0, null);

        m_CloseList.Add(start);

        while (true)
        {
            m_OpenList.AddRange(SearchAroundPosList(start, endPos, map));
            m_OpenList.Sort(SortOpenList);

            // 当 OpenList 为空时，说明是死路
            if (m_OpenList.Count == 0) break;

            m_CloseList.Add(m_OpenList[0]);
            start = m_OpenList[0];
            m_OpenList.RemoveAt(0);

            if (start.MapPos.X == endPos.X && start.MapPos.Y == endPos.Y)
            {
                // 路径找完了
                var pathList = new List<MapPos>();

                while (start.Father != null)
                {
                    pathList.Add(start.Father.MapPos);
                    start = start.Father;
                }
                pathList.Reverse();

                return pathList;
            }
        }

        return null;
    }

    private int SortOpenList(DMapPos a, DMapPos b)
    {
        if (a.F > b.F)
            return 1;
        else if (a.F == b.F)
            return 1;
        else
            return -1;
    }

    private List<DMapPos> SearchAroundPosList(DMapPos pos, MapPos endPos, MapData2D map)
    {
        // 是否是边界
        if (!map.IsLegal(pos.MapPos)) return [];
        // 是否是阻挡
        if (map.IsObstacle(pos.MapPos)) return [];
        // 是否在开启 / 关闭列表
        if (m_OpenList.Contains(pos) || m_CloseList.Contains(pos)) return [];

        var posList = new List<DMapPos>();

        void AddToPosList(DMapPos? pos)
        {
            if (pos == null) return;
            posList.Add(pos);
        }
        // 左上
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X - 1, pos.MapPos.Y - 1), pos, endPos, map, 1.4f));
        // 右上
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X + 1, pos.MapPos.Y - 1), pos, endPos, map, 1.4f));
        // 左下
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X - 1, pos.MapPos.Y + 1), pos, endPos, map, 1.4f));
        // 右下
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X + 1, pos.MapPos.Y + 1), pos, endPos, map, 1.4f));

        // 正上
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X, pos.MapPos.Y - 1), pos, endPos, map, 1f));
        // 正左
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X - 1, pos.MapPos.Y), pos, endPos, map, 1f));
        // 正右
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X + 1, pos.MapPos.Y), pos, endPos, map, 1f));
        // 正下
        AddToPosList(CheckAroundPos(DMapPos.Create(pos.MapPos.X, pos.MapPos.Y + 1), pos, endPos, map, 1f));

        return posList;
    }

    private DMapPos? CheckAroundPos(DMapPos pos, DMapPos father, MapPos endPos, MapData2D map, float g)
    {
        // 是否是边界
        if (!map.IsLegal(pos.MapPos)) return null;
        // 是否是阻挡
        if (map.IsObstacle(pos.MapPos)) return null;
        // 是否在开启 / 关闭列表
        if (m_OpenList.Contains(pos) || m_CloseList.Contains(pos)) return null;

        var nearlyPos = new DMapPos(pos.MapPos, 0, 0, father);
        nearlyPos.G = father.G + g;
        nearlyPos.H = MathF.Abs(endPos.X - nearlyPos.MapPos.X) + MathF.Abs(endPos.Y - nearlyPos.MapPos.Y);

        return nearlyPos;
    }
}