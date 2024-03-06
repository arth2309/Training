using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation
{
    public class RequestFileRepo : IRequestFileRepo
    {
        private readonly HalloDoc.Repositories.DataContext.ApplicationDbContext _dbcontext;

        public RequestFileRepo(HalloDoc.Repositories.DataContext.ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<RequestWiseFile> GetAllFiles(int rid)
        {
            List <RequestWiseFile> requestWiseFiles = _dbcontext.RequestWiseFiles.Where(a=>a.RequestId == rid).ToList();
            return requestWiseFiles;
        }

        public async Task<bool>  DeleteFile(int id) 
        { 
            RequestWiseFile requestWiseFile = _dbcontext.RequestWiseFiles.FirstOrDefault(a=>a.RequestWiseFileId == id);
            requestWiseFile.IsDeleted = new System.Collections.BitArray(1,true);
            _dbcontext.RequestWiseFiles.Update(requestWiseFile);
            await _dbcontext.SaveChangesAsync();
            return true;

        }
    }
}
