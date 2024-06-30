using System.Data;

namespace Csharp;

class Program
{
    static string DummyInput =
        """
        467..114..
        ...*......
        ..35..633.
        ......#...
        617*......
        .....+.58.
        ..592.....
        ......755.
        ...$.*....
        .664.598..
        """;

    static string Input =
        """

        """;

    static void Main(string[] args)
    {
        List<char> chars = DummyInput.ToList();
        

    }

    static IEnumerable<(int, int)> GetNumberCoordinates(List<char> input)
    {
        int start = -1;
        int end = -1;

        for (int i = 0; i < input.Count; i++)
        {
            int number = input[i];

            if (number < 48 || number > 57)
            {
                if (start != -1 && end != -1)
                {
                    yield return (start, end);
                    start = -1;
                    end = -1;
                }
                continue;
            }

            if (start == -1)
            {
                start = i;
            }
            else
            {
                end = i;
            }
        }
    }

    static IEnumerable<(int, int)> GetValidPairs(List<char> input, int start, int end)
    {

    }
   
}
