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

namespace HalloDoc.Controllers
{
    public class AdminSiteController : Controller
    {
        private readonly IAdminDashBoardServices _dashBoardServices;

        public AdminSiteController(IAdminDashBoardServices dashBoardServices)
        {
            _dashBoardServices = dashBoardServices;
        }

        public IActionResult AdminDashBoard(int page ,int pageSize = 10)
        {
            AdminDashBoard adminDashBoard = _dashBoardServices.newStates(page,pageSize);
            return View(adminDashBoard);
        }

       


    }
}
