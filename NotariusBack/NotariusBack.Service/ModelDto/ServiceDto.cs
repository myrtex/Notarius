using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotariusBack.Service.ModelDto
{
    public class ServiceDto
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public double Commission { get; set; }
    }
}
