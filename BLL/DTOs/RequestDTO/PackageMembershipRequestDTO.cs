using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.RequestDTO
{
    public class PackageMembershipRequestDTO
    {
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
    }
}
