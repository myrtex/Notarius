using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotariusBack.Repository.Entity.Enums;

namespace NotariusBack.Repository.Entity
{
    public class Client
    {
        public int Id { get; set; }

        public ClientTypeEnum Type { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }
        
        public string Phone { get; set; }
    }
}
