using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp;

internal abstract class SolverBase
{
    protected string Commands { get; }

    protected Dictionary<string, (string, string)> Input { get; }

    public SolverBase()
    {
        Span<string> lines = File.ReadAllLines("../../../../input.txt");

        Commands = lines[0];
        Input = ParseInputs(lines.Slice(2)); 
    }

    public abstract int Solve();

    protected Queue<char> FillInstructionQueue(string commandQueue)
    {
        Queue<char> queue = new Queue<char>();

        foreach (char command in commandQueue)
        {
            queue.Enqueue(command);
        }

        return queue;
    }

    protected static Dictionary<string, (string, string)> ParseInputs(Span<string> inputs)
    {
        Dictionary<string, (string, string)> dict = new Dictionary<string, (string, string)>();

        foreach (string input in inputs)
        {
            string[] split = input.Split("=");
            string key = split[0].Trim();
            (string, string) value = ParseLeftRight(split[1]);
            dict.Add(key, value);
        }

        return dict;
    }

    protected static (string, string) ParseLeftRight(string input)
    {
        string[] split = input
            .Trim()
            .Substring(1)
            .Split(",");

        return (split[0].Trim(), split[1].Replace(")", string.Empty).Trim());
    }
}
