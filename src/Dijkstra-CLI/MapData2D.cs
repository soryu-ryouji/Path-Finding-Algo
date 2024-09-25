using System.Diagnostics.CodeAnalysis;

namespace PathFindingAlgo;


public struct MapPos(int x, int y)
{
    public int X = x;
    public int Y = y;

    private static int GetManhattanDistance(MapPos startPos, MapPos endPos)
    {
        throw new NotImplementedException();
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
        var mapData = new MapData2D(w, h);
        var random = new Random();
        for (int i = 0; i < mapData.Width; i++)
        {
            for (int j = 0; j < mapData.Height; j++)
            {
                var value = random.Next(0, 10) < 2 ? -1 : random.Next(0, 10);
                mapData.Set(new MapPos(i, j), value);
            }
        }

        return mapData;
    }

    public int Get(MapPos pos)
    {
        if (pos.X < 0 || pos.X >= Width ||
            pos.Y < 0 || pos.Y >= Height)
            throw new ArgumentOutOfRangeException("Map Pos Argument Out of Range");

        return m_MapData[pos.Y, pos.X];
    }

    public int Get(int x, int y)
    {
        if (x < 0 || x >= Width ||
            y < 0 || y >= Height)
            throw new ArgumentOutOfRangeException("Map Pos Argument Out of Range");

        return m_MapData[x, y];
    }

    public void Set(MapPos pos, int value)
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

        m_MapData[x, y] = value;
    }

    public bool IsObstacle(MapPos pos)
    {
        return Get(pos) == -1;
    }

    public bool IsLegal(MapPos pos)
    {
        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height) return false;

        return true;
    }

    public void DrawMap()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (m_MapData[i, j] == -1)
                    Console.Write("{0,3}", "*");
                else
                    Console.Write("{0,3}", m_MapData[i, j]);
            }
            Console.WriteLine();
        }
    }

    public static void DrawMap(MapData2D map)
    {
        for (int i = 0; i < map.Height; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                if (map.Get(i, j) == -1)
                    Console.Write("{0,3}", "*");
                else
                    Console.Write("{0,3}", map.Get(i, j));
            }
            Console.WriteLine();
        }
    }
}