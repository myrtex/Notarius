using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotariusBack.Repository.Entity.Enums;

namespace NotariusBack.Repository.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public UserRoleEnum Role { get; set; }
    }
}
