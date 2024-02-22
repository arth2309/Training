using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.Implementation
{
    public class AdminDashBoardServices : IAdminDashBoardServices
    {
        private readonly IRequestRepo _iRequestRepo;
        private readonly IRequestClientRepo _iRequestClientRepo;

        public AdminDashBoardServices(IRequestRepo requestRepo, IRequestClientRepo iRequestClientRepo)
        {
            _iRequestRepo = requestRepo;
            _iRequestClientRepo = iRequestClientRepo;

        }

        public AdminDashBoard newStates(int page,int pageSize)
        {
            List<Request> requests = _iRequestRepo.GetNewStateName(page,pageSize);

            var TotalCount = _iRequestRepo.GetCount();
            var TotalPages = (int)Math.Ceiling((double)TotalCount / pageSize);
            
            List<NewState> newStates = new List<NewState>();
            for (int i = 0; i < requests.Count; i++)
            {
                RequestClient requestClients = _iRequestClientRepo.GetNewStateData(requests[i].RequestId);
                NewState newState = new();
                {
                    newState.RFirstName = requests[i].FirstName;
                    newState.RLastName = requests[i].LastName;
                    newState.FirstName = requestClients.FirstName;
                    newState.LastName = requestClients.LastName;
                    newState.CreatedDate = requests[i].CreatedDate;
                    newState.Street = requestClients.Street;
                    newState.City = requestClients.City;
                    newState.State = requestClients.State;
                    newState.Status = requests[i].Status;
                    newState.Mobile = requestClients.PhoneNumber;
                    newState.TotalCount = TotalCount;
                    newState.TotalPages = TotalPages;
                    newState.PageNumber = page;
                    newState.PageSize = pageSize;
                };
                newStates.Add(newState);

            }
            AdminDashBoard adminDashBoard = new();
            {
                adminDashBoard.AdminNewState = newStates.ToList();
            }
            return adminDashBoard;

        }
      


        

    }
    
}
