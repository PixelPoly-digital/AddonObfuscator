using HandyControl.Controls;

namespace AddonObfuscator.Wpf.Services;

public static class MessageDialog
{
    public static void Success(string message) => MessageBox.Success(message, "Add-on Obfuscator");
}

