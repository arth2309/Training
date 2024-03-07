using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IViewUploadsServices
    {
        AdminViewUpoads GetUpoads(int rid);
        Task<bool> DeleteFileService(int id);
        int GetReqIdService(int id);
        Task<bool> AddFileData(AdminViewUpoads adminViewUpoads);
    }
}
