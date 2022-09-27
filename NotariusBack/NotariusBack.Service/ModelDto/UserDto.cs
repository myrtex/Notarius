using NotariusBack.Repository.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service.ModelDto
{
    public class UserDto
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public UserRoleEnum Role { get; set; }
    }
}
