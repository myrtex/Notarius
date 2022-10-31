using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service.ModelDto
{
    public class DealDto
    {
        public DealStatusEnum Status { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public string Description { get; set; }
    }
}
