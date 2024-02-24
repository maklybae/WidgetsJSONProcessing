namespace CLI.ConsoleIO;

/// <summary>
/// Provides methods for handling console output.
/// </summary>
internal static class ConsoleOutput
{
    private const string HelpText = @"This program was created by Maksim Klychkov as a solution to homework 2, module 3, variant 4.
Nota Bene: The program supports only formatted file as in the sample *.JSON file.
The program is controlled using the up/down arrows and enter button (left/right arrows are also useful for switching between pages in table-view)

Firstly, you need to enter JSON data from console via absolute path or with file manager (which shows system drives, folders in current folder and *.JSON files).
Then, you will be able to:
- print data in table-view
- sort data by several fields at the same time
- change field of objects
- choose new file to process";
    private const string Author = "© Klychkov Maksim, 2311";

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

    // Fun title Welcome.
    private static readonly string s_asciiWelcome = $".--.      .--.    .-''-.  " +
        $"  .---.        _______      ,-----.    ,---.    ,---.    .-''-.   " +
        $"{Environment.NewLine}|  |_     |  |  .'_ _   \\   | ,_|       /   __ " +
        $" \\   .'  .-,  '.  |    \\  /    |  .'_ _   \\  {Environment.NewLine}|" +
        $" _( )_   |  | / ( ` )   ',-./  )      | ,_/  \\__) / ,-.|  \\ _ \\ | " +
        $" ,  \\/  ,  | / ( ` )   ' {Environment.NewLine}|(_ o _)  |  |. (_ o _" +
        $")  |\\  '_ '`)  ,-./  )      ;  \\  '_ /  | :|  |\\_   /|  |. (_ o _)" +
        $"  | {Environment.NewLine}| (_,_) \\ |  ||  (_,_)___| > (_)  )  \\  '_ " +
        $"'`)    |  _`,/ \\ _/  ||  _( )_/ |  ||  (_,_)___| {Environment.NewLine}" +
        $"|  |/    \\|  |'  \\   .---.(  .  .-'   > (_)  )  __: (  '\\_/ \\   ;| " +
        $"(_ o _) |  |'  \\   .---. {Environment.NewLine}|  '  /\\  `  | \\  `-' " +
        $"   / `-'`-'|___(  .  .-'_/  )\\ `\"/  \\  ) / |  (_,_)  |  | \\  `-'   " +
        $" / {Environment.NewLine}|    /  \\    |  \\       /   |        \\`-'`-' " +
        $"    /  '. \\_/``\".'  |  |      |  |  \\       /  {Environment.NewLine}`-" +
        $"--'    `---`   `'-..-'    `--------`  `._____.'     '-----'    '--'      " +
        $"'--'   `'-..-'   {Environment.NewLine}                                   " +
        $"                                                         ";

    /// <summary>
    /// Prints ASCII art or a basic title for success, depending on the console window width.
    /// </summary>
    /// <param name="isStopped">Indicates whether to wait for a key press if true.</param>
    public static void PrintSuccess(bool isStopped = false)
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

    /// <summary>
    /// Prints an ASCII art or a basic title for welcome, depending on the console window width.
    /// </summary>
    public static void PrintWelcome()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        // Limit width to fit to console.
        if (Console.WindowWidth < 95)
        {
            Console.WriteLine("Welcome!");
        }
        else
        {
            Console.WriteLine(s_asciiWelcome);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Clears the console buffer.
    /// </summary>
    public static void ClearBuffer()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();
    }

    /// <summary>
    /// Prints a message and waits for a key press.
    /// </summary>
    /// <param name="data">The message to print.</param>
    public static void PrintUntilKeyPressed(string data)
    {
        ClearBuffer();
        Console.WriteLine(data);
        Console.ReadKey(true);
    }

    /// <summary>
    /// Prints a selected text with contrasting colors.
    /// </summary>
    /// <param name="text">The text to be printed.</param>
    public static void PrintSelected(string text)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Prints an issue and a fix recommendation with specified text colors.
    /// </summary>
    /// <param name="issue">The issue message.</param>
    /// <param name="fixRecommendation">The fix recommendation message.</param>
    /// <param name="isStopped">Indicates whether to wait for a key press if true.</param>
    public static void PrintIssue(string issue, string fixRecommendation, bool isStopped = false)
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

    /// <summary>
    /// Prints a help page with ASCII art, help text, and author information.
    /// </summary>
    internal static void PrintHelpPage()
    {
        ClearBuffer();
        PrintWelcome();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(HelpText);
        Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}{Author}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{Environment.NewLine}Press any button...");
        Console.ReadKey(true);
    }
}
