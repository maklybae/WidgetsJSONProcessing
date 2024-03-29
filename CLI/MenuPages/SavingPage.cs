﻿using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;

namespace CLI.MenuPages
{
    /// <summary>
    /// Represents a menu page for saving data with various options.
    /// </summary>
    internal class SavingPage : MenuPage
    {
        private readonly SavingType _type;

        // Private constructor to prevent instantiation without a specified SavingType.
        private SavingPage() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingPage"/> class with a specified SavingType.
        /// </summary>
        /// <param name="type">The SavingType to determine the behavior of the SavingPage.</param>
        public SavingPage(SavingType type)
        {
            _type = type;
            UpdateButtons();
        }

        /// <summary>
        /// Enumeration representing different saving types.
        /// </summary>
        public enum SavingType
        {
            /// <summary>
            /// Save recetly requested data.
            /// </summary>
            SaveCache,
            /// <summary>
            /// Save all data in database.
            /// </summary>
            SaveCurrent
        }

        /// <inheritdoc/>
        public override void UpdateButtons()
        {
            Buttons.Add((SaveToDesktop, new ButtonArgs("Save to the desktop directory")));
            Buttons.Add((SaveToCurrentDirectory, new ButtonArgs($"Save to the current directory: {Environment.CurrentDirectory}")));
            Buttons.Add((SaveToRecentlyOpenedFile, new ButtonArgs($"Save to the recently opened file: {Controller.Request.OriginalFilePath}")));
            Buttons.Add((SaveToCustomPath, new ButtonArgs($"Save to any other folder by full path to the file")));
            Buttons.Add((PrintJSONFormattedData, new ButtonArgs($"Print JSON Formatted data in console")));
            Buttons.Add((MoveToPreviousPage, new ButtonArgs("Move to previus menu page")));
        }

        private void RunSaving(string path)
        {
            try
            {
                // Check for the file extension.
                if (!path.ToLower().EndsWith(".json"))
                    throw new IOException();
                if (!File.Exists(path) || ConsoleDialog.InputOverwriteFile())
                {
                    if (_type == SavingType.SaveCache)
                        Controller.Request.SaveCache(path);
                    else if (_type == SavingType.SaveCurrent)
                        Controller.Request.Save(path);
                    ConsoleOutput.PrintSuccess(true);
                }
            }
            catch (IOException)
            {
                ConsoleOutput.PrintIssue("Unable to save to the file",
                    "Check that the file has .json extension and accessed from your local file system", true);
            }
            catch (ArgumentException e)
            {
                ConsoleOutput.PrintIssue(e.Message, string.Empty, true);
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleOutput.PrintIssue("Unable to open securied files", "Check the properties of the file", true);
            }
        }

        // Below are private methods to manage functionality of program.

        private void SaveToDesktop()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ConsoleInput.InputFilename());
            RunSaving(path);
        }

        private void SaveToCurrentDirectory()
        {
            string path = Path.Combine(Environment.CurrentDirectory, ConsoleInput.InputFilename());
            RunSaving(path);
        }

        private void SaveToRecentlyOpenedFile()
        {
            string path = Path.Combine(Controller.Request.OriginalFilePath);
            RunSaving(path);
        }

        private void SaveToCustomPath()
        {
            string path = ConsoleInput.InputFullPath();
            RunSaving(path);
        }

        private void PrintJSONFormattedData()
        {
            if (_type == SavingType.SaveCache)
                ConsoleOutput.PrintUntilKeyPressed(Controller.Request.GetJsonStringCache());
            else if (_type == SavingType.SaveCurrent)
                ConsoleOutput.PrintUntilKeyPressed(Controller.Request.GetJsonStringData());

        }

        private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
    }
}
