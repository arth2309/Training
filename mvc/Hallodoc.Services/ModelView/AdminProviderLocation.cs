using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminProviderLocation
    {
        public List<PhysicianLocation>? ProviderLocationList { get; set; }

        public int? Count { get; set; }
    }
}
