using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestFileRepo
    {
        List<RequestWiseFile> GetAllFiles(int rid);
        Task<bool> DeleteFile(int id);
        int GetreqId(int id);
        Task<bool> AddData(RequestWiseFile requestWiseFile);
    }
}
