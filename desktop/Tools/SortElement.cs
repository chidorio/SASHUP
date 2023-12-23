using System.Collections.ObjectModel;
using desktop.Models;
using ReactiveUI;

namespace desktop.Tools;

public class SortElement : ReactiveObject
{
  public bool _isSelected;
  public Sort Sort {get;set;}
  public string NameSort {get;set;}
  public bool IsSelected
  {
    get => _isSelected;
    set => this.RaiseAndSetIfChanged(ref _isSelected,value);
  }
}
