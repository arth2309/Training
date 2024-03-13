using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataContext;

namespace HalloDoc.Repositories.Implementation
{
    public class RequestNoteRepo : IRequestNoteRepo
    {
        private readonly ApplicationDbContext _dbcontext;

        public RequestNoteRepo(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public RequestNote GetNoteData(int rid) 
        { 
            var note = _dbcontext.RequestNotes.FirstOrDefault(a=>a.RequestId == rid);
            return note;
        }

        public async Task<RequestNote> UpdateTable(RequestNote requestNote)
        {
            _dbcontext.RequestNotes.Update(requestNote);
            await _dbcontext.SaveChangesAsync();
            return requestNote;
        }

        public async Task<RequestNote> AddTable(RequestNote requestNote)
        {
            _dbcontext.RequestNotes.Add(requestNote);
            await _dbcontext.SaveChangesAsync();
            return requestNote;
        }
    }
}
