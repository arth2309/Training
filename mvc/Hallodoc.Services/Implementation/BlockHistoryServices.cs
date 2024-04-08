using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.PagedList;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class BlockHistoryServices : IBlockHistoryServices
    {
        private readonly IRequestClientRepo _repo;

        public BlockHistoryServices(IRequestClientRepo repo)
        {
            _repo = repo;
        }

        public BlockedHistoryVM GetBlockHistoryData()
        {
            DateTime dateTime = new DateTime(0001, 01, 01);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
           BlockedHistoryVM blockedHistoryVM = new BlockedHistoryVM();
            List<BlockedList> blockedLists = new List<BlockedList>();
            List<RequestClient> requestClients = _repo.BlockHistoryList(null,null,dateOnly,null);
            for(int i = 0; i < requestClients.Count; i++)
            {
                BlockedList blockedList = new BlockedList();
                blockedList.Email = requestClients[i].Email;
                blockedList.PatientName = requestClients[i].FirstName + "," + requestClients[i].LastName;
                blockedList.Notes = "good";
                blockedList.Month = requestClients[i].Request.CreatedDate.ToString("MMM");
                blockedList.year = requestClients[i].Request.CreatedDate.Year;
                blockedList.Day = requestClients[i].Request.CreatedDate.Day;
                blockedList.Mobile = requestClients[i].PhoneNumber;
                blockedLists.Add(blockedList);
            }

            blockedHistoryVM.blockedLists = PaginatedList<BlockedList>.ToPagedList(blockedLists, 1, 5);
            return blockedHistoryVM;
        }

        public PaginatedList<BlockedList> GetBlockHistoryDataFilter(string name, string mobile, string email, DateOnly date, int currentPage)
        {
          

            List<BlockedList> blockedLists = new List<BlockedList>();
            List<RequestClient> requestClients = _repo.BlockHistoryList(name,mobile, date,email);
            for (int i = 0; i < requestClients.Count; i++)
            {
                BlockedList blockedList = new BlockedList();
                blockedList.Email = requestClients[i].Email;
                blockedList.PatientName = requestClients[i].FirstName + "," + requestClients[i].LastName;
                blockedList.Notes = "good";
                blockedList.Month = requestClients[i].Request.CreatedDate.ToString("MMM");
                blockedList.year = requestClients[i].Request.CreatedDate.Year;
                blockedList.Day = requestClients[i].Request.CreatedDate.Day;
                blockedList.Mobile = requestClients[i].PhoneNumber;
                blockedLists.Add(blockedList);
            }

            return PaginatedList<BlockedList>.ToPagedList(blockedLists, currentPage, 5);

        }
    }
}
