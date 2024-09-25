namespace PathFindingAlgo;

public class Program
{
    public static void Main(string[] args)
    {
        RunAStar();
    }

    private static void RunAStar()
    {
        var map = MapData2D.RandomMap(10, 10);
        var astar = new AStar();
        var result = astar.FindPath(map, MapNode.Create(0, 0), MapNode.Create(5, 5));

        if (result == null)
        {
            Console.WriteLine("Not Found Path");
        }
        else
        {
            foreach (var pos in result)
            {
                Console.WriteLine($"{pos.X}, {pos.Y}");
            }
        }

        Console.WriteLine();
        MapData2D.DrawMap(map, result);
        Console.WriteLine();
        MapData2D.DrawMap(map);
    }
}