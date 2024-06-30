namespace Csharp;

class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadAllLines("../../../../input.txt").ToList();

        int result = 0;

        foreach (List<int> dataset in ReturnDataPoints(input))
        {
            result += YieldExtrapolations(dataset);
        }

        Console.WriteLine(result);
    }

    static IEnumerable<List<int>> ReturnDataPoints(List<string> span)
    {
        foreach (string input in span)
        {
            yield return input
                .Split(" ")
                .Reverse()
                .Take(4)
                .Reverse()
                .Select(x => int.Parse(x))
                .ToList();
        }
    }

    static int YieldExtrapolations(List<int> dataset)
    {
        for (int i = 0; i < dataset.Count - 1; i++)
        {
            List<int> sub = new List<int>();

            int result = dataset[i + 1] - dataset[i];

            sub.Add(result);
        }



        return ;
    }
}
