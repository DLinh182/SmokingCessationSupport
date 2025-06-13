using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.RequestDTO
{
    public class CommentCreateRequestDTO
    {
        public int Post_ID { get; set; }
        public string Comment { get; set; }
    }
}
