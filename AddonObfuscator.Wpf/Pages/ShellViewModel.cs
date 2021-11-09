using AddonObfuscator.Core;
using AddonObfuscator.Wpf.Services;
using Stylet;
using System.Diagnostics;

namespace AddonObfuscator.Wpf.Pages;

public class ShellViewModel : Screen
{
    private bool isDefaultChecked = true;
    public bool IsDefaultChecked { get => isDefaultChecked; set => SetAndNotify(ref isDefaultChecked, value); }

    private bool isInvalidInput;
    public bool IsInvalidInput { get => isInvalidInput; set => SetAndNotify(ref isInvalidInput, value); }

    private string sourcePath = "";
    public string SourcePath { get => sourcePath; set => SetAndNotify(ref sourcePath, value); }

    private string targetPath = "";
    public string TargetPath { get => targetPath; set => SetAndNotify(ref targetPath, value); }

    private readonly FolderBrowserDialog folderBrowserDialog;

    public ShellViewModel(FolderBrowserDialog folderBrowserDialog) => this.folderBrowserDialog = folderBrowserDialog;

    // TODO: Maybe make this asynchronous.
    public void RunObfuscator()
    {
        if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(targetPath))
        {
            IsInvalidInput = true;
            return;
        }
        IsInvalidInput = false;

        var formatting = IsDefaultChecked ? 0 : 1;

        var watch = new Stopwatch();
        watch.Start();
        new Obfuscator(SourcePath, TargetPath, (Formatting)formatting).Run();
        watch.Stop();

        MessageDialog.Success($"Finished. Process took {watch.ElapsedMilliseconds} milisecond(s)");
    }

    public void LocateSourcePath()
    {
        var path = folderBrowserDialog.Open();
        if (path is not null)
            SourcePath = path;
    }

    public void LocateTargetPath()
    {
        var path = folderBrowserDialog.Open();
        if (path is not null)
            TargetPath = path;
    }
}

