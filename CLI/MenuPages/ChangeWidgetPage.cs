﻿using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages;

internal class ChangeWidgetPage : MenuPage
{
    internal ChangeWidgetPage()
    {
        UpdateButtons();
    }

    internal override void UpdateButtons()
    {
        int currentWidgetNum = Controller.Selector.WidgetNum;
        Buttons = new()
        {
            (ChangeId, new PairButtonArgs("Id", Controller.Request.GetIdByWidgetNum(currentWidgetNum).ToString())),
            (ChangeName, new PairButtonArgs("Name", Controller.Request.GetNameByWidgetNum(currentWidgetNum))),
            (ChangeQuantity, new PairButtonArgs("Quantity", Controller.Request.GetQuantityByWidgetNum(currentWidgetNum).ToString())),
            (ChangePrice, new PairButtonArgs("Price", Controller.Request.GetPriceByWidgetNum(currentWidgetNum).ToString())),
            (ChangeIsAvailable, new PairButtonArgs("Is Available", Controller.Request.GetIsAvailableByWidgetNum(currentWidgetNum).ToString())),
            (ChangeManufactureDate, new PairButtonArgs("Manufacture Date", Controller.Request.GetManufactureDateByWidgetNum(currentWidgetNum).ToString())),
            (ChangeSpecifications, new PairButtonArgs("Specifications", Controller.Request.GetSpecificationsCountByWidgetNum(currentWidgetNum).ToString())),
            (MoveToPreviousPage, new ButtonArgs("Back to menu"))
    };
    }

    private void ChangeId() =>
        ConsoleOutput.PrintIssue("It is restricted to change id", string.Empty, true);

    private void ChangeName()
    {
        try
        {
            Controller.Request.ChangeNameByWidgetNum(Controller.Selector.WidgetNum,
                ConsoleDialog.InputStringField("Name"));
            ConsoleOutput.PrintSuccess(true);
            UpdateButtons();
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
        }
    }

    private void ChangeQuantity()
    {
        try
        {
            Controller.Request.ChangeQuantityByWidgetNum(Controller.Selector.WidgetNum,
                ConsoleDialog.InputIntegerField("Quantity"));
            ConsoleOutput.PrintSuccess(true);
            UpdateButtons();
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
        }
    }

    private void ChangePrice()
    {
        try
        {
            Controller.Request.ChangePriceByWidgetNum(Controller.Selector.WidgetNum,
                ConsoleDialog.InputIntegerField("Price"));
            ConsoleOutput.PrintSuccess(true);
            UpdateButtons();
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
        }
    }

    private void ChangeIsAvailable()
    {
        Controller.Request.ChangeIsAvailaleByWidgetNum(Controller.Selector.WidgetNum,
            ConsoleDialog.InputBooleanField("IsAvailable"));
        ConsoleOutput.PrintSuccess();
        UpdateButtons();
    }

    private void ChangeManufactureDate()
    {
        try
        {
            Controller.Request.ChangeManufactureDateByWidgetNum(Controller.Selector.WidgetNum,
                ConsoleDialog.InputDateTimeField("ManufactureDate"));
            ConsoleOutput.PrintSuccess(true);
            UpdateButtons();
        }
        catch (ArgumentException e)
        {
            ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
        }
    }

    private void ChangeSpecifications()
    {
        Controller.AddMenuPageToStack(new ChangeSpecificationPage());
        Controller.AddMenuPageToStack(new ChooseToChangePage(ChooseToChangePage.DataTypeToChoose.ChooseSpecification));
    }

    private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
}