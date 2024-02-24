using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages;

/// <summary>
/// Represents the home page menu with various options for data manipulation and program settings.
/// </summary>
internal class HomePage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomePage"/> class and sets up the initial options.
    /// </summary>
    public HomePage() =>
        UpdateButtons();

    // <inheritdoc/>
    public override void UpdateButtons()
    {
        Buttons = new()
        {
            (MoveToChangeDataMenu, new ButtonArgs("Change Data")),
            (ShowCurrentTableData, new ButtonArgs("Show data in table-view")),
            (MoveToSortingMenu, new ButtonArgs("Sorting")),
            (MoveToSavingMenu, new ButtonArgs("Saving")),
            (MoveToSettinUpMenu, new ButtonArgs("Set up configuration")),
            (MoveToHelp, new ButtonArgs("Help")),
            (Exit, new ButtonArgs("Exit"))
        };
    }

    // Below are private methods to manage functionality of program. 

    private void MoveToChangeDataMenu()
    {
        Controller.ClearSelector();
        Controller.AddMenuPageToStack(new ChangeWidgetPage());
        Controller.AddMenuPageToStack(new ChooseToChangePage(ChooseToChangePage.DataTypeToChoose.ChooseWidget));
    }

    private void ShowCurrentTableData()
    {
        TablePrinter.ShowTableView((Controller.Request.AltenativeFieldsNames, DataConverter.ConvertToJaggedArray(Controller.Request.GetAllItems())));
    }

    private void MoveToSortingMenu() => Controller.AddMenuPageToStack(new SortingPage());

    private void MoveToSavingMenu() => Controller.AddMenuPageToStack(new SavingPage(SavingPage.SavingType.SaveCurrent));

    private void MoveToSettinUpMenu() => Controller.AddMenuPageToStack(new SettingUpPage());

    private void MoveToHelp() => ConsoleOutput.PrintHelpPage();

    private void Exit() => Environment.Exit(0);
}