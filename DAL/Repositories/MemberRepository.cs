using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
//sdfghjkl
namespace DAL.Repositories
{
    public class MemberRepository(SmokingCessationContext _context)
    {
        //CRUD
        //private SmokingCessationContext _context = null!; //bao voi may rang, cho nay chac chan khong null


        public async Task<List<Member>> GetAll()
        {
            //_context = new SmokingCessationContext();
            return await _context.Members.ToListAsync();
        }


    }
}
