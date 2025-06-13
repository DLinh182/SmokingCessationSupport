using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ResponseDTO
{
    public class PackageMembershipResponseDTO
    {
        public int Package_membership_ID { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
    }
}
