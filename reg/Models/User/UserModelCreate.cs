using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Models.User
{
    public class UserModelCreate
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
