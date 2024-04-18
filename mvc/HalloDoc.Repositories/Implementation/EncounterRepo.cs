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

        public async Task<Encounter> AddData(Encounter encounter) 
        {
            _context.Encounters.Add(encounter); 
            await _context.SaveChangesAsync();
            return encounter;
        }

        public async Task<Encounter> UpDateData(Encounter encounter)
        {
            _context.Encounters.Update(encounter);
            await _context.SaveChangesAsync();
            return encounter;
        }
    }   
}
