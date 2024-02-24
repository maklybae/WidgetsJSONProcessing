using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages
{
    internal class SortingPage : MenuPage
    {
        private readonly List<string> _selectedFields = new();

        public SortingPage()
        {
            UpdateButtons();
        }
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

        private void ChooseField()
        {
            if (_selectedFields.Contains(Buttons[CurrentOption].args.Name))
                _selectedFields.Remove(Buttons[CurrentOption].args.Name);
            else
                _selectedFields.Add(Buttons[CurrentOption].args.Name);
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
            TablePrinter.ShowTableView((Controller.Request.AltenativeFieldsNames, DataConverter.ConvertToJaggedArray(sorted)));
            Controller.AddMenuPageToStack(new SavingPage(SavingPage.SavingType.SaveCache));
        }

        private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
    }
}
