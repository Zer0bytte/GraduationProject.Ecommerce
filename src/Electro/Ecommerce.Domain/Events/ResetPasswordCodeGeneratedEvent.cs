using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Events;
public class ResetPasswordCodeGeneratedEvent
{
    public string ResetToken { get; set; } = default!;
    public string Email { get; set; } = default!;
}