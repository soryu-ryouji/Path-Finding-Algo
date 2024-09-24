namespace PathFindingAlgo;

public class Program
{
    public static void Main(string[] args)
    {
        var map = RandomMap(10, 10);
        DrawMap(map);
    }

    public static int[][] RandomMap(int w, int h)
    {
        // -1 表示障碍物
        // 0 - 100 表示通行的代价
        // 随机生成一张 宽 为 w, 高为 h 的二维数组
        var map = new int[h][];
        var random = new Random();

        for (int i = 0; i < h; i++)
        {
            map[i] = new int[w];
            for (int j = 0; j < w; j++)
            {
                map[i][j] = random.Next(0, 10) < 2 ? -1 : random.Next(0, 10);
            }
        }
        return map;
    }

    private static void DrawMap(int[][] map)
    {
        var height = map.Length;
        var width = map[0].Length;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i][j] == -1)
                    Console.Write("{0,3}", "*");
                else
                    Console.Write("{0,3}", map[i][j]);
            }
            Console.WriteLine();
        }
    }
}