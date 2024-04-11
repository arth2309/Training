using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Implementation;
using HalloDoc.Repositories.PagedList;
using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;

namespace HallodocServices.Implementation
{
    public class AdminDashBoardServices : IAdminDashBoardServices
    {
        private readonly IRequestRepo _iRequestRepo;
        private readonly IRequestClientRepo _iRequestClientRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IPhysicianRepo _PhysicianRepo;
        private readonly IRequestStatusLogRepo _requestStatusLogRepo;

        public AdminDashBoardServices(IRequestRepo requestRepo, IRequestClientRepo iRequestClientRepo, IRegionRepo regionRepo, IPhysicianRepo PhysicianRepo, IRequestStatusLogRepo requestStatusLogRepo)
        {
            _iRequestRepo = requestRepo;
            _iRequestClientRepo = iRequestClientRepo;
            _regionRepo = regionRepo;
            _PhysicianRepo = PhysicianRepo;
            _requestStatusLogRepo = requestStatusLogRepo;
        }

        public AdminDashBoard newStates(int status, int currentPage,int typeid,int regionid, string name)
        {

            PaginatedList<NewState> newStates1 = getStates(status, currentPage,typeid,regionid,name);


            AdminDashBoard adminDashBoard = new();
            {
                adminDashBoard.AdminNewState = newStates1;
                adminDashBoard.NewCount = _iRequestClientRepo.GetNewStateData(1,0,0,null).Count();
                adminDashBoard.PendingCount = _iRequestClientRepo.GetNewStateData(2,0,0,null).Count();
                adminDashBoard.ActiveCount = _iRequestClientRepo.GetNewStateData(4,0, 0,null).Count() + _iRequestClientRepo.GetNewStateData(5,0,0,null).Count();
                adminDashBoard.ConcludeCount = _iRequestClientRepo.GetNewStateData(6, 0, 0, null).Count();
                adminDashBoard.ToCloseCount = _iRequestClientRepo.GetNewStateData(3,0, 0,null).Count() + _iRequestClientRepo.GetNewStateData(7,0,0,null).Count() + _iRequestClientRepo.GetNewStateData(8, 0, 0, null).Count();
                adminDashBoard.UnPaidCount = _iRequestClientRepo.GetNewStateData(9, 0, 0, null).Count();
            }
            return adminDashBoard;
        }

