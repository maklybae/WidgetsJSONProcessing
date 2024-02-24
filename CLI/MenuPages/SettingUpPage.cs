using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages;

/// <summary>
/// Represents a menu page for setting up the application, including inputting JSON data.
/// </summary>
internal class SettingUpPage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingUpPage"/> class.
    /// </summary>
    public SettingUpPage() => UpdateButtons();

    /// <inheritdoc/>
    public override void UpdateButtons()
    {
        Buttons = new()
        {
            (InputFromConsole, new ButtonArgs("Input path to JSON file")),
            (InputFromManager, new ButtonArgs("Open FileManager to select JSON file")),
            (MoveToMainMenu, new ButtonArgs("Move to main menu with all possible data processing functions"))
        };
    }

    // Below are private methods to manage functionality of program.

    private void InputFromConsole() =>
        FileLinker.LinkFile(ConsoleInput.InputFullPath());

    private void InputFromManager() => Controller.AddMenuPageToStack(new FileManagerPage());

    private void MoveToMainMenu()
    {
        // Do not run the main menu before requester is not ready.
        if (Controller.Request.IsReady)
            Controller.PopMenuPageFromStack();
        else
            ConsoleOutput.PrintIssue("Unable to move to functional menu without opening the file",
                "Load the file or enter data to console in setting up menu", true);
    }
}
