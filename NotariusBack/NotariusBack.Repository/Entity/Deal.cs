using NotariusBack.Repository.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Repository.Entity
{
    public class Deal
    {
        public int Id { get; set; }
        public DealStatusEnum Status { get; set; }
        public DateTime Date { get; set; }
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
        public string Description { get; set; }
        public int? TransactionAmount { get; set; }
        public int RealPrice { get; set; }
        public double RealCommission { get; set; }
    }
}
