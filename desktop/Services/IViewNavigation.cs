using System;
using desktop.ViewModels;

namespace desktop;

public interface IViewNavigation
{
    public void GoTo<VM>(Bundle bundle = null) where VM: ViewModelBase;
    public void GoToAndCloseCurrent<VM>(ViewModelBase currentViewModel, Bundle bundle = null) where VM:ViewModelBase;
    public void GoToAndCloseOthers<VM>(Bundle bundle = null) where VM:ViewModelBase;
    public void Close(ViewModelBase viewModel);
}
