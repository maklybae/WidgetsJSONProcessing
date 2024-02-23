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
}
