namespace Csharp;

class Program
{
    static string Commands { get; set; }

    static Queue<char> Instructions { get; set; }
        = new Queue<char>();

    static Dictionary<string, (string, string)> Inputs { get; set; } 
        = new Dictionary<string, (string, string)>();

    static void Main(string[] args)
    {
        Span<string> lines = File.ReadAllLines("../../../../input.txt");

        Commands = lines[0];

        FillInstructionQueue();
        ParseInputs(lines.Slice(2));

        IEnumerator<char> enumerator = Instructions.GetEnumerator();    
        int count = 0;

        string key = "AAA";
        while (true)
        {
            if (!enumerator.MoveNext())
            {
                FillInstructionQueue();
                enumerator = Instructions.GetEnumerator();
                enumerator.MoveNext();
            }

            char instruction = enumerator.Current;
            key = instruction == 'L' ? Inputs[key].Item1 : Inputs[key].Item2;
            count++;

            if (key == "ZZZ")
            {
                break;
            }
        }

        Console.WriteLine(count);
    }

    static void FillInstructionQueue()
    {
        foreach (char command in Commands)
        {
            Instructions.Enqueue(command);  
        }
    }

    static void ParseInputs(Span<string> inputs)
    {
        foreach (string input in inputs)
        {
            string[] split = input.Split("=");
            string key = split[0].Trim();
            (string, string) value = ParseLeftRight(split[1]);
            Inputs.Add(key, value);
        }
    }

    static (string, string) ParseLeftRight(string input)
    {
        string[] split = input
            .Trim()
            .Substring(1)
            .Split(",");

        return (split[0].Trim(), split[1].Replace(")", string.Empty).Trim());
    }
}
