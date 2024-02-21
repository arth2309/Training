using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.ModelView;

namespace HallodocServices.Interfaces
{
    public interface IPatientDashBoardServices
    {
        int GetId(int id);
        List<PatientDashBoard> patientDashBoards(int id);
    }
}

