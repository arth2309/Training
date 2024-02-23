using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using Azure;

namespace HalloDoc.Controllers
{
    public class AdminSiteController : Controller
    {
        private readonly IAdminDashBoardServices _dashBoardServices;

        public AdminSiteController(IAdminDashBoardServices dashBoardServices)
        {
            _dashBoardServices = dashBoardServices;
        }

        public IActionResult AdminDashBoard(int status)
        {
            AdminDashBoard adminDashBoard = _dashBoardServices.newStates(status);
            return View(adminDashBoard);
        }

        public IActionResult CheckStatus(int statusI)
        {
            List<NewState> newStates = _dashBoardServices.getStates(statusI);
          
            if(statusI == 1)
            {
                return PartialView("_NewState", newStates);
            }
            
            else if(statusI == 2)
            {
                return PartialView("_PendingState", newStates);
            }
            else if (statusI == 3)
            {
                return PartialView("_ActiveState", newStates);
            }
            else if (statusI == 4)
            {
                return PartialView("_ConcludeState", newStates);
            }
            else if (statusI == 5)
            {
                return PartialView("_ToCloseState", newStates);
            }
            else if (statusI == 6)
            {
                return PartialView("_UnPaidState", newStates);
            }
            else
            {
                return PartialView("_NewState", newStates);
            }





        }
       


    }
}
