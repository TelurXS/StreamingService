using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Domain.Models;

namespace API.Services;

public sealed class EmailSender : IEmailSender<User>
{
	private const string SECTION_NAME = "Smtp";

    public EmailSender(
		IConfiguration configuration,
		ClientRoutesService routesService)
    {
		RoutesService = routesService;

		var section = configuration.GetSection(SECTION_NAME);

		Host = section.GetValue<string>(nameof(Host))!;
		Port = section.GetValue<int>(nameof(Port))!;
		Address = section.GetValue<string>(nameof(Address))!;
		Password = section.GetValue<string>(nameof(Password))!;
	}

	private ClientRoutesService RoutesService { get; }

	private string Host { get; }
	private int Port { get; }
	private string Address { get; }
	private string Password { get; }

    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
	{
		var subject = "Confirmation Link";
		var link = RoutesService.ToConfirmEmailLink(confirmationLink);
		var body =
			$"""
				<h1>Hello {user.UserName}</h1>
				<span>Confirmation Link: {link}</span>
			""";

		await SendMailAsync(email, subject, body);
	}

	public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
	{
		var subject = "Reset Code";
		var link = RoutesService.ToResetPasswordLink(resetCode, email);
		var body =
			$"""
				<h1>Hello {user.UserName}</h1>
				<span>
					Your password reset link:
					<a href="{link}">Reset Password</a>
				</span>
			""";

		await SendMailAsync(email, subject, body);
	}

	public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
	{
		var subject = "Reset Link";
		var body =
			$"""
				<h1>Hello {user.UserName}</h1>
				<span>Reset Link: {resetLink}</span>
			""";

		await SendMailAsync(email, subject, body);
	}

	private async Task SendMailAsync(string to, string subject, string body)
	{
		var mail = new MimeMessage();
		mail.From.Add(MailboxAddress.Parse(Address));
		mail.To.Add(MailboxAddress.Parse(to));
		mail.Subject = subject;
		mail.Body = new TextPart(TextFormat.Html) { Text = body };

		using var client = new SmtpClient();
		await client.ConnectAsync(Host, Port);
		await client.AuthenticateAsync(Address, Password);
		await client.SendAsync(mail);
		await client.DisconnectAsync(true);
	}
}
