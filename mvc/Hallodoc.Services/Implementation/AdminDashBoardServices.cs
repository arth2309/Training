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

        public AdminDashBoard newStates(int status, int currentPage)
        {

            PaginatedList<NewState> newStates1 = getStates(status, currentPage);


            AdminDashBoard adminDashBoard = new();
            {
                adminDashBoard.AdminNewState = newStates1;
                adminDashBoard.NewCount = _iRequestClientRepo.GetNewStateData(1).Count();
                adminDashBoard.PendingCount = _iRequestClientRepo.GetNewStateData(2).Count();
                adminDashBoard.ActiveCount = _iRequestClientRepo.GetNewStateData(4).Count() + _iRequestClientRepo.GetNewStateData(5).Count();
                adminDashBoard.ConcludeCount = _iRequestClientRepo.GetNewStateData(6).Count();
                adminDashBoard.ToCloseCount = _iRequestClientRepo.GetNewStateData(3).Count() + _iRequestClientRepo.GetNewStateData(7).Count() + _iRequestClientRepo.GetNewStateData(8).Count();
                adminDashBoard.UnPaidCount = _iRequestClientRepo.GetNewStateData(9).Count();
            }
            return adminDashBoard;
        }

        public PaginatedList<NewState> getStates(int status, int currentPage)
        {
            List<RequestClient> requestClients = new List<RequestClient>();

            if (status == 1)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(1);

            }
            else if (status == 2)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(2);

            }
            else if (status == 3)
            {

                requestClients = _iRequestClientRepo.GetNewStateData(4);
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(5));

            }

            else if (status == 4)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(6);


            }

            else if (status == 5)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(3);
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(7));
                requestClients.AddRange(_iRequestClientRepo.GetNewStateData(8));



            }

            else if (status == 6)
            {
                requestClients = _iRequestClientRepo.GetNewStateData(9);



            }
            else
            {
                requestClients = _iRequestClientRepo.GetNewStateData(1);

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
                string regionName = region.Name;


                NewState newState = new();
                {
                    if (requestClients[i].Request.PhysicianId != null)
                    {
                        Physician physician = _PhysicianRepo.GetPhysician(requestClients[i].Request.PhysicianId);
                        newState.physicianName = "Dr" + physician.FirstName + " " + physician.LastName;
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

                };
                newStates.Add(newState);

            }
            return PaginatedList<NewState>.ToPagedList(newStates, currentPage, 5);

        }




    }

}
