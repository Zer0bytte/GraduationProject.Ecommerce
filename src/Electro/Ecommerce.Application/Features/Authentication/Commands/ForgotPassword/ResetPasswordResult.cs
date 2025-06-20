using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.ForgotPassword;
public class ResetPasswordResult
{
    public Guid UserId { get; set; }
}