using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using desktop.Models;
using ReactiveUI;
using desktop.Tools;

namespace desktop;

public class OwnerParameters : ReactiveObject
{
  private int? _pageNumber = 1;
  private int? _sizePage = 5;
  private string? _searchString;
  private string? _filterParameters;
  private string? _sortsParameters;
  public int PageNumber
  {
    get => _pageNumber.GetValueOrDefault(1);
    set => this.RaiseAndSetIfChanged(ref _pageNumber, value);
  }
  public int SizePage
  {
    get => _sizePage.GetValueOrDefault(5);
    set => this.RaiseAndSetIfChanged(ref _sizePage, value);
  }
  public string SearchString
  {
    get=> _searchString;
    set => this.RaiseAndSetIfChanged(ref _searchString, value);
  }
  public string Filters
  {
    get => _filterParameters;
    set => this.RaiseAndSetIfChanged(ref _filterParameters, value);
  }
  public string Sorts
  {
    get => _sortsParameters;
    set => this.RaiseAndSetIfChanged(ref _sortsParameters,value);
  }
}
