﻿namespace CLI.ConsoleIO;

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

    internal static string InputFilename()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a name of the JSON file with the extension: ");
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
            throw new ArgumentException("Invalid input value. It may be too large double or not a double");
        }
    }

    internal static DateTime InputDateTime()
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