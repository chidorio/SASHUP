using System;
using System.Collections.Generic;
using desktop.ViewModels;

namespace desktop.ViewModels;

public class Bundle
{
  public ViewModelBase OwnerViewModel {get; private set;}
  private Dictionary<string,object> _dictionary;
  public Bundle(ViewModelBase ownerViewModel)
  {
    OwnerViewModel = ownerViewModel;
    _dictionary = new Dictionary<string,object>();
  }
  public bool AddNewParameter(string key, object value) => _dictionary.TryAdd(key, value);
  public bool RemoveParameter(string key) => _dictionary.Remove(key);
  public object? GetParameter(string key) => _dictionary.GetValueOrDefault(key);
}
