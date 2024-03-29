﻿using CLI.ButtonArgsClasses;
using CLI.ConsoleIO;
using System.Security;

namespace CLI.MenuPages;

/// <summary>
/// Represents a file manager menu that allows navigation through directories and opening JSON files.
/// </summary>
internal class FileManagerPage : MenuPage
{
    // The current directory being displayed
    private DirectoryInfo _currentDirectory = new(Environment.CurrentDirectory);

    /// <summary>
    /// Initializes a new instance of the <see cref="FileManagerPage"/> class and updates the option list.
    /// </summary>
    public FileManagerPage() => UpdateButtons();

    /// <inheritdoc/>
    public override void UpdateButtons()
    {
        Buttons.Clear();

        UpdateDrives();
        UpdateParentDirectory();
        UpdateDirectories();
        UpdateJSONFiles();

        Buttons.Add((MoveToPreviousPage, new ButtonArgs("Back to menu")));

        CurrentOption = 0;
    }

    // Below are private methods to update list of dirs/files in the menu list.

    private void UpdateJSONFiles()
    {
        // Add JSON files in the current directory as options.
        Buttons.Add((null, new ButtonArgs("==JSON Files in the current directory==")));
        foreach (var file in _currentDirectory.EnumerateFiles("*.json"))
        {
            Buttons.Add((OpenFile, new ButtonArgs(file.FullName)));
        }
        Buttons.Add((null, new ButtonArgs(string.Empty)));
    }

    private void UpdateDirectories()
    {
        // Add directories as options.
        Buttons.Add((null, new ButtonArgs("==Directories in the current directory==")));
        foreach (var directory in _currentDirectory.EnumerateDirectories())
        {
            Buttons.Add((OpenDirectory, new ButtonArgs(directory.FullName)));
        }
        Buttons.Add((null, new ButtonArgs(string.Empty)));
    }

    private void UpdateParentDirectory()
    {
        // Add the parent directory as an option if it exists and is not a drive.
        if (_currentDirectory.Parent != null &&
            !Array.ConvertAll(DriveInfo.GetDrives(), drive => drive.Name).Contains(_currentDirectory.Parent.FullName))
        {
            Buttons.Add((null, new ButtonArgs("==Parent directory==")));
            Buttons.Add((OpenDirectory, new ButtonArgs(_currentDirectory.Parent.FullName)));
            Buttons.Add((null, new ButtonArgs(string.Empty)));
        }
    }

    private void UpdateDrives()
    {
        // Add drives as options.
        Buttons.Add((null, new ButtonArgs("==Drives==")));
        var drives = Array.ConvertAll(DriveInfo.GetDrives(), drive => drive.Name);
        foreach (var drive in drives)
        {
            Buttons.Add((OpenDirectory, new ButtonArgs(drive)));
        }
        Buttons.Add((null, new ButtonArgs(string.Empty)));
    }

    // Below are private methods to manage functionality of program.

    private void OpenDirectory()
    {
        try
        {
            var tmpDirectory = new DirectoryInfo(Buttons[CurrentOption].args.Name);
            if (!tmpDirectory.Exists)
            {
                throw new DirectoryNotFoundException();
            }
            _currentDirectory = tmpDirectory;
        }
        catch (SecurityException)
        {
            ConsoleOutput.PrintIssue("Inpossible to open this directory due to security settings",
                "Check your settings in properties of the directory", true);
        }
        catch (ArgumentException)
        {
            ConsoleOutput.PrintIssue("Something wrong with your directory", "Move to another directory", true);
        }
        catch (DirectoryNotFoundException)
        {
            ConsoleOutput.PrintIssue("Directory not found", "Create it or move to another directory", true);
        }
        UpdateButtons();
    }

    private void OpenFile()
    {
        FileLinker.LinkFile(Buttons[CurrentOption].args.Name);
        Controller.PopMenuPageFromStack();
    }

    private void MoveToPreviousPage() => Controller.PopMenuPageFromStack();
}
