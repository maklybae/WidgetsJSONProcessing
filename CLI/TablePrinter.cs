using Microsoft.VisualBasic;
using System.Net.NetworkInformation;

namespace CLI;

internal static class TablePrinter
{
    private const int MaxColumnsInConsole = 3;

    /// <summary>
    /// Shows a table view with auto-width console output.
    /// </summary>
    /// <param name="tableData">Pair of headings and data for the table.</param>
    /// <param name="columnToExclude">Index of the column to exclude from printing if the value is null.</param>
    private static void PrintTableView((string[] headings, string[][] data) tableData, int currentWidth, int first, int last)
    {
        int columnWidth = (currentWidth - last - first + 1) / (last - first);
        ConsoleOutput.ClearBuffer();

        if (columnWidth <= 3)
        {
            ConsoleOutput.PrintIssue("Unable to print with this size", "Zoom out the console window");
            return;
        }

        // Print headings.
        Console.WriteLine(string.Join('|', Array.ConvertAll(tableData.headings[first..last],
            s => s.Length <= columnWidth ? s.PadRight(columnWidth) : s[..(columnWidth - 3)] + "...")));
        Console.WriteLine(new string('—', currentWidth));

        // Print data.
        foreach (var dataRow in tableData.data)
        {
            Console.WriteLine(string.Join('|', Array.ConvertAll(dataRow[first..last],
                s => s.Length <= columnWidth ? s.PadRight(columnWidth) : s[..(columnWidth - 3)] + "...")));
        }

        // Print statistics.
        Console.WriteLine(new string('—', currentWidth));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total data rows: {tableData.data.Length}");
        Console.WriteLine($"Use Left-Right arrows to check other columns. Press any other button to move from this page.");
        Console.WriteLine($"To view more characters in a column, zoom in or zoom out the console window or chage the console font size");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Press any button...");
    }

    /// <summary>
    /// Displays a table view with options for partial printing and navigation.
    /// </summary>
    /// <param name="tableData">Pair of headings and data for the table.</param>
    /// <param name="option">Printing options for the table.</param>
    /// <param name="count">Number of rows to print for partial printing options.</param>
    internal static void ShowTableView((string[] headings, string[][] data) tableData)
    {
        // Estimate
        int currentFirstColumn = 0;
        int countColumns = tableData.headings.Length;
        int currentLastColumn = Math.Min(countColumns, MaxColumnsInConsole);

        int prevWidth = Console.WindowWidth;
        PrintTableView(tableData, prevWidth, currentFirstColumn, currentLastColumn);

        // Continuously check for changes in the console window width to update the displayed table.
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                if (prevWidth != Console.WindowWidth)
                {
                    prevWidth = Console.WindowWidth;
                    PrintTableView(tableData, prevWidth, currentFirstColumn, currentLastColumn);
                }
            }
            else
            {
                ConsoleKey ck = Console.ReadKey(true).Key;
                if (ck == ConsoleKey.LeftArrow)
                {
                    if (currentFirstColumn - 1 >= 0)
                    {
                        currentFirstColumn--;
                        currentLastColumn--;
                        PrintTableView(tableData, prevWidth, currentFirstColumn, currentLastColumn);
                    }
                }
                else if (ck == ConsoleKey.RightArrow)
                {
                    if (currentLastColumn + 1 <= countColumns)
                    {
                        currentFirstColumn++;
                        currentLastColumn++;
                        PrintTableView(tableData, prevWidth, currentFirstColumn, currentLastColumn);
                    }
                }
                else
                {
                    break;
                }
            }
            Thread.Sleep(5);
        }
    }
}
