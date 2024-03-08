using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;

namespace HallodocServices.Implementation
{
    
    public class AssignCaseServices : IAssignCaseServices
    {
        private readonly IRegionRepo _regionRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IRequestStatusLogRepo _requestStatusLogRepo;
        private readonly IPhysicianRepo _physicianRepo;
         
        public AssignCaseServices(IRegionRepo regionRepo,IRequestRepo requestRepo,IRequestStatusLogRepo requestStatusLogRepo,IPhysicianRepo physicianRepo)
        {
            _regionRepo = regionRepo;
            _requestRepo = requestRepo;
            _requestStatusLogRepo = requestStatusLogRepo;
            _physicianRepo = physicianRepo;
        }
        
        public AdminAssignCase AdminAssignCase(AdminAssignCase adminAssignCase)
        {
            Request request = _requestRepo.GetRequest(adminAssignCase.RequestId);
            request.Status = 2;
            request.PhysicianId = adminAssignCase.PhysicianId;
            request.RequestId = adminAssignCase.RequestId;
            _requestRepo.UpdateTable(request);

            RequestStatusLog requestStatusLog = new();
            requestStatusLog.Status = 2;
            requestStatusLog.PhysicianId = adminAssignCase.PhysicianId;
            requestStatusLog.RequestId = adminAssignCase.RequestId;
            requestStatusLog.Notes = adminAssignCase.Description;
            _requestStatusLogRepo.AddData(requestStatusLog);

            return adminAssignCase;


        }

        public List<Region> GetRegions() 
        {

           List<Region> regions = _regionRepo.GetRegions();
            return regions;
        }
       
        public List<Physician> GetPhysciansByRegions(int regionid)
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansData(regionid);
            return physicians;
        }
        
    }
}
