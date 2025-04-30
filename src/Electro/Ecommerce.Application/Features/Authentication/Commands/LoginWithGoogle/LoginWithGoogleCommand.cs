using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithGoogle;
public class LoginWithGoogleCommand : IRequest<LoginWithGoogleResult>
{
    public string Credentials { get; set; } = default!;
}
