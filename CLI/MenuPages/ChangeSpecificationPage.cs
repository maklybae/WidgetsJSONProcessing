﻿namespace CLI.MenuPages;
using ButtonArgsClasses;
using CLI.ConsoleIO;

/// <summary>
/// Represents a menu page for changing specifications of a widget.
/// </summary>
internal class ChangeSpecificationPage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeSpecificationPage"/> class.
    /// </summary>
    public ChangeSpecificationPage()
    {
        UpdateButtons();
    }

    /// <inheritdoc/>
    public override void UpdateButtons()
    {
        (int currentWidgetNum, int currentSpecificationNum) =
            (Controller.Selector.WidgetNum, Controller.Selector.SpecificationNum);

        Buttons = new()
        {
            (ChangeName, new PairButtonArgs("Name", Controller.Request.GetSpecificationNameByNums(currentWidgetNum, currentSpecificationNum))),
            (ChangePrice, new PairButtonArgs("Price", Controller.Request.GetSpecificationPriceByNums(currentWidgetNum, currentSpecificationNum).ToString())),
            (ChangeIsCustom, new PairButtonArgs("Is Custom", Controller.Request.GetSpecificationIsCustomByNums(currentWidgetNum, currentSpecificationNum).ToString())),
            (MoveToPreviousPage, new ButtonArgs("Back to menu"))
    };
    }

    // Below are private methods to manage functionality of program.

    private void ChangeName()
    {
        try
        {
            Controller.Request.ChangeSpecificationNameByNums(Controller.Selector.WidgetNum,
                Controller.Selector.SpecificationNum,
                ConsoleDialog.InputStringField("Name"));
            ConsoleOutput.PrintSuccess(true);
            UpdateButtons();
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
        }
    }


    // Restriction by task description in .pdf file
    private void ChangePrice() =>
        ConsoleOutput.PrintIssue("It is restricted to change specification's price", "Change widget price instead", true);

    private void ChangeIsCustom()
    {
        Controller.Request.ChangeSpecificationIsCustomByNums(Controller.Selector.WidgetNum,
            Controller.Selector.SpecificationNum,
            ConsoleDialog.InputBooleanField("IsAvailable"));
        ConsoleOutput.PrintSuccess();
        UpdateButtons();
    }

    private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
}
