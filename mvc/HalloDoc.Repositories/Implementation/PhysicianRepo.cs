using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation
{
    public class PhysicianRepo : IPhysicianRepo
    {
        private readonly ApplicationDbContext _DbContext;
        public PhysicianRepo(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<Physician> GetPhysiciansData(int regionId)
        {
            return  _DbContext.Physicians.Where(a=>a.RegionId == regionId).ToList();

        }

        public List<Physician> GetPhysiciansListForScheduling(int regionId)
        {
            if(regionId == 0)
            {
                return _DbContext.Physicians.ToList();
            }
            return _DbContext.Physicians.Where(a => a.RegionId == regionId).ToList();

        }

        public List<PhysicianNotification> GetListForNotifications(int regionId)
        {
            List<PhysicianNotification> physicianNotifications;
            if (regionId == 0)
            {
                physicianNotifications = _DbContext.PhysicianNotifications.Include(a => a.Physician).OrderBy(a => a.PhysicianId).ToList();
                return physicianNotifications;

            }
            else
            {

                physicianNotifications = _DbContext.PhysicianNotifications.Include(a => a.Physician).Where(a => a.Physician.RegionId == regionId).OrderBy(a=>a.PhysicianId).ToList();
                return physicianNotifications;
            }
        }

        public List<Physician> GetPhysiciansList()
        {
            return _DbContext.Physicians.ToList();

        }


        public Physician GetPhysician(int? physicianid)
        {
            return _DbContext.Physicians.Include(a=>a.AspNetUser).Include(a=>a.PhysicianRegions).FirstOrDefault(a => a.PhysicianId == physicianid);
        }

        public async Task<Physician> AddDatainPhysician(Physician physician)
        {
            _DbContext.Physicians.Add(physician);
            await _DbContext.SaveChangesAsync();
            return physician;
        }

        public async Task<PhysicianRegion> AddDataInPhysicianRegion(PhysicianRegion physicianRegion)
        {
            _DbContext.PhysicianRegions.Add(physicianRegion);
            await _DbContext.SaveChangesAsync();
            return physicianRegion;
        }

        public PhysicianNotification GetPhysicianNotificationData(int id)
        {
            return _DbContext.PhysicianNotifications.FirstOrDefault(a => a.Id == id);
        }

        public async Task<PhysicianNotification> AddDataInPhysicianNotification(PhysicianNotification physicianNotification)
        {
            _DbContext.PhysicianNotifications.Add(physicianNotification);
            await _DbContext.SaveChangesAsync();
            return physicianNotification;
        }

        public async Task<PhysicianNotification> UpdateDataInPhysicianNotification(PhysicianNotification physicianNotification)
        {
            _DbContext.PhysicianNotifications.Update(physicianNotification);
            await _DbContext.SaveChangesAsync();
            return physicianNotification;
        }

        public List<PhysicianLocation> GetProviderLocation()
        {
            return _DbContext.PhysicianLocations.ToList();
        }

        public async Task<Physician> UpdateDatainPhysician(Physician physician)
        {
            _DbContext.Physicians.Update(physician);
            await _DbContext.SaveChangesAsync();
            return physician;
        }

        public async Task<PhysicianRegion> RemoveDataInPhysicianRegion(PhysicianRegion physicianRegion)
        {

            _DbContext.PhysicianRegions.Remove(physicianRegion);
            await _DbContext.SaveChangesAsync();
            return physicianRegion;
        }
    }
}
