using System;
using System.Collections.Generic;
using System.Text;

namespace LocalChow.Domain.DTO
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
    }
}
