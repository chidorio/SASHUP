using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Splat;
using desktop.Services;

namespace desktop.Views;

public partial class AddEditTeacherWindow : Window
{
    public AddEditTeacherWindow()
    {
        InitializeComponent();
        Locator.Current.GetService<IFilePickerService>().RegisterProvider(TopLevel.GetTopLevel(this));
    }
}