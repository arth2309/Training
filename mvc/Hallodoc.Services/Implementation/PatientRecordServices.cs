using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.PagedList;
using HalloDoc.Repositories.DataModels;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;

namespace HallodocServices.Implementation
{
    public class PatientRecordServices : IPatientRecordServices
    {
        private readonly IRequestRepo _repo;

        public PatientRecordServices(IRequestRepo repo)
        {
            _repo = repo;
        }

        public PatientRecordVM GetPatientRecord(int UserId)
        {
            PatientRecordVM patientRecordVMs = new PatientRecordVM();
            List<PatientRecordList> patientRecordLists = new List<PatientRecordList>();

            List<Request> requests = _repo.GetAllRequests(UserId);

            for (int i = 0; i < requests.Count; i++)
            {
                PatientRecordList patientRecordList = new PatientRecordList();
                patientRecordList.Name = requests[i].FirstName + " " + requests[i].LastName;
                patientRecordList.CreatedDate = DateOnly.FromDateTime(requests[i].CreatedDate);
                patientRecordList.ConcludesdDate = DateOnly.FromDateTime(requests[i].CreatedDate);
                patientRecordList.ConfirmationNumber = "-";
                patientRecordList.ProviderName = requests[i].Physician == null ? "-" : requests[i].Physician.FirstName + " " + requests[i].Physician.LastName;
                patientRecordList.status = requests[i].Status;
                patientRecordList.RequestId = requests[i].RequestId;

                patientRecordLists.Add(patientRecordList);
            }

            patientRecordVMs.list = PaginatedList<PatientRecordList>.ToPagedList(patientRecordLists, 1, 5);
            patientRecordVMs.UserId = UserId;
            return patientRecordVMs;
        }

       public PaginatedList<PatientRecordList> GetRecordFilter(int UserId,int CurrentPage)
        {

            List<PatientRecordList> patientRecordLists = new List<PatientRecordList>();
            List<Request> requests = _repo.GetAllRequests(UserId);

            for (int i = 0; i < requests.Count; i++)
            {
                PatientRecordList patientRecordList = new PatientRecordList();
                patientRecordList.Name = requests[i].FirstName + " " + requests[i].LastName;
                patientRecordList.CreatedDate = DateOnly.FromDateTime(requests[i].CreatedDate);
                patientRecordList.ConcludesdDate = DateOnly.FromDateTime(requests[i].CreatedDate);
                patientRecordList.ConfirmationNumber = "-";
                patientRecordList.ProviderName = requests[i].Physician == null ? "-" : requests[i].Physician.FirstName + " " + requests[i].Physician.LastName;
                patientRecordList.status = requests[i].Status;
                patientRecordList.RequestId = requests[i].RequestId;

                patientRecordLists.Add(patientRecordList);
            }

            return PaginatedList<PatientRecordList>.ToPagedList(patientRecordLists, CurrentPage, 5);

        }


    }
}
