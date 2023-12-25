using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp;

internal static class PipeTypeExtensions
{
    public static PipeType FromChar(this PipeType pipeType, char ch) => ch switch
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

}
