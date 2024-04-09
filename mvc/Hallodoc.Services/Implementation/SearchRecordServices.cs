using HalloDoc.Repositories.DataModels;
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

        public SearchRecordServices(IRequestClientRepo repo)
        {
            _repo = repo;
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
                searchRecordList.PatientName = requestClients[i].FirstName + "," + requestClients[i].LastName;
                searchRecordList.Requestor = requestClients[i].Request.FirstName + " " + requestClients[i].LastName;
                searchRecordList.PhoneNumber = requestClients[i].PhoneNumber;
                searchRecordList.Email = requestClients[i].Email;
                searchRecordList.AdminNote =  requestClients[i].Request.RequestNotes.LastOrDefault(a=>a.RequestId == requestClients[i].RequestId) == null? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).AdminNotes;
                searchRecordList.PhysicianNote = requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId) == null ? "-" : requestClients[i].Request.RequestNotes.LastOrDefault(a => a.RequestId == requestClients[i].RequestId).PhysicianNotes;
                searchRecordList.PatientNote = "-";
                searchRecordList.DateOfService = DateOnly.FromDateTime(requestClients[i].Request.CreatedDate);
                searchRecordList.Physician = requestClients[i].Request.Physician == null ? "-" : "Dr" + requestClients[i].Request.Physician.FirstName + "" + requestClients[i].Request.Physician.LastName;
                searchRecordList.Zip = requestClients[i].ZipCode;
                searchRecordList.Address = requestClients[i].Street + "," + requestClients[i].City + "," + requestClients[i].State;
                searchRecordList.ProviderCancelledNote = "-";

                searchRecordLists.Add(searchRecordList);
            }

            searchRecordVM.lists = PaginatedList<SearchRecordList>.ToPagedList(searchRecordLists, 1, 5);
            return searchRecordVM;
        }
    }
}
