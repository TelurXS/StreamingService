using Domain.Interfaces.Services;
using Domain.Models;
using FluentFTP;
using FluentFTP.Helpers;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace API.Services;

public sealed class FileService : IFileService
{
	private const string SECTION_NAME = "Ftp";

	private const string USERS_IMAGES_DIRECTORY = "/users/images";
	private const string TITLES_IMAGES_DIRECTORY = "/titles/images";
	private const string TITLES_SCREENSHOTS_DIRECTORY = "/titles/screenshots";
	private const string SERIES_DIRECTORY = "/series";

	private const string SEPARATOR = "/";

	public FileService(IConfiguration configuration)
	{
		var section = configuration.GetSection(SECTION_NAME);

		Host = section.GetValue<string>(nameof(Host))!;
		Username = section.GetValue<string>(nameof(Username))!;
		Password = section.GetValue<string>(nameof(Password))!;

		Credentials = new NetworkCredential(Username, Password);
	}

	private string Host { get; }
	private string Username { get; }
	private string Password { get; }

	private NetworkCredential Credentials { get; }

	public string Upload(string directory, IFormFile file)
	{
		var stream = file.OpenReadStream();

		var name = Guid.NewGuid().ToString();
		var extension = Path.GetExtension(file.FileName);
		var path = $"{directory}/{name}{extension}";

		using var client = new FtpClient(Host, Credentials);
		client.Connect();

		var status = client.UploadStream(stream, path);
		
		if (status.IsSuccess())
			return $"{name}{extension}";

		return status.ToString();
	}

	public byte[]? Download(string path)
	{
		using var client = new FtpClient(Host, Credentials);
		client.Connect();

		var stream = new MemoryStream();

		var status = client.DownloadBytes(out byte[] bytes, path);

		if (status is false)
			return null;

		return bytes;
	}

	public bool Delete(string path)
	{
		using var client = new FtpClient(Host, Credentials);
		client.Connect();

		try
		{
			client.DeleteFile(path);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public string UploadSeries(IFormFile file)
	{
		var name = Upload(SERIES_DIRECTORY, file);
		return ToServerUrl(ApiRoutes.Files.Series, name);
	}

	public string UploadTitleImage(IFormFile file)
	{
		var name = Upload(TITLES_IMAGES_DIRECTORY, file);
		return ToServerUrl(ApiRoutes.Files.TitleImages, name);
	}

	public string UploadTitleScreenshot(IFormFile file)
	{
		var name = Upload(TITLES_SCREENSHOTS_DIRECTORY, file);
		return ToServerUrl(ApiRoutes.Files.TitleScreenshots, name);
	}

	public string UploadUserImage(IFormFile file)
	{
		var name = Upload(USERS_IMAGES_DIRECTORY, file);
		return ToServerUrl(ApiRoutes.Files.UserImages, name);
	}

	public byte[]? DownloadUserImage(string name)
	{
		return Download(USERS_IMAGES_DIRECTORY + SEPARATOR + name);
	}

	public bool DeleteUserImage(string name)
	{
		return Delete(USERS_IMAGES_DIRECTORY + SEPARATOR + name);
	}

	public byte[]? DownloadTitleImage(string name)
	{
		return Download(TITLES_IMAGES_DIRECTORY + SEPARATOR + name);
	}

	public byte[]? DownloadTitleScreenshot(string name)
	{
		return Download(TITLES_SCREENSHOTS_DIRECTORY + SEPARATOR + name);
	}

	public byte[]? DownloadSeries(string name)
	{
		return Download(SERIES_DIRECTORY + SEPARATOR + name);
	}

	private string ToServerUrl(string serverRoute, string fileName)
	{
		return $"{serverRoute}{SEPARATOR}{fileName}";
	}
}
