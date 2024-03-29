﻿using Model;

namespace CLI.ConsoleIO;

/// <summary>
/// Provides methods for interacting with the console to input various field types.
/// </summary>
internal class ConsoleDialog
{
    /// <summary>
    /// Takes user input for a string field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted string.</returns>
    public static string InputStringField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input string field \"{requestedName}\": ");
        return ConsoleInput.InputStringWithCursor();
    }

    /// <summary>
    /// Takes user input for a boolean field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>True if the user presses 't', false if 'f'.</returns>
    public static bool InputBooleanField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input string field \"{requestedName}\": ");
        Console.Write("Press QWERTY 't' for true, 'f' for false: ");
        while (true)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.T:
                    Console.WriteLine("true");
                    return true;
                case ConsoleKey.F:
                    Console.WriteLine("false");
                    return false;
            }
        }
    }

    /// <summary>
    /// Takes user input for an integer field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted integer.</returns>   
    public static int InputIntegerField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input integer field \"{requestedName}\": ");
        return ConsoleInput.InputIntegerInRange(int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Takes user input for a double field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted double.</returns>
    public static double InputDoubleField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input double field \"{requestedName}\": ");
        return ConsoleInput.InputDoubleInRange(int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Takes user input for a DateTime field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted DateTime.</returns>
    public static DateTime InputDateTimeField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input DateTime field \"{requestedName}\" in format DD/MM/YYYY hh:mm:ss :");
        return ConsoleInput.InputDateTime();
    }

    /// <summary>
    /// Takes user input to choose a sorting option for a field.
    /// </summary>
    /// <param name="field">The field for which the sorting option is chosen.</param>
    /// <returns>Sorting option for the field.</returns>
    public static SortingOptions InputSortingOption(string field)
    {
        while (true)
        {
            ConsoleOutput.ClearBuffer();
            Console.WriteLine($"Choosing sorting option for field: {field}");
            Console.WriteLine("Press QWERTY 'a' to sort with the ascending option");
            Console.WriteLine("Press QWERTY 'd' to sort with the descending option");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.A:
                    return SortingOptions.Ascending;
                case ConsoleKey.D:
                    return SortingOptions.Descending;
                default:
                    ConsoleOutput.PrintIssue("Incorrect button has been pressed", "Try again", true);
                    continue;
            }
        }
    }

    /// <summary>
    /// Takes user input to create a dictionary of sorting options for a list of fields.
    /// </summary>
    /// <param name="fields">List of fields for which sorting options are chosen.</param>
    /// <returns>Dictionary containing sorting options for each field.</returns>
    public static Dictionary<string, SortingOptions> InputSortingOptionsDictionary(List<string> fields)
    {
        Dictionary<string, SortingOptions> res = new();
        foreach (var field in fields)
        {
            res.Add(field, InputSortingOption(field));
        }
        return res;
    }

    /// <summary>
    /// Takes user input to confirm whether to overwrite an existing file.
    /// </summary>
    /// <returns>True if the user chooses to overwrite, false otherwise.</returns>
    public static bool InputOverwriteFile()
    {
        while (true)
        {
            ConsoleOutput.ClearBuffer();
            Console.WriteLine("Press QWERTY 'y' to overwrite existing file");
            Console.WriteLine("Press QWERTY 'n' otherwise");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    return true;
                case ConsoleKey.N:
                    return false;
                default:
                    ConsoleOutput.PrintIssue("Incorrect button has been pressed", "Try again", true);
                    continue;
            }
        }
    }
}
