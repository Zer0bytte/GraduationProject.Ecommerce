using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithGoogle;
public class LoginWithGoogleResult
{
    public string AccessToken { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}