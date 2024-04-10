using HalloDoc.Repositories.DataModels;
using System.Data;
using HalloDoc.Repositories.Implementation;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.PagedList;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class SearchRecordServices : ISearchRecordServices
    {
        private readonly IRequestClientRepo _repo;
        private readonly IRequestRepo _requestRepo;

        public SearchRecordServices(IRequestClientRepo repo, IRequestRepo requestRepo)
        {
            _repo = repo;
            _requestRepo = requestRepo;
        }


        public SearchRecordVM GetList()
        {
            DateOnly dateOnly = new DateOnly(0001,01,01);
            SearchRecordVM searchRecordVM = new SearchRecordVM();
            List<SearchRecordList> searchRecordLists = new List<SearchRecordList>();
            List<RequestClient> requestClients = _repo.SearchRecordList(null,null,null,null,0,dateOnly,dateOnly);
            for(int i = 0;i < requestClients.Count; i++) 
            {
                SearchRecordList searchRecordList = new SearchRecordList();
                searchRecordList.RequestId = requestClients[i].Request.RequestId;
                searchRecordList.PatientName = requestClients[i].FirstName + "," + requestClients[i].LastName;
                searchRecordList.Requestor = requestClients[i].Request.FirstName + " " + requestClients[i].LastName;
                searchRecordList.PhoneNumber = requestClients[i].PhoneNumber;
                searchRecordList.Email = requestClients[i].Email;
                searchRecordList.AdminNote =  requestClients[i].Request.RequestNotes.LastOrDefault(a=>a.RequestId == requestClients[i].RequestId) == null? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).AdminNotes;
                searchRecordList.PhysicianNote = requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId) == null ? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).PhysicianNotes;
                searchRecordList.PatientNote = "-";
                searchRecordList.DateOfService = DateOnly.FromDateTime(requestClients[i].Request.CreatedDate);
                searchRecordList.Physician = requestClients[i].Request.Physician == null ? "-" : "Dr " + requestClients[i].Request.Physician.FirstName + " " + requestClients[i].Request.Physician.LastName;
                searchRecordList.Zip = requestClients[i].ZipCode;
                searchRecordList.Address = requestClients[i].Street + "," + requestClients[i].City + "," + requestClients[i].State;
                searchRecordList.ProviderCancelledNote = "-";

                searchRecordLists.Add(searchRecordList);
            }

            searchRecordVM.lists = PaginatedList<SearchRecordList>.ToPagedList(searchRecordLists, 1, 5);
            return searchRecordVM;
        }

        public PaginatedList<SearchRecordList> GetListFilter(string PatientName, string ProviderName, int TypeId, string Email, string Mobile, DateOnly FDate, DateOnly TDate, int CurrentPage)
        {
            List<SearchRecordList> searchRecordLists = new List<SearchRecordList>();
            List<RequestClient> requestClients = _repo.SearchRecordList(PatientName, ProviderName, Email, Mobile, TypeId, TDate, FDate);

            for (int i = 0; i < requestClients.Count; i++)
            {
                SearchRecordList searchRecordList = new SearchRecordList();
                searchRecordList.RequestId = requestClients[i].Request.RequestId;
                searchRecordList.PatientName = requestClients[i].FirstName + "," + requestClients[i].LastName;
                searchRecordList.Requestor = requestClients[i].Request.FirstName + " " + requestClients[i].LastName;
                searchRecordList.PhoneNumber = requestClients[i].PhoneNumber;
                searchRecordList.Email = requestClients[i].Email;
                searchRecordList.AdminNote = requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId) == null ? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).AdminNotes;
                searchRecordList.PhysicianNote = requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId) == null ? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).PhysicianNotes;
                searchRecordList.PatientNote = "-";
                searchRecordList.DateOfService = DateOnly.FromDateTime(requestClients[i].Request.CreatedDate);
                searchRecordList.Physician = requestClients[i].Request.Physician == null ? "-" : "Dr " + requestClients[i].Request.Physician.FirstName + " " + requestClients[i].Request.Physician.LastName;
                searchRecordList.Zip = requestClients[i].ZipCode;
                searchRecordList.Address = requestClients[i].Street + "," + requestClients[i].City + "," + requestClients[i].State;
                searchRecordList.ProviderCancelledNote = "-";

                searchRecordLists.Add(searchRecordList);
            }

            return PaginatedList<SearchRecordList>.ToPagedList(searchRecordLists, CurrentPage, 5);

        }

        public DataTable getExportData(string PatientName, string ProviderName, int TypeId, string Email, string Mobile, DateOnly FDate, DateOnly TDate)
        {
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "Record";
            //Add Columns  

            dt.Columns.Add("Sr.", typeof(int));
            dt.Columns.Add("Patient Name", typeof(string));
            dt.Columns.Add("Requestor", typeof(string));
            dt.Columns.Add("Date of Service", typeof(DateOnly));
            dt.Columns.Add("Date of Close Case", typeof(DateOnly));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone Number", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("ZipCode", typeof(string));
            dt.Columns.Add("Provider", typeof(string));
            dt.Columns.Add("Provider Note", typeof(string));
            dt.Columns.Add("Admin Note", typeof(string));
            dt.Columns.Add("Patient Note", typeof(string));

            List<RequestClient> requestClients = _repo.SearchRecordList(PatientName, ProviderName, Email, Mobile, TypeId, TDate, FDate);
            for (int i =0; i<requestClients.Count;i++)
            {
                dt.Rows.Add(i + 1, requestClients[i].FirstName + "," + requestClients[i].LastName, requestClients[i].Request.FirstName + " " + requestClients[i].LastName, DateOnly.FromDateTime(requestClients[i].Request.CreatedDate), DateOnly.FromDateTime(requestClients[i].Request.CreatedDate), requestClients[i].Email, requestClients[i].PhoneNumber, requestClients[i].Street + "," + requestClients[i].City + "," + requestClients[i].State, requestClients[i].ZipCode, requestClients[i].Request.Physician == null ? "-" : "Dr " + requestClients[i].Request.Physician.FirstName + " " + requestClients[i].Request.Physician.LastName, requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId) == null ? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).PhysicianNotes, requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId) == null ? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).AdminNotes,"-");
                
            }
            dt.AcceptChanges();
            return dt;
        }

        public async Task<int> Delete(int RequestId)
        {
            Request request = _requestRepo.GetRequest(RequestId);
            request.IsDeleted = new System.Collections.BitArray(1,true);
            await _requestRepo.UpdateTable(request);
            return RequestId;
        }

        
    }
}
