using System;
using System.Windows;
using System.Windows.Controls;

namespace AddonObfuscator.Wpf.Pages.UserControls;

/// <summary>
/// Interaction logic for LocatePath.xaml
/// </summary>
public partial class LocatePath : UserControl
{
    public event EventHandler? Click;

    public LocatePath()
    {
        InitializeComponent();
    }

    public object Path { get => GetValue(PathProperty); set => SetValue(PathProperty, value); }

    public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(object), typeof(UserControl));

    public object Title { get => GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(UserControl));

    private void LocateButtonClick(object sender, RoutedEventArgs eventArgs) => Click?.Invoke(this, EventArgs.Empty);
}

