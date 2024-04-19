using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;
using System.Net;
using System.IO.Compression;

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
           await _fileRepo.DeleteFile(id);
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
           await _fileRepo.AddData(requestWise);
            return true;
        }

        public void SendEmail(int requestid)
        {
            
            MailMessage mm = new MailMessage("tatva.dotnet.arthgandhi@outlook.com", "arthgandhi151@gmail.com");
            mm.Subject = "Agreement";
            List<RequestWiseFile> requestWises = _fileRepo.GetAllFiles(requestid);
            for(int i= 0;i<requestWises.Count;i++)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                path = Path.Combine(path, requestWises[i].FileName);
                Attachment attachment = new Attachment(path);
                mm.Attachments.Add(attachment);
            }
           
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName: "tatva.dotnet.arthgandhi@outlook.com", password: "Liony@2002");
            smtp.Port = 587;
            smtp.Send(mm);
        }

        public byte[] GetFilesAsZip(int requestid)
        {

            List<RequestWiseFile> files = _fileRepo.GetAllFiles(requestid);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", file.FileName);
                        if (File.Exists(filePath))
                        {
                            // Add file to the zip archive
                            
                            string fileName = file.FileName;
                            var zipEntry = archive.CreateEntry(fileName);
                            using (var entryStream = zipEntry.Open())
                            using (var fileStream = File.OpenRead(filePath))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
                return memoryStream.ToArray();
            }
        }

        public byte[] GetSelectedFilesAsZip(List<string> files)
        {

           
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", file);
                        if (File.Exists(filePath))
                        {
                            // Add file to the zip archive

                            string fileName = file;
                            var zipEntry = archive.CreateEntry(fileName);
                            using (var entryStream = zipEntry.Open())
                            using (var fileStream = File.OpenRead(filePath))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
                return memoryStream.ToArray();
            }
        }
    }
}
