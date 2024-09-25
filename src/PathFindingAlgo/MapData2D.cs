namespace PathFindingAlgo;

public struct MapNode(int x, int y)
{
    public int X = x;
    public int Y = y;

    public static MapNode Create(int x, int y)
    {
        return new MapNode(x, y);
    }
}

public struct MapData2D
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private int[,] m_MapData;

    public MapData2D(int width, int height)
    {
        Width = width;
        Height = height;
        m_MapData = new int[height, width];
    }

    public static MapData2D RandomMap(int w, int h)
    {
        var mapData = new MapData2D(h, w);
        var random = new Random();
        for (int y = 0; y < mapData.Width; y++)
        {
            for (int x = 0; x < mapData.Height; x++)
            {
                var value = random.Next(0, 10) < 2 ? -1 : random.Next(0, 10);
                mapData.Set(MapNode.Create(x, y), value);
            }
        }

        return mapData;
    }

    public int Get(MapNode pos)
    {
        if (pos.X < 0 || pos.X >= Width ||
            pos.Y < 0 || pos.Y >= Height)
            throw new ArgumentOutOfRangeException("Map Pos Argument Out of Range");

        return m_MapData[pos.Y, pos.X];
    }

    public void Set(MapNode pos, int value)
    {
        if (pos.X < 0 || pos.X >= Width ||
            pos.Y < 0 || pos.Y >= Height)
            throw new ArgumentOutOfRangeException("Map Pos Argument Out of Range");

        m_MapData[pos.Y, pos.X] = value;
    }

    public void Set(int x, int y, int value)
    {
        if (x < 0 || x >= Width ||
            y < 0 || y >= Height)
            throw new ArgumentOutOfRangeException("Map Pos Argument Out of Range");

        m_MapData[y, x] = value;
    }

    public bool IsObstacle(MapNode pos)
    {
        return Get(pos) == -1;
    }

    public bool IsLegal(MapNode pos)
    {
        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height) return false;

        return true;
    }

    public void DrawMap()
    {
        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                if (Get(MapNode.Create(w, h)) == -1)
                    Console.Write("{0,3}", "☗");
                else
                    Console.Write("{0,3}", Get(MapNode.Create(w, h)));
            }
            Console.WriteLine();
        }
    }

    public static void DrawMap(MapData2D map)
    {
        for (int h = 0; h < map.Height; h++)
        {
            for (int w = 0; w < map.Width; w++)
            {
                if (map.Get(MapNode.Create(w, h)) == -1)
                {
                    Console.Write("{0,3}", "☗");
                }
                else
                {
                    Console.Write("{0,3}", map.Get(MapNode.Create(w, h)));
                }
            }
            Console.WriteLine();
        }
    }

    public static void DrawMap(MapData2D map, List<MapNode> path)
    {
        foreach (var pos in path)
        {
            map.Set(pos, -2);
        }

        for (int h = 0; h < map.Height; h++)
        {
            for (int w = 0; w < map.Width; w++)
            {
                if (map.Get(MapNode.Create(w, h)) == -1)
                {
                    Console.Write("{0,3}", "☗");
                }
                else if (map.Get(MapNode.Create(w, h)) == -2)
                    Console.Write("{0,3}", "☐");
                else
                    Console.Write("{0,3}", map.Get(MapNode.Create(w, h)));
            }
            Console.WriteLine();
        }
    }
}