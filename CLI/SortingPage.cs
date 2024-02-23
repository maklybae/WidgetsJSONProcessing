using System.Xml.Linq;

namespace CLI
{
    internal class SortingPage : MenuPage
    {
        private List<string> _selectedFields = new();

        internal SortingPage()
        {
            UpdateButtons();
        }
        internal override void UpdateButtons()
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
            var sortingOptions = ConsoleDialog.InputSortingOptionsDictionary(_selectedFields);
            var sortedCourses = Controller.Request.Sort(_selectedFields, sortingOptions);
            TablePrinter.ShowTableView((Controller.Request.AltenativeFieldsNames, DataConverter.ConvertToJaggedArray(sortedCourses)));
        }

        private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
    }
}
