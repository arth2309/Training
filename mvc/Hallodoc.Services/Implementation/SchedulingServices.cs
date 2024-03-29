using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class SchedulingServices : ISchedulingServices
    {
        private readonly IPhysicianRepo _physicianRepo;

        public SchedulingServices(IPhysicianRepo physicianRepo)
        {
            _physicianRepo = physicianRepo;
        }

        public AdminScheduling GetData(int regionid)
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            SchedulingList schedulingList = new SchedulingList();
            schedulingList.GetPhysicians = physicians;

            AdminScheduling scheduling = new AdminScheduling();
            scheduling.SchedulingList = schedulingList;

            return scheduling;
        }

        public SchedulingList GetSchedulingList(int regionid) 
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            SchedulingList list = new SchedulingList();
            list.GetPhysicians = physicians;
            return list;
        }
    }
}
