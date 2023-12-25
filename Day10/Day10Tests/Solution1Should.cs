using Csharp;

using Xunit;

namespace Day10Tests
{
    public class Solution1Should
    {
        [Fact]
        public void ReturnCorrectFieldData()
        {
            string field =
                """
                ........
                ........
                ........
                ........
                ........
                """;

            (int rows, int columns) = Program.GetFieldSize(field.Replace("\r", "").Split("\n"));

            Assert.Equal(5, rows);
            Assert.Equal(8, columns);
        }

        [Fact]
        public void ReturnCorrectFieldWithTypes()
        {
            string input =
                """
                .....
                .S-7.
                .|.|.
                .L-J.
                .....
                """;

            //PipeType[,] field = Program.ParseInput(input.Replace("\r", "").Split("\n"));

            
        }
    }
}
