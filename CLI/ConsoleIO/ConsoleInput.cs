namespace CLI.ConsoleIO;

internal class ConsoleInput
{
    public static string InputStringWithCursor()
    {
        Console.CursorVisible = true;
        string input = Console.ReadLine() ?? "";
        Console.CursorVisible = false;
        return input;
    }

    public static string InputFullPath()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a full path to file with the .json extension: ");
        return InputStringWithCursor();
    }

    public static string InputFilename()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a name of the JSON file with the extension: ");
        return InputStringWithCursor();
    }

    public static int InputIntegerInRange(int lowerBound, int upperBound)
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

    public static double InputDoubleInRange(int lowerBound, int upperBound)
    {
        var input = InputStringWithCursor();

        // Check whether it fulfills requirements.
        if (double.TryParse(input, out double res) && lowerBound <= res && res <= upperBound)
        {
            return res;
        }
        else
        {
            throw new ArgumentException("Invalid input value. It may be too large double or not a double");
        }
    }

    public static DateTime InputDateTime()
    {
        var input = InputStringWithCursor();

        // Check whether it fulfills requirements.
        if (DateTime.TryParse(input, out DateTime res))
        {
            return res;
        }
        else
        {
            throw new ArgumentException("Invalid input value. Check the correctness of format");
        }
    }
}
