using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.SetPassword;
public class SetPasswordCommand : IRequest<SetPasswordResult>
{
    public Guid UserId { get; set; } = default!;
    public string ResetToken { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}