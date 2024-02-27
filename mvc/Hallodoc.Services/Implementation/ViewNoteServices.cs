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

        public ViewNoteServices(IRequestNoteRepo repo)
        {
            _repo = repo;
        }

        public AdminViewNote GetViewNote(int id)
        {
            var r1 = _repo.GetNoteData(id);
            if (r1 != null)
            {
                AdminViewNote adminViewNote1 = new AdminViewNote();
                adminViewNote1.RequestNotesId = r1.RequestNotesId;
                adminViewNote1.AdminNotes = r1.AdminNotes;
                adminViewNote1.PhysicianNotes = r1.PhysicianNotes;
                adminViewNote1.RequestId = id;
                return adminViewNote1;
            }
            else
            {
                return null;
            }
        }

        public AdminViewNote EditAdminNote(AdminViewNote adminViewNote)
        {
            RequestNote requestNote = _repo.GetNoteData(adminViewNote.RequestId);
            requestNote.AdminNotes = adminViewNote.AdminNotes;
            requestNote.PhysicianNotes = adminViewNote.PhysicianNotes;
            requestNote.RequestId = adminViewNote.RequestId;
            requestNote.RequestNotesId = adminViewNote.RequestNotesId;
            _repo.UpdateTable(requestNote);
            return adminViewNote;

    }


    }

}
