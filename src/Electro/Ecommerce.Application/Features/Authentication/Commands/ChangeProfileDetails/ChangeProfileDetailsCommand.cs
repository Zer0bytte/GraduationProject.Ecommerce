using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.ChangeProfileDetails;
public class ChangeProfileDetailsCommand : IRequest<ChangeProfileDetailsResult>
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
}