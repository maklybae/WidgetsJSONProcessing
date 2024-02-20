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
}
