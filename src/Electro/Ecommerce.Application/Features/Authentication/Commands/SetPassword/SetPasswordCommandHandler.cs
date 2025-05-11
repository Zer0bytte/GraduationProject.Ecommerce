using BuildingBlocks.Exceptions.Handler;
using ImageProcessor.Common.Exceptions;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Ecommerce.Application.Features.Authentication.Commands.SetPassword;
public class SetPasswordCommandHandler(UserManager<AppUser> userManager, ILogger<CustomExceptionHandler> logger) : IRequestHandler<SetPasswordCommand, SetPasswordResult>
{
    public async Task<SetPasswordResult> Handle(SetPasswordCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null) throw new UserNotFoundException();

        int result;
        string token = "";

        if (int.TryParse(command.ResetToken, out result))
        {
            token = command.ResetToken;
        }
        else
        {
            var base64EncodedBytes = Convert.FromBase64String(command.ResetToken);
            token = Encoding.UTF8.GetString(base64EncodedBytes);
        }

        IdentityResult changeResult = await userManager.ResetPasswordAsync(user, token, command.NewPassword);
        if (!changeResult.Succeeded)
        {
            logger.LogError("The Token Sent: " + token);

            foreach (var item in changeResult.Errors)
            {
                logger.LogError(item.Description);
            }
            throw new ApplicationException("برجاء المحاولة لاحقا");
        }
        return new SetPasswordResult
        {
            IsSuccess = true
        };
    }
}