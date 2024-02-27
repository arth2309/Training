﻿using System.Diagnostics.Eventing.Reader;
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
        private readonly IViewCaseServices _viewCaseServices;
        private readonly IViewNoteServices _viewNoteServices;

        public AdminSiteController(IAdminDashBoardServices dashBoardServices,IViewCaseServices viewCaseServices, IViewNoteServices viewNoteServices)
        {
            _dashBoardServices = dashBoardServices;
            _viewCaseServices = viewCaseServices;
            _viewNoteServices = viewNoteServices;

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
        public IActionResult ViewCase(int rcid)
        {
           AdminViewCase adminViewCase = _viewCaseServices.GetAdminViewCaseData(rcid);
            return View(adminViewCase);
        }

        [HttpPost]
        public async Task<IActionResult> ViewCase(AdminViewCase adminViewCase)
        {
            
            AdminViewCase adminViewCase1 = _viewCaseServices.EditViewData(adminViewCase);
            return View(adminViewCase1);
        }

       
        public IActionResult ChangeState(int rid)
        {
            AdminViewCase adminViewCase1 = _viewCaseServices.CancelViewData(rid);
            return RedirectToAction("AdminDashBoard");
        }

        public IActionResult ViewNotes(int reqid)
        {
            
            AdminViewNote adminViewNote = _viewNoteServices.GetViewNote(reqid);
            return View(adminViewNote ?? null);
        }

        [HttpPost]
        
        public async Task<IActionResult> ViewNotes(AdminViewNote adminViewNote)
        {
            AdminViewNote adminViewNote1 = _viewNoteServices.EditAdminNote(adminViewNote);
            return View(adminViewNote1);
        }


    }
}
