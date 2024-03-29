using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IPhysicianRepo
    {
        List<Physician> GetPhysiciansData(int regionId);
        Physician GetPhysician(int? physicianid);

        List<Physician> GetPhysiciansList();

        Task<Physician> AddDatainPhysician(Physician physician);

        Task<PhysicianRegion> AddDataInPhysicianRegion(PhysicianRegion physicianRegion);

        Task<PhysicianNotification> AddDataInPhysicianNotification(PhysicianNotification physicianNotification);

        List<Physician> GetPhysiciansListForScheduling(int regionId);


    }
}
