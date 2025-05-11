using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.ChangePassword;
public class ChangePasswordCommand : IRequest<ChangePasswordResult>
{
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}