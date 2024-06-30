using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp;

internal class Solver2 : SolverBase
{
    public override int Solve()
    {
        Queue<char> instructions = FillInstructionQueue(Commands);

        List<string> keys = Input.Keys
            .Where(x => x.EndsWith("A"))
            .ToList();

        IEnumerator<char> enumerator = instructions.GetEnumerator();

        int count = 0;

        while(true)
        {
            if(!enumerator.MoveNext())
            {
                instructions = FillInstructionQueue(Commands);
                enumerator = instructions.GetEnumerator();
                enumerator.MoveNext();
            }

            char instruction = enumerator.Current;
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i] = instruction == 'L' ? Input[keys[i]].Item1 : Input[keys[i]].Item2;
            }

            count += 1;

            Console.WriteLine(count);

            if (keys.All(x => x.EndsWith("Z")))
            {
                break;
            }
        }

        return count;   
    }
}
