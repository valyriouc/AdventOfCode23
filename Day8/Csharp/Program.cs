using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace Csharp;

class Program
{
    static void Main(string[] args)
    {
        SolverBase solver = args[0] == "1" ? 
            new Solver1() : 
            new Solver2();

        int count = solver.Solve();

        Console.WriteLine(count);
    }
}