        public PaginatedList<NewState> getStates(int status, int currentPage,int typeid,int regionid,string name)
        {
            List<RequestClient> requestClients = new List<RequestClient>();

            if (status == 1)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(1,typeid, regionid,name);

            }
            else if (status == 2)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(2,typeid, regionid,name);

            }
            else if (status == 3)
            {

                requestClients = _iRequestClientRepo.GetNewStateData(4,typeid, regionid,name);
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(5,typeid,regionid,name));

            }

            else if (status == 4)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(6,typeid, regionid,name);


            }

            else if (status == 5)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(3,typeid,regionid,name);
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(7,typeid, regionid,name));
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(8,typeid,regionid,name));



            }

            else if (status == 6)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(9, typeid, regionid,name);



            }
            else
            {
                requestClients = _iRequestClientRepo.GetNewStateData(1, typeid,regionid,name);

            }

            List<NewState> newStates = new List<NewState>();

            for (int i = 0; i < requestClients.Count; i++)
            {
                AdminCancelCase adminCancelCase = new AdminCancelCase();
                adminCancelCase.requestId = requestClients[i].RequestId;

                AdminAssignCase assignCase = new AdminAssignCase();
                assignCase.RequestId = requestClients[i].RequestId;
                assignCase.regions = _regionRepo.GetRegions();


                AdminBlockCase blockCase = new AdminBlockCase();
                blockCase.requestId = requestClients[i].RequestId;
                blockCase.Email = requestClients[i].Email;
                blockCase.Mobile = requestClients[i].PhoneNumber;

                SendAgreement sendAgreement = new SendAgreement();
                sendAgreement.Requestid = requestClients[i].RequestId;

                Region region = _regionRepo.GetRegion(requestClients[i].RegionId);
                string regionName = region == null?"-":region.Name;


                NewState newState = new();
                {
                    if (requestClients[i].Request.PhysicianId != null)
                    {
                        Physician physician = _PhysicianRepo.GetPhysician(requestClients[i].Request.PhysicianId);
                        newState.physicianName = "Dr." + physician.FirstName + " " + physician.LastName;
                    }

                    List<RequestStatusLog> requestStatusLogs = _requestStatusLogRepo.GetData(requestClients[i].RequestId);

                    String notes;
                    if (requestStatusLogs.Count() > 0)
                    {
                        int j = requestStatusLogs.Count() - 1;
                         notes = requestStatusLogs[j].Notes;

                    }
                    else
                    {
                        notes = "-";
                    }

                    newState.RFirstName = requestClients[i].Request.FirstName;
                    newState.RLastName = requestClients[i].Request.LastName;
                    newState.FirstName = requestClients[i].FirstName;
                    newState.LastName = requestClients[i].LastName;
                    newState.CreatedDate = requestClients[i].Request.CreatedDate;
                    newState.Street = requestClients[i].Street;
                    newState.City = requestClients[i].City;
                    newState.State = requestClients[i].State;
                    newState.Status = requestClients[i].Request.Status;
                    newState.Mobile = requestClients[i].PhoneNumber;
                    newState.id = requestClients[i].RequestClientId;
                    newState.Email = requestClients[i].Email;
                    newState.RequestId = requestClients[i].RequestId;
                    newState.cancelCases = adminCancelCase;
                    newState.assignCases = assignCase;
                    newState.blockCases = blockCase;
                    newState.RequestTypeId = requestClients[i].Request.RequestTypeId;
                    newState.sendAgreement = sendAgreement;
                    newState.Notes = notes;
                    newState.regionName = regionName;
                    newState.StrMonth = requestClients[i].StrMonth;
                    newState.year = requestClients[i].IntYear;
                    newState.day = requestClients[i].IntDate;

                };
                newStates.Add(newState);

            }
            return PaginatedList<NewState>.ToPagedList(newStates, currentPage, 5);

        }

        public DataTable getData()
        {
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "RequestData";
            //Add Columns  

            dt.Columns.Add("Sr.", typeof(int));
            dt.Columns.Add("First Name", typeof(string));
            dt.Columns.Add("Last Name", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("status", typeof(int));

            List<RequestClient> requestClients = _iRequestClientRepo.requestClient();
            for(int i = 0;i<requestClients.Count;i++)
            {
                dt.Rows.Add(i + 1, requestClients[i].FirstName, requestClients[i].LastName, requestClients[i].PhoneNumber, requestClients[i].Email, requestClients[i].Request.Status);
            }
            dt.AcceptChanges();
            return dt;
        }

        public DataTable getExportData(int status, int currentpage,int typeid,int regionid,string name)
        {
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "ImportData";
            //Add Columns  

            dt.Columns.Add("Sr.", typeof(int));
            dt.Columns.Add("First Name", typeof(string));
            dt.Columns.Add("Last Name", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("status", typeof(int));

            List<RequestClient> requestClients;
            if(status == 1) 
            {
                requestClients = _iRequestClientRepo.GetNewStateData(1, typeid, regionid, name);
            }
            else if(status == 2) 
            {
                requestClients = _iRequestClientRepo.GetNewStateData(2, typeid, regionid, name);
            }
            else if (status == 3)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(4, typeid, regionid, name);
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(5, typeid, regionid, name));
            }
            else if (status == 4)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(6, typeid, regionid, name);
            }
            else if (status == 5)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(3, typeid, regionid, name);
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(7, typeid, regionid, name));
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(8, typeid, regionid, name));
            }
            else
            {
                requestClients = _iRequestClientRepo.GetNewStateData(9, typeid, regionid, name);
            }

            int requestcount = currentpage;
            for (int i = (requestcount-1)*5; i < requestClients.Count && i < (requestcount-1)*5+5; i++)
            {
                dt.Rows.Add(i + 1, requestClients[i].FirstName, requestClients[i].LastName, requestClients[i].PhoneNumber, requestClients[i].Email, requestClients[i].Request.Status);
            }
            dt.AcceptChanges();
            return dt;
        }

        public void SendEmail(string FirstName,string LastName, string email)
        {
            MailMessage mm = new MailMessage("tatva.dotnet.arthgandhi@outlook.com", email);
            mm.Subject = "Create Request";
            string url = "https://localhost:7091/Patient/SubmitRequest";
            mm.Body = string.Format("Hi <p><a href=\"" + url+ "\">Create Request</a></p>");
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName: "tatva.dotnet.arthgandhi@outlook.com", password: "Liony@2002");
            smtp.Port = 587;
            smtp.Send(mm);
        }

    }

    }


