using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace GraphSolution;

internal enum Direction
{
    Up,
    Down
}

internal static class PipeTypeExtensions
{
    public static bool IsValidFollower(this PipeType current, PipeType next, Direction direction)
    {
        if (current == PipeType.Start)
        {
            return true;
        }

        if (current == PipeType.NorthSourth && yDiff == Difference.Smaller && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.NorthEast || next == PipeType.NorthWest;
        }
        else if (current == PipeType.NorthSourth && yDiff == Difference.Higher && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.EastSourth || next == PipeType.WestSourth;
        }
        else if (current == PipeType.EastWest && xDiff == Difference.Higher && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthEast || next == PipeType.EastSourth;
        }
        else if (current == PipeType.EastWest && xDiff == Difference.Smaller && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthWest || next == PipeType.WestSourth;
        }
        else if (current == PipeType.NorthEast && xDiff == Difference.Smaller && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthWest || next == PipeType.WestSourth;
        }
        else if (current == PipeType.NorthEast && yDiff == Difference.Higher && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.WestSourth || next == PipeType.EastSourth;
        }
        else if (current == PipeType.NorthWest && xDiff == Difference.Higher && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthEast || next == PipeType.EastSourth;
        }
        else if (current == PipeType.NorthWest && yDiff == Difference.Higher && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.WestSourth || next == PipeType.EastSourth;
        }
        else if (current == PipeType.WestSourth && xDiff == Difference.Higher && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthEast || next == PipeType.EastSourth;
        }
        else if (current == PipeType.WestSourth && yDiff == Difference.Smaller && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.NorthEast || next == PipeType.NorthWest;
        }
        else if (current == PipeType.EastSourth && yDiff == Difference.Smaller && xDiff == Difference.Equal)
        {
            return next == PipeType.NorthSourth || next == PipeType.NorthEast || next == PipeType.NorthWest;
        }
        else if (current == PipeType.EastSourth && xDiff == Difference.Smaller && yDiff == Difference.Equal)
        {
            return next == PipeType.EastWest || next == PipeType.NorthWest || next == PipeType.WestSourth;
        }

        return false;
    }
}
