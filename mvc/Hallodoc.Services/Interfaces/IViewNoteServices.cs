using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IViewNoteServices
    {
        AdminViewNote GetViewNote(int id);
        AdminViewNote EditAdminNote(AdminViewNote adminViewNote);

        Task<AdminViewNote> EditPhysicianNote(AdminViewNote adminViewNote);
    }
}
