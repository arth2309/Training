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
    public class PatientHistoryServices : IPatientHistoryServices
    {
        private readonly IUserRepo _userRepo;

        public PatientHistoryServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public PatientHistoryVM GetList()
        {
            PatientHistoryVM vm = new PatientHistoryVM();
            List<PatientHistoryList> patientHistoryLists = new List<PatientHistoryList>();
            List<User> users = _userRepo.GetPatientHistory(null, null, null, null);

            for(int i =0; i < users.Count; i++) 
            {
                PatientHistoryList patientHistoryList = new PatientHistoryList();
                patientHistoryList.FirstName = users[i].FirstName;
                patientHistoryList.LastName = users[i].LastName;
                patientHistoryList.Email = users[i].Email;
                patientHistoryList.Mobile = users[i].Mobile;
                patientHistoryList.Address = users[i].Street + "," + users[i].City + "," + users[i].State;
                patientHistoryList.UserId = users[i].UserId;

                patientHistoryLists.Add(patientHistoryList);
            }

            vm.list = PaginatedList<PatientHistoryList>.ToPagedList(patientHistoryLists, 1, 5);
            return vm;
        }

        public PaginatedList<PatientHistoryList> GetListFilter(string FirstName,string LastName, string Email,string Mobile, int CurrentPage)
        {
            List<PatientHistoryList> patientHistoryLists = new List<PatientHistoryList>();
            List<User> users = _userRepo.GetPatientHistory(FirstName,LastName,Email,Mobile);

            for (int i = 0; i < users.Count; i++)
            {
                PatientHistoryList patientHistoryList = new PatientHistoryList();
                patientHistoryList.FirstName = users[i].FirstName;
                patientHistoryList.LastName = users[i].LastName;
                patientHistoryList.Email = users[i].Email;
                patientHistoryList.Mobile = users[i].Mobile;
                patientHistoryList.Address = users[i].Street + "," + users[i].City + "," + users[i].State;
                patientHistoryList.UserId = users[i].UserId;

                patientHistoryLists.Add(patientHistoryList);
            }

            return PaginatedList<PatientHistoryList>.ToPagedList(patientHistoryLists,CurrentPage, 5);


        }
    }
}
