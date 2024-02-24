using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages;

internal class SettingUpPage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingUpPage"/> class.
    /// </summary>
    public SettingUpPage() => UpdateButtons();

    public override void UpdateButtons()
    {
        Buttons = new()
        {
            (InputFromConsole, new ButtonArgs("Input path to JSON file")),
            (InputFromManager, new ButtonArgs("Open FileManager to select JSON file")),
            (MoveToMainMenu, new ButtonArgs("Move to main menu with all possible data processing functions"))
        };
    }

    private void InputFromConsole() =>
        FileLinker.LinkFile(ConsoleInput.InputFullPath());


    ///// <summary>
    ///// Handles inputting JSON data from the console.
    ///// </summary>
    //private void InputJSONDataFromConsole() => ConsoleInput.InputJSONDataFromConsoleDialog();

    /// <summary>
    /// Handles inputting JSON data from a local file using the file manager menu.
    /// </summary>
    private void InputFromManager() => Controller.AddMenuPageToStack(new FileManagerPage());

    /// <summary>
    /// Moves to the main menu with all possible data processing functions.
    /// </summary>
    private void MoveToMainMenu()
    {
        if (Controller.Request.IsReady)
            Controller.PopMenuPageFromStack();
        else
            ConsoleOutput.PrintIssue("Unable to move to functional menu without opening the file",
                "Load the file or enter data to console in setting up menu", true);
    }
}
