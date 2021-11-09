using Ookii.Dialogs.Wpf;

namespace AddonObfuscator.Wpf.Services;

public class FolderBrowserDialog
{
    public string? Open()
    {
        var dialog = new VistaFolderBrowserDialog
        {
            Description = "Select a Folder.",
            UseDescriptionForTitle = true
        };

        return dialog.ShowDialog() == true ? dialog.SelectedPath : null;
    }
}

