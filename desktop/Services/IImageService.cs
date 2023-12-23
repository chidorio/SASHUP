using System;
using System.Threading.Tasks;
using Refit;

namespace desktop;

public interface IImageService
{
  [Multipart]
  [Post("/Image/UploadImage")]
  public Task<string> UploadImage([Authorize("Bearer")] string accessToken, [AliasAs("file")] StreamPart file);
}
