namespace CLI.MenuPages;
using CLI.ButtonArgsClasses;

internal class ChooseToChangePage : MenuPage
{
    private readonly DataTypeToChoose _option;

    public ChooseToChangePage(DataTypeToChoose option)
    {
        _option = option;
        UpdateButtons();
    }

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

    public enum DataTypeToChoose
    {
        ChooseWidget,
        ChooseSpecification
    }

    /// <summary>
    /// Moves to the previous menu by popping the current menu page from the stack.
    /// </summary>
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
        Controller.PeekMenuPageFromStack().UpdateButtons();
    }
}