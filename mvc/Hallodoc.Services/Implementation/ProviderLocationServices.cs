using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class ProviderLocationServices : IProviderLocationServices
    {
        private readonly IPhysicianRepo _physicianRepo;

        public ProviderLocationServices(IPhysicianRepo physicianRepo)
        {
            _physicianRepo = physicianRepo;
        }

        public AdminProviderLocation GetLocation()
        {
            AdminProviderLocation location = new AdminProviderLocation();   
            List<PhysicianLocation> physicianLocations = _physicianRepo.GetProviderLocation();
            location.ProviderLocationList = physicianLocations;
            location.Count = physicianLocations.Count;
            return location;
        }

    }
}
