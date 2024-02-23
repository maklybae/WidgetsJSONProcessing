using Model;

namespace CLI;

internal class ConsoleDialog
{
    /// <summary>
    /// Takes user input for a string field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted string.</returns>
    internal static string InputStringField(string requestedName)
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
    internal static bool InputBooleanField(string requestedName)
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
    internal static int InputIntegerField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input integer field \"{requestedName}\": ");
        return ConsoleInput.InputIntegerInRange(int.MinValue, int.MaxValue);
    }

    internal static double InputDoubleField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input double field \"{requestedName}\": ");
        return ConsoleInput.InputDoubleInRange(int.MinValue, int.MaxValue);
    }

    internal static DateTime InputDateTimeField(string requestedName)
    {
        Console.WriteLine();
        Console.Write($"Input DateTime field \"{requestedName}\" in format DD/MM/YYYY hh:mm:ss :");
        return ConsoleInput.InputDateTime();
    }

    internal static SortingOptions InputSortingOption(string field)
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
    internal static Dictionary<string, SortingOptions> InputSortingOptionsDictionary(List<string> fields)
    {
        Dictionary<string, SortingOptions> res = new();
        foreach (var field in fields)
        {
            res.Add(field, InputSortingOption(field));
        }
        return res;
    }
}
