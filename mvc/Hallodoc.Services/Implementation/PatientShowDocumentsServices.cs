using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class PatientShowDocumentsServices : IPatientShowDocumentsServices

    {
        private readonly IRequestFileRepo _fileRepo;
        private readonly IRequestRepo _requestRepo;
        public PatientShowDocumentsServices(IRequestFileRepo fileRepo, IRequestRepo requestRepo)
        {
            _fileRepo = fileRepo;
            _requestRepo = requestRepo;
        }

        public List<PatientShowDocument> showDocuments(int requestid)
        {
            List<RequestWiseFile> requestWiseFiles = _fileRepo.GetAllFiles(requestid);



            List<PatientShowDocument> showDocuments = new List<PatientShowDocument>();
            for (int i = 0; i < requestWiseFiles.Count; i++)
            {
                String uploader;
                Request request = _requestRepo.GetRequest(requestid);
                uploader = request.FirstName + request.LastName;
                RequestWiseFile requestWiseFile = requestWiseFiles[i];
                PatientShowDocument showDocuments1 = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    FileName = Path.GetFileName(requestWiseFile.FileName),
                    uploader = uploader,
                    UploadDate = requestWiseFile.CreatedDate

                };
                showDocuments.Add(showDocuments1);

            }

            return showDocuments;

        }
        
    }
}
