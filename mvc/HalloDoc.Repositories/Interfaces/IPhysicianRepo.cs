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

        Task<Physician> UpdateDatainPhysician(Physician physician);

        List<PhysicianNotification> GetListForNotifications(int regionId);

        Task<PhysicianRegion> AddDataInPhysicianRegion(PhysicianRegion physicianRegion);

        Task<PhysicianNotification> AddDataInPhysicianNotification(PhysicianNotification physicianNotification);

        List<Physician> GetPhysiciansListForScheduling(int regionId);

        PhysicianNotification GetPhysicianNotificationData(int id);

        Task<PhysicianNotification> UpdateDataInPhysicianNotification(PhysicianNotification physicianNotification);

        List<PhysicianLocation> GetProviderLocation();

        Task<PhysicianRegion> RemoveDataInPhysicianRegion(PhysicianRegion physicianRegion);


    }
}
