namespace CLI.ConsoleIO;

/// <summary>
/// Provides methods for handling console input.
/// </summary>
internal class ConsoleInput
{
    /// <summary>
    /// Reads a line of input from the console with the cursor visible.
    /// </summary>
    /// <returns>User-inputted string.</returns>
    public static string InputStringWithCursor()
    {
        Console.CursorVisible = true;
        string input = Console.ReadLine() ?? "";
        Console.CursorVisible = false;
        return input;
    }

    /// <summary>
    /// Prompts the user to input a full file path.
    /// </summary>
    /// <returns>User-inputted full file path.</returns>
    public static string InputFullPath()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a full path to file with the .json extension: ");
        return InputStringWithCursor();
    }

    /// <summary>
    /// Prompts the user to input a filename with an extension.
    /// </summary>
    /// <returns>User-inputted filename with an extension.</returns>
    public static string InputFilename()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a name of the JSON file with the extension: ");
        return InputStringWithCursor();
    }

    /// <summary>
    /// Prompts the user to input an integer within a specified range.
    /// </summary>
    /// <param name="lowerBound">The lower bound of the valid range.</param>
    /// <param name="upperBound">The upper bound of the valid range.</param>
    /// <returns>User-inputted integer within the specified range.</returns>
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

    /// <summary>
    /// Prompts the user to input a double within a specified range.
    /// </summary>
    /// <param name="lowerBound">The lower bound of the valid range.</param>
    /// <param name="upperBound">The upper bound of the valid range.</param>
    /// <returns>User-inputted double within the specified range.</returns>
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

    /// <summary>
    /// Prompts the user to input a DateTime value.
    /// </summary>
    /// <returns>User-inputted DateTime value.</returns>
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
