//using NotificationService.API.Persistence;
using NotificationService.API.Services;
using FluentValidation;
using System.Net;
using ReservationSystem.Shared.Results;
using NotificationService.API.Persistence;
using NotificationService.API.Logging;
using System.Text;


namespace NotificationService.API.Features;

public record SendEmailRequest(IList<string> address, string subject, string body);
public record SendEmailResponse(int? Id = null);

public class SendEmailRequestValidator : AbstractValidator<SendEmailRequest>
{
    public SendEmailRequestValidator()
    {
        RuleFor(x => x.address).NotEmpty();
        RuleForEach(x => x.address).EmailAddress();
        RuleFor(x => x.subject).NotEmpty().MaximumLength(75);
        RuleFor(x => x.body).NotEmpty().MaximumLength(1500);
    }
}

public class SendEmailHandler
{
    private readonly MailAppService _mailAppService;

    public SendEmailHandler(MailAppService mailAppService)
    {
        _mailAppService = mailAppService;
    }

    public async Task<ApiResult<SendEmailResponse>> Handle(SendEmailRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var emailArgs = new EmailArgs
        {
            Address = request.address,
            Subject = request.subject,
            Body = request.body
        };

        try
        {
            await _mailAppService.SendEmailAsync(emailArgs);
            return new ApiResult<SendEmailResponse>(new SendEmailResponse());
        }
        catch (Exception ex)
        {
            FileLogger.LogError($"Failed to send email to: {string.Join(", ", request.address)}", ex);
            return new ApiResult<SendEmailResponse>(null, false, "Email was not sent");
        }
    }
}
public static class SendEmailEndpoint
{
    public static void SendEmail(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/notification/sendemail",
            async (SendEmailRequest request, SendEmailHandler handler, SendEmailRequestValidator validator, CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Validation failed for SendEmailRequest:");
                    foreach (var error in validationResult.Errors)
                    {
                        sb.AppendLine($" - {error.PropertyName}: {error.ErrorMessage}");
                    }
                    FileLogger.LogError(sb.ToString());

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
