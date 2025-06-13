using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ResponseDTO
{
    public class CommentResponseDTO()
    {
        public string FullName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateOnly CreateTime { get; set; }
    }
}
