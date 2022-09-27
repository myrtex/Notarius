using NotariusBack.Repository.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service.ModelDto
{
    public class ClientDto
    {
        public ClientTypeEnum Type { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }
    }
}
