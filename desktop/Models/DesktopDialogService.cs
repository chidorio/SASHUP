using System;
using System.Threading.Tasks;
using desktop.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace desktop;

public class DesktopDialogService : IDialogService
{
  public async Task<IDialogService.DialogResult> ShowDialog(string title, string text, IDialogService.DialogType dialogType)
  {
    var box = dialogType == IDialogService.DialogType.Standard
      ? MessageBoxManager.GetMessageBoxStandard(title,text)
      : MessageBoxManager.GetMessageBoxStandard(title,text, ButtonEnum.YesNo);
    return (IDialogService.DialogResult)await box.ShowAsync();
  }
}
