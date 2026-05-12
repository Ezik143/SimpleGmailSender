# EmailSending

A small ASP.NET Core Web API project for sending email messages using MailKit and SMTP.

## Overview

This project exposes a simple email-sending endpoint that accepts a JSON payload with recipients, subject, and content. It uses `MailKit` to send emails through an SMTP server configured in `appsettings.json`.

## Technologies

- .NET 9
- ASP.NET Core Web API
- MailKit (`NETCore.MailKit`)
- OpenAPI / Swagger support via `Microsoft.AspNetCore.OpenApi`
- `Scalar.AspNetCore` for API reference mapping in development

## Project structure

- `Program.cs` - application startup and DI registration
- `Controllers/EmailController.cs` - API endpoint for sending email
- `Services/Intefaces/IEmailService.cs` - email service contract
- `Services/Implementation/EmailServices.cs` - SMTP email sender implementation
- `Configuration/EmailConfiguration.cs` - typed email configuration model
- `Entities/Message.cs` - email message entity
- `appsettings.json` - SMTP and application configuration

## Configuration

Set the SMTP settings in `EmailSending/appsettings.json` before running the app:

```json
"EmailConfiguration": {
  "From": "Test@gmail.com",
  "SmtpServer": "smtp.gmail.com",
  "Port": 587,
  "Username": "your-Email",
  "Password": "your-Password"
}
```

> Note: Do not store real credentials in source control. Use user secrets, environment variables, or a secure vault for production.

## Run the app

From the project root:

```powershell
cd EmailSending
dotnet run
```

The application will start and listen on the configured ports. In development, Swagger/OpenAPI is available.

## API endpoint

### POST /Email/send

Send an email with this request body:

```json
{
  "to": ["recipient@example.com"],
  "subject": "Test Email",
  "content": "Hello from EmailSending API"
}
```

Example response:

```json
{
  "message": "Email sent successfully."
}
```

## Notes

- The endpoint returns `400 Bad Request` if the request has no recipients.
- The email sender uses `SecureSocketOptions.StartTls` when connecting to SMTP.

## Development

- Ensure `.NET 9 SDK` is installed.
- Restore NuGet packages automatically with `dotnet restore` when building.
- Use a valid SMTP account and server settings for email delivery.
