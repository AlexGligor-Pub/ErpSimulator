using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO.Response
{
    public class OrderGroupResponse
    {
        public string Group { get; set; } // This will hold status or priority
        public int Number { get; set; }   // This will hold the count of orders
    }
}
