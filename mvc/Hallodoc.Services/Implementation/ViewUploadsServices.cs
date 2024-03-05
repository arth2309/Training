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
    public class ViewUploadsServices : IViewUploadsServices
    {
        private readonly IRequestFileRepo _fileRepo;
        private readonly IRequestClientRepo _clientRepo;

        public ViewUploadsServices(IRequestFileRepo fileRepo,IRequestClientRepo clientRepo)
        {
            _fileRepo = fileRepo;
            _clientRepo = clientRepo;
        }

        public List<AdminViewUpoads> GetUpoads(int rid) 
        {
            List<AdminViewUpoads> adminViewUpoads = new List<AdminViewUpoads>();    
            var requestfile = _fileRepo.GetAllFiles(rid);
            RequestClient requestClient = _clientRepo.requestClient1(rid);

            var count = 0;

            if (requestfile.Count > 0)
            {
               count = requestfile.Count;
                for (int i = 0; i < requestfile.Count; i++)
                {

                    AdminViewUpoads adminViewUpoads1 = new AdminViewUpoads();
                    adminViewUpoads1.filename = Path.GetFileName(requestfile[i].FileName.Trim());
                    adminViewUpoads1.Id = requestfile[i].RequestWiseFileId;
                    adminViewUpoads1.requestId = requestfile[i].RequestId;
                    adminViewUpoads1.CreatedDate = requestfile[i].CreatedDate;
                    adminViewUpoads1.Patientname = requestClient.FirstName + " " + requestClient.LastName;
                    adminViewUpoads1.count = count;


                    adminViewUpoads.Add(adminViewUpoads1);
                }
                return adminViewUpoads;
            }

            else
            {
                
                AdminViewUpoads adminViewUpoads1 = new AdminViewUpoads();
                adminViewUpoads1.requestId = rid;
                adminViewUpoads1.Patientname = requestClient.FirstName + " " + requestClient.LastName;
                adminViewUpoads1.count = 0;
                adminViewUpoads.Add(adminViewUpoads1);
              

                return adminViewUpoads;

            }
        }
    }
}
