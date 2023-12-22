namespace Csharp;

internal class Solver1 : SolverBase
{
    public override int Solve()
    {
        Queue<char> instructions = FillInstructionQueue(Commands);

        IEnumerator<char> enumerator = instructions.GetEnumerator();
        int count = 0;

        string key = "AAA";
        while (true)
        {
            if (!enumerator.MoveNext())
            {
                instructions = FillInstructionQueue(Commands);
                enumerator = instructions.GetEnumerator();
                enumerator.MoveNext();
            }

            char instruction = enumerator.Current;
            key = instruction == 'L' ? Input[key].Item1 : Input[key].Item2;
            count++;

            if (key == "ZZZ")
            {
                break;
            }
        }

        return count;
    }
}
