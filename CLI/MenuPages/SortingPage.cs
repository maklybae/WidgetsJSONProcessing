using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages;

/// <summary>
/// Represents a menu page for sorting data based on selected fields.
/// </summary>
internal class SortingPage : MenuPage
{
    private readonly List<string> _selectedFields = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="SortingPage"/> class.
    /// </summary>
    public SortingPage()
    {
        UpdateButtons();
    }

    /// <inheritdoc/>
    public override void UpdateButtons()
    {
        Buttons.Clear();

        Buttons.Add((ChooseField, new NumberedButtonArgs(_selectedFields.IndexOf("widgetId") + 1, "widgetId")));
        Buttons.Add((ChooseField, new NumberedButtonArgs(_selectedFields.IndexOf("name") + 1, "name")));
        Buttons.Add((ChooseField, new NumberedButtonArgs(_selectedFields.IndexOf("quantity") + 1, "quantity")));
        Buttons.Add((ChooseField, new NumberedButtonArgs(_selectedFields.IndexOf("price") + 1, "price")));
        Buttons.Add((ChooseField, new NumberedButtonArgs(_selectedFields.IndexOf("isAvailable") + 1, "isAvailable")));
        Buttons.Add((ChooseField, new NumberedButtonArgs(_selectedFields.IndexOf("manufactureDate") + 1, "manufactureDate")));

        Buttons.Add((RunSortingBySelectedFields, new ButtonArgs("Sort by chosen fields (all requests will be taken into account)")));
        Buttons.Add((MoveToPreviousPage, new ButtonArgs("Back to menu")));
    }

    // Below are private methods to manage functionality of program.

    private void ChooseField()
    {
        // Add/remove new item to selected fields.
        if (_selectedFields.Contains(Buttons[CurrentOption].args.Name))
            _selectedFields.Remove(Buttons[CurrentOption].args.Name);
        else
            _selectedFields.Add(Buttons[CurrentOption].args.Name);

        // Re-numerate fields.
        UpdateButtons();
    }

    private void RunSortingBySelectedFields()
    {
        List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
            List<(string specName, double specPrice, bool isCustom)> specifications)> sorted;

        try
        {
            var sortingOptions = ConsoleDialog.InputSortingOptionsDictionary(_selectedFields);
            sorted = Controller.Request.Sort(_selectedFields, sortingOptions);
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
            return;
        }

        // Printing and saving page added.
        TablePrinter.ShowTableView((Controller.Request.AltenativeFieldsNames, DataConverter.ConvertToJaggedArray(sorted)));
        Controller.AddMenuPageToStack(new SavingPage(SavingPage.SavingType.SaveCache));
    }

    private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
}
