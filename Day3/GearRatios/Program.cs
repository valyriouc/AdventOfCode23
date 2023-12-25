using System.Runtime.InteropServices;

namespace GearRatios;

class Program
{
    static void Main(string[] args)
    {
        Span<char> characters = File.ReadAllText("dummy.txt").ToArray();

        int lineLength = GetLineLength(characters);

        IEnumerable<(int, int)> result = YieldNumberPositions(characters);

        foreach ((int start, int end) in result) {
            Console.Write($"{start} : {end}");
            Console.WriteLine();
        }
        // get the neighbors around a number
    }

    private static int GetLineLength(Span<char> input){
        int lineLength = 0;

        foreach (char b in input) {
            lineLength += 1;
            if (b == '\n') break;
        }

        return lineLength;
    }

    public static IEnumerable<(int, int)> YieldNumberPositions(
        Span<char> input) {
        int start = -1;
        int end = -1;
        for (int i = 0; i < input.Length; i++) {
            int value = (int)input[i];
            if (value < 48 || value > 57) {
                if (start != -1 && end != -1)
                    yield return (start, end);
            }

            if (start != -1) {
                end = i;
            }
            else {
                start = i;
            }
        }
    }
}
