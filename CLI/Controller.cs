namespace CLI;

using CLI.ConsoleIO;
using CLI.MenuPages;
using Model;


internal static class Controller
{
    private static RequestProcessor _request = new();
    private static WidgetSpecificationSelector _selector = new();
    
    private static Stack<MenuPage> MenuPagesStack { get; }

    /// <summary>
    /// Static constructor initializes the menu stack and adds initial menu pages.
    /// </summary>
    static Controller()
    {
        MenuPagesStack = new Stack<MenuPage>();
        MenuPagesStack.Push(new HomePage());
        MenuPagesStack.Push(new SettingUpPage());
    }

    public static RequestProcessor Request => _request;
    public static WidgetSpecificationSelector Selector => _selector;

    /// <summary>
    /// Method to clear all data usage properties.
    /// </summary>
    private static void ClearAll()
    {
        MenuPagesStack.Clear();
        MenuPagesStack.Push(new HomePage());
        MenuPagesStack.Push(new SettingUpPage());
        _request = new();
    }

    public static void AddWidgetNumToSelector(int widgetNum) =>
        _selector.WidgetNum = widgetNum;

    public static void AddSpecificationNumToSelector(int specificationNum) =>
        _selector.SpecificationNum = specificationNum;

    public static void ClearSelector() => _selector = new();

    /// <summary>
    /// Method to add a new menu page to the stack.
    /// </summary>
    /// <param name="toAdd">Menu page to add to the stack.</param>
    public static void AddMenuPageToStack(MenuPage toAdd) => MenuPagesStack.Push(toAdd);

    /// <summary>
    /// Method to remove the top menu page from the stack.
    /// </summary>
    public static void PopMenuPageFromStack() => MenuPagesStack.Pop();

    public static MenuPage PeekMenuPageFromStack() => MenuPagesStack.Peek();

    /// <summary>
    /// Method to display the menu and handle user interaction.
    /// </summary>
    public static void ShowMenu()
    {
        // Hide the cursor for a cleaner interface.
        Console.CursorVisible = false;

        // Print the initial help page with instructions.
        ConsoleOutput.PrintHelpPage();

        // Continue showing the menu until the application exits.
        while (true)
        {
            try
            {
                // Get the current menu page from the top of the stack.
                MenuPage currentPage = MenuPagesStack.Peek();
                currentPage.DrawPage();

                // Handle user input based on arrow keys and Enter key.
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        currentPage.ExecuteCurrentOption();
                        break;
                    case ConsoleKey.UpArrow:
                        currentPage.CurrentOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        currentPage.CurrentOption++;
                        break;
                }
            }
            // Proceeding unexpected behaviour of the program.
            catch (Exception ex)
            {
                ConsoleOutput.PrintIssue($"Unexpected issue: {ex.Message}", "Try again", true);
                ClearAll();
            }
        }
    }
}

