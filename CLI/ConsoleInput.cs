namespace CLI;

internal class ConsoleInput
{
    internal static string InputStringWithCursor()
    {
        Console.CursorVisible = true;
        string input = Console.ReadLine() ?? "";
        Console.CursorVisible = false;
        return input;
    }

    internal static string InputFullPath()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a full path to file with the .json extension: ");
        return InputStringWithCursor();
    }

    internal static int InputIntegerInRange(int lowerBound, int upperBound)
    {
        var input = InputStringWithCursor();

        // Check whether it fulfills requirements.
        if (int.TryParse(input, out int res) && lowerBound <= res && res <= upperBound)
        {
            return res;
        }
        else
        {
            throw new ArgumentException("Invalid input value. It may be too large integer or not an integer");
        }
    }

    internal static double InputDoubleInRange(int lowerBound, int upperBound)
    {
        var input = InputStringWithCursor();

        // Check whether it fulfills requirements.
        if (double.TryParse(input, out double res) && lowerBound <= res && res <= upperBound)
        {
            return res;
        }
        else
        {
            throw new ArgumentException("Invalid input value. It may be too large integer or not an integer");
        }
    }
}
