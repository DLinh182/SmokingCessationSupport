using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ResponseDTO
{
    public class RankingResponseDTO
    {
        public int Rank { get; set; }
        public string FullName { get; set; }
        public string Badge { get; set; }
        public int TotalScore { get; set; }


    }
}
