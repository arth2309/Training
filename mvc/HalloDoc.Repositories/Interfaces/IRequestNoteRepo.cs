using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestNoteRepo
    {
        RequestNote GetNoteData(int rid);
        Task<RequestNote> UpdateTable(RequestNote requestNote);
    }
}
