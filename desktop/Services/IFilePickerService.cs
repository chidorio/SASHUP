using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Microsoft.Extensions.Logging;

namespace desktop.Services;

public interface IFilePickerService
{
  public enum Filter
  {
    PngImage,
    Docx
  }
  public void RegisterProvider(object provider);
  public Task<Stream> OpenFile(Filter filter);

  public Task<Uri> SaveFile(Filter filter);
    
}
public class FilePickerservice : IFilePickerService
{
  private TopLevel? _topLevel;
  public static FilePickerFileType Doc {get;} = new("Docx")
  {
    Patterns = new [] { "*.docx"},
    AppleUniformTypeIdentifiers = new [] { "public.document"},
    MimeTypes = new [] {"document/*"}
  };
  public async Task<Stream> OpenFile(IFilePickerService.Filter filter)
  {
    IReadOnlyList<IStorageFile> files = null;
    if(filter == IFilePickerService.Filter.PngImage)
    {
      files = await _topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
      {
        Title = "Выбор файла",
        AllowMultiple = false,
        FileTypeFilter = new[] {FilePickerFileTypes.ImagePng}
      });
    }
    if (files != null && files.Count >= 1)
    {
      return await files[0].OpenReadAsync();
    }
    return null;
  }

  public async Task<Uri> SaveFile(IFilePickerService.Filter filter)
  {
    IStorageFile? storageFile = null;
    if(filter == IFilePickerService.Filter.Docx)
    {
      storageFile = await _topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
      {
        Title = "Сохранять файл",
        FileTypeChoices = new []{Doc}
      });
    }
    if (storageFile != null)
      return storageFile.Path;
    return null;
  }
  public void RegisterProvider(object provider)
  {
    if(provider is TopLevel)
      _topLevel = (TopLevel)provider;
    Debug.WriteLine("");
  }
}   
