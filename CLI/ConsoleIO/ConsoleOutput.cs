namespace CLI.ConsoleIO;

internal static class ConsoleOutput
{
    // ASCII art titles for success and welcome
    private static readonly string s_asciiSuccess = $"{Environment.NewLine}   " +
        $".-'''-.   ___    _     _______       _______      .-''-.     .-'''-" +
        $".    .-'''-.  {Environment.NewLine}  / _     \\.'   |  | |   /   __" +
        $"  \\     /   __  \\   .'_ _   \\   / _     \\  / _     \\ " +
        $"{Environment.NewLine} (`' )/`--'|   .'  | |  | ,_/  \\__)   | ,_/  " +
        $"\\__) / ( ` )   ' (`' )/`--' (`' )/`--' {Environment.NewLine}(_ o _" +
        $").   .'  '_  | |,-./  )       ,-./  )      . (_ o _)  |(_ o _).   (" +
        $"_ o _).    {Environment.NewLine} (_,_). '. '   ( \\.-.|\\  '_ '`)  " +
        $"   \\  '_ '`)    |  (_,_)___| (_,_). '.  (_,_). '.  " +
        $"{Environment.NewLine}.---.  \\  :' (`. _` /| > (_)  )  __  > (_)  )" +
        $"  __'  \\   .---..---.  \\  :.---.  \\  : {Environment.NewLine}\\  " +
        $"  `-'  || (_ (_) _)(  .  .-'_/  )(  .  .-'_/  )\\  `-'    /\\    `-" +
        $"'  |\\    `-'  | {Environment.NewLine} \\       /  \\ /  . \\ / `-'" +
        $"`-'     /  `-'`-'     /  \\       /  \\       /  \\       /  " +
        $"{Environment.NewLine}  `-...-'    ``-'`-''    `._____.'     `._____" +
        $".'    `'-..-'    `-...-'    `-...-'   {Environment.NewLine}        " +
        $"                                                                   " +
        $"         {Environment.NewLine}";

    /// <summary>
    /// Prints ASCII art or a basic title for success, depending on the console window width.
    /// </summary>
    /// <param name="isStopped">Indicates whether to wait for a key press if true.</param>
    internal static void PrintSuccess(bool isStopped = false)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        // Limit width to fit to console.
        if (Console.WindowWidth < 90)
        {
            Console.WriteLine("Success!");
        }
        else
        {
            Console.WriteLine(s_asciiSuccess);
        }
        Console.ForegroundColor = ConsoleColor.White;

        // Wait for user input if specified
        if (isStopped)
            Console.ReadKey(true);
    }

    internal static void ClearBuffer()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();
    }

    internal static void PrintUntilKeyPressed(string data)
    {
        ClearBuffer();
        Console.WriteLine(data);
        Console.ReadKey(true);
    }

    internal static void PrintSelected(string text)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }

    internal static void PrintIssue(string issue, string fixRecommendation, bool isStopped = false)
    {
        ClearBuffer();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Issue: ");
        Console.WriteLine(issue);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(fixRecommendation);

        Console.ForegroundColor = ConsoleColor.White;
        if (isStopped)
            Console.ReadKey(true);
    }
}
