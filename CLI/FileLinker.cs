using CLI.ConsoleIO;

namespace CLI;

/// <summary>
/// Static class responsible for linking and processing data from a JSON file.
/// </summary>
internal static class FileLinker
{
    /// <summary>
    /// Links and processes data from the specified JSON file.
    /// </summary>
    /// <param name="path">The full path to the JSON file.</param>
    public static void LinkFile(string path)
    {
        try
        {
            // Attempt to rebase the processor with the provided file path.
            Controller.Request.RebaseProcessor(path);
            ConsoleOutput.PrintSuccess(true);
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, "Try to select another file", true);
        }
        catch (System.Text.Json.JsonException)
        {
            ConsoleOutput.PrintIssue("Issue with your JSON file",
                "Check the format and try again", true);
        }
        catch (IOException)
        {
            ConsoleOutput.PrintIssue("Unable to open this file", "Check it on the local PC", true);
        }
        catch (UnauthorizedAccessException)
        {
            ConsoleOutput.PrintIssue("Unable to open securied files", "Check the properties of the file", true);
        }
    }
}
