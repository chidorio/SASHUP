using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using desktop.ViewModels;
using Splat;
using desktop.Services;
using DocumentFormat.OpenXml.Presentation;

namespace desktop.Views;

public partial class ReportView : UserControl
{
    public ReportView()
    {
        InitializeComponent();
        AttachedToVisualTree += OnAttachedToVisualTree;
    }
    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        Locator.Current.GetService<IFilePickerService>().RegisterProvider(TopLevel.GetTopLevel(this));
    }
}