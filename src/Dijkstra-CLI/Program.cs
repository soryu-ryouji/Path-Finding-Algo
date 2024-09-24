namespace PathFindingAlgo;

public class Program
{
    public static void Main(string[] args)
    {
        var map = MapData2D.RandomMap(10, 10);
        MapData2D.DrawMap(map);
    }
}