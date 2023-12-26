namespace Csharp;

public enum PipeType
{
    NorthSourth = 0,
    EastWest = 1,
    NorthEast = 2,
    NorthWest = 3,
    WestSourth = 4,
    EastSourth = 5,
    Empty = 6,
    Start = 7,
    End = 8
}

public enum Difference
{
    Smaller = 0,
    Equal = 1,
    Higher = 2
}

public class Program
{
    public static int Rows { get; set; }

    public static int Columns { get; set; }

    static void Main(string[] args)
    {
        Span<string> input = File.ReadAllLines("../../../../input");

        (int rows, int columns) = GetFieldSize(input);

        Rows = rows;
        Columns = columns;

        PipeType[,] field = ParseInput(input, rows, columns);

        // searching for the start field 
        (int y, int x) = GetStartIndex(field, columns);

        IEnumerable<PipeType> loop = BuildLoop(field, y, x);

        foreach (PipeType t in loop)
        {
            Console.Write(t + " ");
        }

        Console.WriteLine(loop.Count() / 2);    
    }

    public static PipeType[,] ParseInput(Span<string> input, int rows, int columns)
    {
        PipeType[,] field = new PipeType[rows, columns];

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                field[i, j] = FromChar(input[i][j]);
            }
        }

        return field;
    }

    public static (int, int) GetFieldSize(Span<string> input) =>
        (input.Length, input[0].Length);

    public static PipeType FromChar(char ch) => ch switch
    {
        'S' => PipeType.Start,
        '|' => PipeType.NorthSourth,
        '-' => PipeType.EastWest,
        'L' => PipeType.NorthEast,
        'J' => PipeType.NorthWest,
        '7' => PipeType.WestSourth,
        'F' => PipeType.EastSourth,
        _ => PipeType.Empty
    };

    public static (int, int) GetStartIndex(PipeType[,] array, int columnCount)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (array[i, j] == PipeType.Start)
                {
                    return (i, j);
                }
            }
        }

        throw new Exception("No start in input!");
    }

    public static IEnumerable<PipeType> BuildLoop(PipeType[,] array, int yStart, int xStart)
    {
        List<PipeType> output = new List<PipeType>();   
        (bool state, PipeType first) = BuildLoopInternal(array, output, yStart, xStart, yStart, xStart);
        output.Add(first);
        return output;
    }

    private static (bool, PipeType) BuildLoopInternal(
        PipeType[,] input,
        List<PipeType> output,
        int yStart, 
        int xStart,
        int yOld, 
        int xOld)
    {
        bool saveState = false;
        PipeType saveSign = PipeType.Empty;

        PipeType current = input[yStart, xStart];
        foreach ((int y, int x) in GetNextCoordinates(current, yStart, xStart))
        {
            if (input[y, x] == PipeType.Empty || 
                (y == yOld && x == xOld))
            {
                continue;
            }

            if (input[y, x] == PipeType.Start)
            {
                return (true, PipeType.End);
            }

            PipeType next = input[y, x];

            Difference yDiff = GetDifference(yStart, y);
            Difference xDiff = GetDifference(xStart, x);

            if (IsValidFollower(current, next, yDiff, xDiff))
            {
                (bool state, PipeType sign) = BuildLoopInternal(input, output, y, x, yStart, xStart);

                if (state)
                {
                    output.Add(sign);
                    saveState = true;
                    saveSign = input[y, x];
                    break;
                }
            }
            else
            {
                continue;
            }
        }

        return (saveState, saveSign);
    }

    public static Difference GetDifference(int current, int next)
    {
        if (current == next)
        {
            return Difference.Equal;
        }

        return current > next ? Difference.Higher : Difference.Smaller;
    }

    public static IEnumerable<(int, int)> GetNextCoordinates(PipeType current, int yStart, int xStart)
    {
        if (yStart > 0 && 
            (current == PipeType.NorthSourth ||
            current == PipeType.NorthEast ||
            current == PipeType.NorthWest ||
            current == PipeType.Start))
        {
            yield return (yStart - 1, xStart);
        }

        if (yStart < Rows - 1 &&
            (current == PipeType.NorthSourth ||
            current == PipeType.WestSourth ||
            current == PipeType.EastSourth ||
            current == PipeType.Start))
        {
            yield return (yStart + 1, xStart);
        }

        if (xStart > 0 && 
            (current == PipeType.EastWest ||
            current == PipeType.NorthWest ||
            current == PipeType.WestSourth ||
            current == PipeType.Start))
        {
            yield return (yStart, xStart - 1);
        }

        if (xStart < Columns - 1 && 
            (current == PipeType.EastWest || 
            current == PipeType.NorthEast ||
            current == PipeType.EastSourth ||
            current == PipeType.Start))
        {
            yield return (yStart, xStart + 1);
        }
    }

    private static bool IsValidFollower(PipeType current, PipeType next, Difference yDiff, Difference xDiff)
    {
        if (current == PipeType.Start)
        {
            return true;
        }

        if (current == PipeType.NorthSourth && yDiff == Difference.Smaller && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.NorthEast || next == PipeType.NorthWest;
        }
        else if (current == PipeType.NorthSourth && yDiff == Difference.Higher && xDiff == Difference.Equal)
         {
            return next == PipeType.NorthSourth || next == PipeType.EastSourth || next == PipeType.WestSourth;
        }
        else if (current == PipeType.EastWest && xDiff == Difference.Higher && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthEast || next == PipeType.EastSourth;
        }
        else if (current == PipeType.EastWest && xDiff == Difference.Smaller && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthWest || next == PipeType.WestSourth;
        }
        else if (current == PipeType.NorthEast && xDiff == Difference.Smaller && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthWest || next == PipeType.WestSourth;
        }
        else if (current == PipeType.NorthEast && yDiff == Difference.Higher && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.WestSourth || next == PipeType.EastSourth;
        }
        else if (current == PipeType.NorthWest && xDiff == Difference.Higher && yDiff == Difference.Equal )
        {
            return next == PipeType.EastWest || next == PipeType.NorthEast || next == PipeType.EastSourth;
        }
        else if (current == PipeType.NorthWest && yDiff == Difference.Higher && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.WestSourth || next == PipeType.EastSourth;
        }
        else if (current == PipeType.WestSourth && xDiff == Difference.Higher && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthEast || next == PipeType.EastSourth;
        }
        else if (current == PipeType.WestSourth && yDiff == Difference.Smaller && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.NorthEast || next == PipeType.NorthWest;
        }
        else if (current == PipeType.EastSourth && yDiff == Difference.Smaller && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.NorthEast || next == PipeType.NorthWest;
        }
        else if (current == PipeType.EastSourth && xDiff == Difference.Smaller && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthWest || next == PipeType.WestSourth;
        }

        return false;
    }
}
