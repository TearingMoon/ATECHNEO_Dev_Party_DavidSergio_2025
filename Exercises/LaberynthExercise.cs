using System.IO.Pipelines;
using System.Transactions;

public class LaberynthExercise
{
    public char[,] map { get; set; } = new char[0, 0];

    public Coordinate start { get; set; }

    public Coordinate end { get; set; }

    static readonly int[] dx = { -1, 1, 0, 0 }; // movimiento vertical (arriba, abajo)
    static readonly int[] dy = { 0, 0, -1, 1 }; // movimiento horizontal (izq, der)
    static readonly char[] moveChar = { 'U', 'D', 'L', 'R' }; // direcciones (arriba, abajo, izquierda, derecha)

    public LaberynthExercise()
    {
        GetLaberynthFromFile();
        Console.WriteLine("Laberynth loaded successfully.");
        String solution = FindPath();
        Console.WriteLine("Result: " + solution);
        PrintSolvedLaberynth(solution);
    }


    public string FindPath()
    {
        Console.WriteLine("Laberynth size: " + map.GetLength(0) + " x " + map.GetLength(1));
        Console.WriteLine("Starting point: " + start.X + ", " + start.Y);
        Console.WriteLine("Ending point: " + end.X + ", " + end.Y);
        Console.WriteLine("Finding path...");

        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        bool[,] visited = new bool[rows, cols];

        (int x, int y)[,] parent = new (int, int)[rows, cols];
        char[,] moveTaken = new char[rows, cols];

        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
        queue.Enqueue((start.X, start.Y));
        visited[start.X, start.Y] = true;

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            if (x == end.X && y == end.Y)
            {
                Console.WriteLine("Path found!");
                string path = "";
                while ((x, y) != (start.X, start.Y))
                {
                    char move = moveTaken[x, y]; // Qué movimiento me trajo hasta aquí
                    path = move + path;          // Lo agrego al inicio de la cadena
                    (x, y) = parent[x, y];       // Me muevo al padre para seguir retrocediendo
                }
                ; // Eliminar el último movimiento, que es el que me lleva a la posición inicial


                return path.Length.ToString() + ":" + path;
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                // Check boundaries and walls
                if (nx >= 0 && ny >= 0 && nx < rows && ny < cols)
                {
                    if (!visited[nx, ny] && map[nx, ny] != '#')
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue((nx, ny));
                        parent[nx, ny] = (x, y);
                        moveTaken[nx, ny] = moveChar[i];
                    }
                }
            }
        }

        Console.WriteLine("No path found.");
        return "";
    }

    public void GetLaberynthFromFile()
    {
        String[] lines = File.ReadAllLines("data/laberinto.txt");
        map = new char[lines.Length, lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                map[i, j] = lines[i][j];

                if (map[i, j] == 'S')
                {
                    start = new Coordinate(i, j);
                }

                if (map[i, j] == 'E')
                {
                    end = new Coordinate(i, j);
                }
            }
        }
    }

    public void PrintSolvedLaberynth(string solution)
    {
        char[,] solvedMap = new char[map.GetLength(0), map.GetLength(1)];
        Array.Copy(map, solvedMap, map.Length);
        Coordinate start = new Coordinate(this.start.X, this.start.Y);
        foreach (char c in solution)
        {
            if (c == 'U')
            {
                start.X--;
            }
            else if (c == 'D')
            {
                start.X++;
            }
            else if (c == 'L')
            {
                start.Y--;
            }
            else if (c == 'R')
            {
                start.Y++;
            }
            solvedMap[start.X, start.Y] = 'X';
        }
        for (int i = 0; i < solvedMap.GetLength(0); i++)
        {
            for (int j = 0; j < solvedMap.GetLength(1); j++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (i == start.X && j == start.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (i == end.X && j == end.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (solvedMap[i, j] == 'X')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (solvedMap[i, j] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(solvedMap[i, j]);
            }
            Console.WriteLine();
        }
    }
}

public struct Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}