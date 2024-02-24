﻿using CLI.ButtonArgsClasses;
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

    private void MoveToChangeDataMenu()
    {
        Controller.ClearSelector();
        Controller.AddMenuPageToStack(new ChangeWidgetPage());
        Controller.AddMenuPageToStack(new ChooseToChangePage(ChooseToChangePage.DataTypeToChoose.ChooseWidget));
    }

    /// <summary>
    /// Moves to the data output menu by adding it to the menu stack.
    /// </summary>
    private void ShowCurrentTableData()
    {
        TablePrinter.ShowTableView((Controller.Request.AltenativeFieldsNames, DataConverter.ConvertToJaggedArray(Controller.Request.GetAllItems())));
    }

    /// <summary>
    /// Moves to the sorting menu by adding it to the menu stack.
    /// </summary>
    private void MoveToSortingMenu() => Controller.AddMenuPageToStack(new SortingPage());

    private void MoveToSavingMenu() => Controller.AddMenuPageToStack(new SavingPage(SavingPage.SavingType.SaveCurrent));

    /// <summary>
    /// Moves to the configuration setup menu by adding it to the menu stack.
    /// </summary>
    private void MoveToSettinUpMenu() => Controller.AddMenuPageToStack(new SettingUpPage());

    /// <summary>
    /// Prints the help page to the console.
    /// </summary>
    private void MoveToHelp() => ConsoleOutput.PrintHelpPage();

    /// <summary>
    /// Exits the program by terminating the environment.
    /// </summary>
    private void Exit() => Environment.Exit(0);
}