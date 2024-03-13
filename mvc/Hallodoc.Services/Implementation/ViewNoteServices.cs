using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.Implementation
{

    public class ViewNoteServices : IViewNoteServices
    {
        private readonly IRequestNoteRepo _repo;
        private readonly IRequestStatusLogRepo _logRepo;

        public ViewNoteServices(IRequestNoteRepo repo,IRequestStatusLogRepo logRepo)
        {
            _repo = repo;
            _logRepo = logRepo;
        }

        public AdminViewNote GetViewNote(int id)
        {
            var r1 = _repo.GetNoteData(id);
            if (r1 != null)
            {
                List<RequestStatusLog> requestStatusLog = _logRepo.GetData(id);

                AdminViewNote adminViewNote1 = new AdminViewNote();
                adminViewNote1.RequestNotesId = r1.RequestNotesId;
                adminViewNote1.AdminNotes = r1.AdminNotes;
                adminViewNote1.PhysicianNotes = r1.PhysicianNotes;
                adminViewNote1.RequestId = id;
                adminViewNote1.TransferNotes = requestStatusLog;
                return adminViewNote1;
            }
            else
            {
                return null;
            }
        }

        public AdminViewNote EditAdminNote(AdminViewNote adminViewNote)
        {
            if(adminViewNote.RequestNotesId != 0) { 
            RequestNote requestNote = _repo.GetNoteData(adminViewNote.RequestId);
            requestNote.AdminNotes = adminViewNote.AdminNotes;
            requestNote.PhysicianNotes = adminViewNote.PhysicianNotes;
            requestNote.RequestId = adminViewNote.RequestId;
            requestNote.RequestNotesId = adminViewNote.RequestNotesId;
            _repo.UpdateTable(requestNote);
            return adminViewNote;
            }

            else
            {
                RequestNote requestnote = new();
                requestnote.RequestId = adminViewNote.RequestId;
                requestnote.AdminNotes = adminViewNote.AdminNotes;
                requestnote.CreatedBy = 2;
                requestnote.CreatedDate = DateTime.Now;
                _repo.AddTable(requestnote);
                return adminViewNote;
                
            }

        }


    }

}
