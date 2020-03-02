using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users
{
    //Register,Login
    public class UserDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
