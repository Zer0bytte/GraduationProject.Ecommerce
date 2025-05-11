using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.ResetPassword;
public class ResetPasswordCommand : IRequest<ResetPasswordResult>
{
    public string Email { get; set; } = default!;
}