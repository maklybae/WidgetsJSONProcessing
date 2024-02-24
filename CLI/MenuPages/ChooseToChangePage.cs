namespace CLI.MenuPages;
using CLI.ButtonArgsClasses;

/// <summary>
/// Represents a menu page for choosing between widget and specification data to change.
/// </summary>
internal class ChooseToChangePage : MenuPage
{
    private readonly DataTypeToChoose _option;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChooseToChangePage"/> class.
    /// </summary>
    /// <param name="option">The data type to choose (Widget or Specification).</param>
    public ChooseToChangePage(DataTypeToChoose option)
    {
        _option = option;
        UpdateButtons();
    }

    /// <inheritdoc/>
    public override void UpdateButtons()
    {
        if (_option == DataTypeToChoose.ChooseWidget)
        {
            foreach (var (id, name) in Controller.Request.WidgetsIdNamesPairs)
            {
                Buttons.Add((Choose, new PairButtonArgs(id, name)));
            }
        }
        else if (_option == DataTypeToChoose.ChooseSpecification)
        {
            foreach (var specName in Controller.Request.GetSpecificationsByWidgetNum(
                Controller.Selector.WidgetNum))
            {
                Buttons.Add((Choose, new ButtonArgs(specName)));
            }
        }

        Buttons.Add((null, new ButtonArgs(string.Empty)));
        Buttons.Add((MoveToPreviousPage, new ButtonArgs("Back to menu")));
    }

    /// <summary>
    /// Enum representing the data type to choose (Widget or Specification).
    /// </summary>
    public enum DataTypeToChoose
    {
        ChooseWidget,
        ChooseSpecification
    }

    // Below are private methods to manage functionality of program.

    private void MoveToPreviousPage()
    {
        Controller.PopMenuPageFromStack();
        Controller.PopMenuPageFromStack();
    }

    private void Choose()
    {
        if (_option == DataTypeToChoose.ChooseWidget)
        {
            Controller.AddWidgetNumToSelector(CurrentOption);
        }
        else if (_option == DataTypeToChoose.ChooseSpecification)
        {
            Controller.AddSpecificationNumToSelector(CurrentOption);
        }
        Controller.PopMenuPageFromStack();

        // Show information immediately in changing menu.
        Controller.PeekMenuPageFromStack().UpdateButtons();
    }
}