using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Wheel.Commands.SpinWheel;
public class SpinWheelResult
{
    public bool Success { get; set; }
    public string? Reward { get; set; }
    public object? Value { get; set; }
}