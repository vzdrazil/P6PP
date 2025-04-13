//using NotificationService.API.Persistence;
using NotificationService.API.Services;
using FluentValidation;
using System.Net;
using ReservationSystem.Shared.Results;
using NotificationService.API.Persistence;
using NotificationService.API.Logging;
using System.Text;

namespace NotificationService.API.Features;

public record SendRegistrationEmail(string address, string name, string? language);
public record SendRegistrationEmailResponse(int? Id = null);

public class SendRegistrationEmailValidator : AbstractValidator<SendRegistrationEmail>
{
    public SendRegistrationEmailValidator()
    {
        RuleFor(x => x.address).NotEmpty().EmailAddress();
        RuleFor(x => x.name).NotEmpty().MaximumLength(100);
    }
}

public class SendRegistrationEmailHandler
{
    private readonly MailAppService _mailAppService;
    private readonly TemplateAppService _templateAppService;

    public SendRegistrationEmailHandler(MailAppService mailAppService, TemplateAppService templateAppService)
    {
        _mailAppService = mailAppService;
        _templateAppService = templateAppService;
    }

    public async Task<ApiResult<SendRegistrationEmailResponse>> Handle(SendRegistrationEmail request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var template = await _templateAppService.GetTemplateAsync("Registration", request.language);
            template.Text = template.Text.Replace("{name}", request.name);

            var emailArgs = new EmailArgs
            {
                Address = new List<string> { request.address },
                Subject = template.Subject,
                Body = template.Text
            };

            await _mailAppService.SendEmailAsync(emailArgs);
            return new ApiResult<SendRegistrationEmailResponse>(new SendRegistrationEmailResponse());
        }
        catch (Exception ex)
        {
            FileLogger.LogError($"Failed to send registration email to: {request.address}", ex);
            return new ApiResult<SendRegistrationEmailResponse>(null, false, "Email was not sent");
        }
    }
}

public static class SendRegistrationEmailEndpoint
{
    public static void SendRegistrationEmail(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/notification/user/sendregistrationemail",
            async (SendRegistrationEmail request, SendRegistrationEmailHandler handler, SendRegistrationEmailValidator validator, CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errorLog = new StringBuilder();
                    errorLog.AppendLine("[VALIDATION ERROR] Registration email request validation failed:");
                    foreach (var error in validationResult.Errors)
                    {
                        errorLog.AppendLine($" - {error.PropertyName}: {error.ErrorMessage}");
                    }

                    FileLogger.LogError(errorLog.ToString());

                    var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new ApiResult<IEnumerable<string>>(errorMessages, false, "Validation failed"));
                }

                var result = await handler.Handle(request, cancellationToken);

                return result.Success
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });
    }
}
