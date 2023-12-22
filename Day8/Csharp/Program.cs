using System.Collections.Concurrent;

namespace Csharp;

class Program
{
    static string Commands { get; set; }

    static Dictionary<string, (string, string)> Inputs { get; set; } 
        = new Dictionary<string, (string, string)>();

    static void Main(string[] args)
    {
        Span<string> lines = File.ReadAllLines("../../../../input.txt");

        Commands = lines[0];

        ParseInputs(lines.Slice(2));

        Solution2();

        //Solution1();
    }

    static void Solution1()
    {
        Queue<char> instructions = FillInstructionQueue();

        IEnumerator<char> enumerator = instructions.GetEnumerator();
        int count = 0;

        string key = "AAA";
        while (true)
        {
            if (!enumerator.MoveNext())
            {
                instructions = FillInstructionQueue();
                enumerator = instructions.GetEnumerator();
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

    static void Solution2()
    {
        // finding the number of keys ending with A 
        Queue<char> instructions = FillInstructionQueue();
        IEnumerator<char> enumer = instructions.GetEnumerator();

        IEnumerable<string> startKeys = Inputs.Keys.Where(key => key.EndsWith("Z"));

        List<(string, int, bool)> results = new List<(string, int, bool)>();

        foreach (string startKey in startKeys)
        {
            results.Add((startKey, 0, false));
        }

        int counter = 0;
        bool movingForward = true; 

        while(true)
        {
            // Forbid moving forward when not updated 

            if (true)
            {
                if (!enumer.MoveNext())
                {
                    instructions = FillInstructionQueue();
                    enumer = instructions.GetEnumerator();
                    enumer.MoveNext();
                }
            }

            if (results[counter].Item3 == false)
            {
                movingForward = true;
                char instruction = enumer.Current;
                string newKey = instruction == 'L' ? 
                    Inputs[results[counter].Item1].Item1 : 
                    Inputs[results[counter].Item1].Item2;

                int increment = results[counter].Item2 + 1;
                if (newKey.EndsWith("Z"))
                {
                    results[counter] = (newKey, increment, true);
                }
                else
                {
                    results[counter] = (newKey, increment, false);
                }
            }
            else
            {
                // Do not move 
                //movingForward = false;
            }

            counter++;

            if (counter == startKeys.Count())
            {
                counter = 0;
            }

            foreach ((string key, int count, bool finished) in results)
            {
                Console.WriteLine($"{key} : {count} : {finished}");
            }
            Console.WriteLine();

            if (results.All(x => x.Item3 == true))
            {
                Console.WriteLine("Finished");
                break;
            }
        }

        int max = results.Max(x => x.Item2);
        Console.WriteLine(max);
    }

    static Queue<char> FillInstructionQueue()
    {
        Queue<char> queue = new Queue<char> ();

        foreach (char command in Commands)
        {
            queue.Enqueue(command);    
        }

        return queue;
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
