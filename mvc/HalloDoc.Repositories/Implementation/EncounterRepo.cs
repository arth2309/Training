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
    public class EncounterRepo : IEncounterRepo
    {
        private readonly ApplicationDbContext _context;

        public EncounterRepo(ApplicationDbContext context) 
        {
            _context = context;
        }

        public Encounter GetData(int requestid)
        {
            return _context.Encounters.FirstOrDefault(a => a.RequestId == requestid);
        }
    }   
}
