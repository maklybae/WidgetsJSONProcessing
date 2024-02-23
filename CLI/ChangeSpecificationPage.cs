namespace CLI;

internal class ChangeSpecificationPage : MenuPage
{
    internal ChangeSpecificationPage()
    {
        UpdateButtons();
    }

    internal override void UpdateButtons()
    {
        (int currentWidgetNum, int currentSpecificationNum) =
            (Controller.Selector.WidgetNum, Controller.Selector.SpecificationNum);
        
        Buttons = new()
        {
            (ChangeName, new PairButtonArgs("Name", Controller.Request.GetSpecificationNameByNums(currentWidgetNum, currentSpecificationNum))),
            (ChangePrice, new PairButtonArgs("Price", Controller.Request.GetSpecificationPriceByNums(currentWidgetNum, currentSpecificationNum).ToString())),
            (ChangeIsAvailable, new PairButtonArgs("Is Custom", Controller.Request.GetSpecificationIsCustomByNums(currentWidgetNum, currentSpecificationNum).ToString())),
            (MoveToPreviousPage, new ButtonArgs("Back to menu"))
    };
    }

    private void ChangeName() { }

    private void ChangePrice() =>
        ConsoleOutput.PrintIssue("It is restricted to change specification's price", "Change widget price instead", true);

    private void ChangeIsAvailable() { }

    private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
}
