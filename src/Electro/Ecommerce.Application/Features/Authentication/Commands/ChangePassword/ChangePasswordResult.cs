﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.ChangePassword;
public class ChangePasswordResult
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}