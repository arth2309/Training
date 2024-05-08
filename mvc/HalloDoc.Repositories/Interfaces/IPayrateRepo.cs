using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IPayrateRepo
    {
        PhysicianPayrate GetData(int PhysicianId);

        Task<bool> AddData(PhysicianPayrate physicianPayrate);
        Task<bool> UpdateData(PhysicianPayrate physicianPayrate);

    }
}
