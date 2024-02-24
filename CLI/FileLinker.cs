using CLI.ConsoleIO;

namespace CLI
{
    internal static class FileLinker
    {
        internal static void LinkFile(string path)
        {
            try
            {
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
}
