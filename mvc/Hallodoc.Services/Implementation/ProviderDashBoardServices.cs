using DocumentFormat.OpenXml.Wordprocessing;
using HalloDoc.Repositories.DataModels;
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
    public class ProviderDashBoardServices : IProviderDashBoardServices
    {
        private readonly IRequestClientRepo _iRequestClientRepo;

        public ProviderDashBoardServices(IRequestClientRepo requestClientRepo)
        {
            _iRequestClientRepo = requestClientRepo;
        }

        public AdminDashBoard newStates(int status, int currentPage, int typeid, int physicianid, string name)
        {

            PaginatedList<NewState> newStates1 = getStates(name, typeid, physicianid, status,currentPage);


            AdminDashBoard adminDashBoard = new();
            {
                adminDashBoard.AdminNewState = newStates1;
                adminDashBoard.NewCount = _iRequestClientRepo.ProviderDashBoardList(null, 0, new List<int> { 1 }, physicianid).Count();
                adminDashBoard.PendingCount = _iRequestClientRepo.ProviderDashBoardList(null, 0, new List<int> { 2 }, physicianid).Count();
                adminDashBoard.ActiveCount = _iRequestClientRepo.ProviderDashBoardList(null, 0, new List<int> { 4,5 }, physicianid).Count();
                adminDashBoard.ConcludeCount = _iRequestClientRepo.ProviderDashBoardList(null, 0, new List<int> {6}, physicianid).Count();
                return adminDashBoard;
            }
        }

            public PaginatedList<NewState> getStates(string Name, int TypeId, int Physicianid, int status,int currentPage)
            {
                List<RequestClient> requestClients = new List<RequestClient>();

                if (status == 1)
                {

                    requestClients = _iRequestClientRepo.ProviderDashBoardList(Name, TypeId, new List<int> { 1 }, Physicianid);
                }
                else if (status == 2)
                {
                    requestClients = _iRequestClientRepo.ProviderDashBoardList(Name, TypeId, new List<int> { 2 }, Physicianid);

                }
                else if (status == 3)
                {

                    requestClients = _iRequestClientRepo.ProviderDashBoardList(Name, TypeId, new List<int> { 4, 5 }, Physicianid);
                }



                else if (status == 4)
                {
                    requestClients = _iRequestClientRepo.ProviderDashBoardList(Name, TypeId, new List<int> { 6 }, Physicianid);

                }


                else
                {
                    requestClients = _iRequestClientRepo.ProviderDashBoardList(Name, TypeId, new List<int> { 1 }, Physicianid);


                }

                List<NewState> newStates = new List<NewState>();

                for (int i = 0; i < requestClients.Count; i++)
                {

                    NewState newState = new();
                    {

                        newState.FirstName = requestClients[i].FirstName;
                        newState.LastName = requestClients[i].LastName;
                        newState.Street = requestClients[i].Street;
                        newState.City = requestClients[i].City;
                        newState.State = requestClients[i].State;
                        newState.Status = requestClients[i].Request.Status;
                        newState.Mobile = requestClients[i].PhoneNumber;
                        newState.id = requestClients[i].RequestClientId;
                        newState.RequestId = requestClients[i].RequestId;
                        newState.RequestTypeId = requestClients[i].Request.RequestTypeId;
                        newState.Calltype = requestClients[i].Request.CallType;
                        newState.IsFinalize = requestClients[i].Request.IsMobile!=null?false:true;
                        newState.Email = requestClients[i].Request.Email;
                       


                    };
                    newStates.Add(newState);

                }
                return PaginatedList<NewState>.ToPagedList(newStates, currentPage, 5);

            }

        

    }
}
