﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.Models.Requests
{
    public class LoginUserRequest
    {
        [Required] public string LogonName { get; set; } /*It's either Username or Email*/
        [Required] public string Password { get; set; }
    }
}
