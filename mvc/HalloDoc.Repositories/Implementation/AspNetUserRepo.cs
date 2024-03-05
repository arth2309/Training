using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;

namespace HalloDoc.Repositories.Implementation
{
    public class AspNetUserRepo
    {
        private readonly ApplicationDbContext _context;

        public AspNetUserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        
    }
}
