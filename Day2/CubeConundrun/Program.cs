namespace CubeConundrun;

struct Game {

    public int Number { get; set; }

    public int Red { get; set; }

    public int Green { get; set;}

    public int Blue { get; set;}


    public bool IsValid(int mRed, int mGreen, int mBlue) {
        return Red <= mRed && Green <= mGreen && Blue <= mBlue;
    }
}

class Program
{
    public static int MaxRed { get; } = 12;

    public static int MaxGreen { get; } = 13;

    public static int MaxBlue { get; } = 14;

    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        int result = 0;

        foreach (string line in lines) {
            // parsing the game number
            string[] parts = line.Split(":");

            int gameNumber = GetGameNumber(parts[0]);

            bool possible = ParseGameState(parts[1]);

            if (possible) {
                result += gameNumber;
            }
        }

        Console.WriteLine(result);
    }

    static int GetGameNumber(string input) {
        string sub = input.Substring(5);
        return int.Parse(sub);
    }

    static bool ParseGameState(string input) {
        string[] sets = input.Split(";");
        foreach (string set in sets) {
            string[] parts = set.Split(",");
            foreach (string part in parts){
                string[] numberColor = part.Trim().Split(" ");
                int number = int.Parse(numberColor[0]);
                switch(numberColor[1][0]) {
                    case 'b':
                        if (number > MaxBlue) 
                            return false;
                        break;
                    case 'r':
                        if (number > MaxRed) 
                            return false;
                        break;
                    case 'g':
                        if (number > MaxGreen) 
                            return false;
                        break;
                }
            }
        }

        return true;
    }
}
