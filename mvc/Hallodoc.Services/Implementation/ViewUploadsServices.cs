﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;
using System.Security.Cryptography.X509Certificates;

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

        public AdminViewUpoads GetUpoads(int rid) 
        {
            AdminViewUpoads adminViewUpoads = new AdminViewUpoads();    
            var requestfile = _fileRepo.GetAllFiles(rid);
            RequestClient requestClient = _clientRepo.requestClient1(rid);

            List<RequestWiseFile> requestWiseFile = new List<RequestWiseFile>();


            adminViewUpoads.requestId = rid;
            adminViewUpoads.Patientname = requestClient.FirstName + " " + requestClient.LastName;
            adminViewUpoads.count = requestfile.Count();

            if (requestfile.Count > 0)
            {
               
                for (int i = 0; i < requestfile.Count; i++)
                {
                    RequestWiseFile requestWise = new RequestWiseFile();
                    requestWise.RequestWiseFileId = requestfile[i].RequestWiseFileId;
                    requestWise.CreatedDate = requestfile[i].CreatedDate;
                    requestWise.FileName = Path.GetFileName(requestfile[i].FileName);
                    requestWiseFile.Add(requestWise);
                }
                adminViewUpoads.WiseFiles = requestWiseFile;
            }

                return adminViewUpoads;
        }

        public async Task<bool> DeleteFileService(int id)
        {
            _fileRepo.DeleteFile(id);
            return true;
        }

        public int GetReqIdService(int id) 
        {
          int reqid =  _fileRepo.GetreqId(id);
            return reqid;
        }

        public async Task<bool> AddFileData(AdminViewUpoads adminViewUpoads)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
            FileInfo fileInfo = new FileInfo(adminViewUpoads.formFile.FileName);
            string fileName = adminViewUpoads.formFile.FileName;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                adminViewUpoads.formFile.CopyTo(stream);
            }
            
            RequestWiseFile requestWise = new RequestWiseFile();
            requestWise.RequestId = adminViewUpoads.requestId;
            requestWise.FileName = fileName;
            requestWise.CreatedDate = DateTime.Now;
            _fileRepo.AddData(requestWise);
            return true;
        }
    }
}
