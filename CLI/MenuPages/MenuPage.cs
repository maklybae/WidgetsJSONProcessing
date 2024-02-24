using CLI.ButtonArgsClasses;

namespace CLI.MenuPages;

internal abstract class MenuPage
{
    private List<(Action? buttonAction, ButtonArgs args)> _buttons;

    // The index of the currently selected option.
    private int _currentOption = 0;

    /// <summary>
    /// Gets or sets the list of options for this menu page.
    /// </summary>
    protected List<(Action? buttonAction, ButtonArgs args)> Buttons
    {
        get { return _buttons; }
        set { _buttons = value; }
    }

    /// <summary>
    /// Gets or sets the index of the currently selected option, handling circular navigation.
    /// </summary>  
    internal int CurrentOption
    {
        get { return _currentOption; }
        set
        {
            // Ensure the index stays within the bounds of the options list (circular navigation).
            _currentOption = (value % Buttons.Count + Buttons.Count) % Buttons.Count;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuPage"/> class with an empty list of options.
    /// </summary>
    protected MenuPage()
    {
        _buttons = new();
    }

    internal abstract void UpdateButtons();

    /// <summary>
    /// Draws the menu page on the console, highlighting the currently selected option.
    /// </summary>
    internal virtual void DrawPage()
    {
        ConsoleOutput.ClearBuffer();
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i == CurrentOption)
                ConsoleOutput.PrintSelected(Buttons[i].args.ToString());
            else
                Console.WriteLine(Buttons[i].args.ToString());
        }
    }

    /// <summary>
    /// Executes the action associated with the currently selected option.
    /// </summary>
    internal void ExecuteCurrentOption()
    {
        Buttons[CurrentOption].buttonAction?.Invoke();
    }
}
