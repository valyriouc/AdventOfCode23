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

public class Program
{
    static void Main(string[] args)
    {
        Span<string> input = File.ReadAllLines("../../../../input");

        (int rows, int columns) = GetFieldSize(input);

        PipeType[,] field = ParseInput(input, rows, columns);

        // searching for the start field 
        (int y, int x) = GetStartIndex(field, columns);

        IEnumerable<PipeType> loop = BuildLoop(field, y, x);

        //Console.WriteLine(loop.Count() / 2); // Here comes the result

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

    public static (bool, PipeType) BuildLoopInternal(
        PipeType[,] input,
        List<PipeType> output,
        int yStart, 
        int xStart,
        int yOld, 
        int xOld)
    {
        bool saveState = false;
        PipeType saveSign = PipeType.Empty;

        foreach ((int y, int x) in GetNextCoordinates(input[yStart, xStart], yStart, xStart))
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

            (bool state, PipeType sign) = BuildLoopInternal(input, output, y, x, yStart, xStart);
             
            if (state)
            {
                output.Add(sign);
                saveState = true;
                saveSign = input[y, x];
                break;
            }
        }

        return (saveState, saveSign);
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

        if (yStart < 4 &&
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

        if (xStart < 4 && 
            (current == PipeType.EastWest || 
            current == PipeType.NorthEast ||
            current == PipeType.EastSourth ||
            current == PipeType.Start))
        {
            yield return (yStart, xStart + 1);
        }
    }
}
