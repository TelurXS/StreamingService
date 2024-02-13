using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services;

public interface IFileService
{
	string Upload(string directory, IFormFile file);

	byte[]? Download(string path);

	string UploadUserImage(IFormFile file);

	string UploadTitleImage(IFormFile file);

	string UploadTitleScreenshot(IFormFile file);

	string UploadSeries(IFormFile file);

	byte[]? DownloadUserImage(string name);

	byte[]? DownloadTitleImage(string name);

	byte[]? DownloadTitleScreenshot(string name);

	byte[]? DownloadSeries(string name);
}
