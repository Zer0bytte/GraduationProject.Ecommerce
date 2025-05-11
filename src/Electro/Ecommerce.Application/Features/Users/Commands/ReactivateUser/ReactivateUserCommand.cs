using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Users.Commands.RestoreUser;
public class ReactivateUserCommand : IRequest<ReactivateUserResult>
{
    public Guid UserId { get; set; }
}